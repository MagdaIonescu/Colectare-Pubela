using colectare_date.Data;
using colectare_date.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_date.Controllers
{
    public class PubelaCetateniController : Controller
    {
        private readonly AppDbContext context;

        public PubelaCetateniController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AtribuirePubela()
        {
            if (HttpContext.Session.GetString("admin") != "true")
                return RedirectToAction("Index", "Login");

            var cetateni = await context.Cetateni.ToListAsync();

            var pubeleAtribuite = await context.PubeleCetateni
                .Select(p => p.PubelaId)
                .ToListAsync();

            var pubeleDisponibile = await context.Pubele
                .Where(p => !pubeleAtribuite.Contains(p.PubelaId))
                .ToListAsync();

            if (!pubeleDisponibile.Any())
            {
                TempData["ErrorMessage"] = "Nu exista pubele disponibile pentru atribuire.";
                return RedirectToAction("ListaCetatenilor", "Cetatean");
            }

            ViewBag.Cetateni = cetateni.Select(c => new
            {
                Id = c.Id,
                NumeComplet = c.Nume + " " + c.Prenume
            }).ToList();

            ViewBag.Pubele = pubeleDisponibile;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AtribuirePubela(PubelaCetateni model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Datele nu sunt valide!";
            }

            if (ModelState.IsValid)
            {
                context.PubeleCetateni.Add(model);
                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pubela a fost atribuita cu succes!";
                return RedirectToAction("ListaCetatenilor", "Cetatean");
            }

            var cetateni = await context.Cetateni.ToListAsync();
            var pubeleAtribuite = await context.PubeleCetateni.Select(p => p.PubelaId).ToListAsync();

            ViewBag.Cetateni = cetateni.Select(c => new
            {
                Id = c.Id,
                NumeComplet = c.Nume + " " + c.Prenume
            }).ToList();

            ViewBag.Pubele = await context.Pubele
                .Where(p => !pubeleAtribuite.Contains(p.PubelaId))
                .ToListAsync();

            return View(model);
        }
    }
}
