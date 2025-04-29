using colectare_date.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_date.Controllers
{
    public class AlerteController : Controller
    {
        private readonly AppDbContext context;

        public AlerteController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") != "true")
                return RedirectToAction("Index", "Login");

            var colectari = await context.Colectari
                .Where(c => !c.EsteRezolvata).ToListAsync();
            var atribuiri = await context.PubeleCetateni.ToListAsync();

            var alerte = new List<dynamic>();

            foreach (var colectare in colectari)
            {
                var atribuire = atribuiri.FirstOrDefault(a => a.PubelaId == colectare.PubelaId);

                if (atribuire != null && colectare.Adresa != null &&
                    colectare.Adresa.Trim() != atribuire.Adresa.Trim())
                {
                    alerte.Add(new
                    {
                        Id = colectare.Id, 
                        PubelaId = colectare.PubelaId,
                        AdresaColectata = colectare.Adresa,
                        AdresaContract = atribuire.Adresa,
                        TimpColectare = colectare.TimpColectare
                    });
                }
            }

            return View(alerte);
        }

        [HttpPost]
        public async Task<IActionResult> RezolvareAlerta(int id)
        {
            var colectare = await context.Colectari.FindAsync(id);

            if (colectare == null)
            {
                return NotFound();
            }

            colectare.EsteRezolvata = true; 
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Alerta a fost marcata ca rezolvata!";
            return RedirectToAction("Index");
        }

    }
}
