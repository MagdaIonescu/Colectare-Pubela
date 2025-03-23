using colectare_date.Data;
using colectare_date.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_date.Controllers
{
    public class CetateanController : Controller
    {
        private readonly AppDbContext context;

        public CetateanController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Adauga()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adauga(Cetatean model)
        {
            if (ModelState.IsValid)
            {
                context.Cetateni.Add(model);
                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cetateanul a fost adaugat cu succes!";
                return RedirectToAction("ListaCetatenilor");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Sterge(int id)
        {
            var cetatean = await context.Cetateni.FindAsync(id);
            if (cetatean == null)
            {
                return NotFound();
            }

            context.Cetateni.Remove(cetatean);
            await context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cetateanul a fost sters cu succes!";
            return RedirectToAction("ListaCetatenilor");
        }

        [HttpGet]
        public async Task<IActionResult> ListaCetatenilor()
        {
            var cetateni = await context.Cetateni.ToListAsync();
            return View(cetateni);
        }
    }
}