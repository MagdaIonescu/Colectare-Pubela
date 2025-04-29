using colectare_date.Data;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using colectare_date.ViewModels;
using colectare_date.Models;

namespace colectare_date.Controllers
{
    public class HartaController : Controller
    {
        private readonly AppDbContext context;

        public HartaController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Harta()
        {
            if (HttpContext.Session.GetString("admin") != "true")
                return RedirectToAction("Index", "Login");

            var dataCautata = new DateTime(2024, 10, 15);
            var colectari = context.Colectari
                .Where(c => c.TimpColectare.Date == dataCautata)
                .ToList();

            var rutaNeoptimizata = Enumerable.Range(0, colectari.Count + 2).ToList();
            var matrice = IncarcaMatrice();

            var rutaOptimizata = CalculeazaRutaOptimizata(matrice);

            var distantaTraseuNeoptimizat = CalculeazaDistanta(rutaNeoptimizata, matrice);
            var distantaTraseuOptimizat = CalculeazaDistanta(rutaOptimizata, matrice);

            var distantaTraseuNeoptimizatKm = distantaTraseuNeoptimizat / 1000.0;
            var distantaTraseuOptimizatKm = distantaTraseuOptimizat / 1000.0;
            var timpDeplasare = distantaTraseuOptimizatKm / 0.5;
            var timpColectare = colectari.Count * 1;
            var timpEstimativ = timpDeplasare + timpColectare;

            var model = new RutaOptimizataViewModel
            {
                Colectari = colectari,
                ColectariOptimizate = rutaOptimizata.Where(index => index > 0 && index <= colectari.Count)
                                        .Select(index => colectari[index - 1])
                                        .ToList(),
                DistantaTraseuNeoptimizat = distantaTraseuNeoptimizatKm,
                DistantaTraseuOptimizat = distantaTraseuOptimizatKm,
                DiferentaDistanta = distantaTraseuNeoptimizatKm - distantaTraseuOptimizatKm,
                TimpEstimativ = timpEstimativ
            };

            return View(model);
        }

        private int[,] IncarcaMatrice()
        {
            var caleFisier = "matrice.xlsx"; 
            using var workbook = new XLWorkbook(caleFisier);
            var ws = workbook.Worksheet("Matrix");
            int nrLinii = ws.LastRowUsed().RowNumber();
            int nrColoane = ws.LastColumnUsed().ColumnNumber();
            var matrice = new int[nrLinii, nrColoane];

            for (int i = 2; i <= nrLinii; i++) 
            {
                for (int j = 2; j <= nrColoane; j++) 
                {
                    var cellValue = ws.Cell(i, j).GetValue<double>();
                    matrice[i - 2, j - 2] = (int)Math.Round(cellValue);
                }
            }

            return matrice;
        }

        private int CalculeazaDistanta(List<int> ordine, int[,] matrice)
        {
            int distanta = 0;

            for (int i = 0; i < ordine.Count - 1; i++)
            {
                int from = ordine[i];
                int to = ordine[i + 1];
                distanta += matrice[from, to];
            }
            return distanta;
        }

        private List<int> CalculeazaRutaOptimizata(int[,] matrice)
        {
            int n = matrice.GetLength(0);
            bool[] vizitat = new bool[n];
            List<int> ruta = new List<int>();
            int current = 0;
            ruta.Add(current);
            vizitat[current] = true;

            for (int i = 1; i < n; i++)
            {
                int next = -1;
                int minDist = int.MaxValue;

                for (int j = 0; j < n; j++)
                {
                    if (!vizitat[j] && matrice[current, j] < minDist)
                    {
                        minDist = matrice[current, j];
                        next = j;
                    }
                }

                if (next != -1)
                {
                    ruta.Add(next);
                    vizitat[next] = true;
                    current = next;
                }
            }
            return ruta;
        }
    }
}
