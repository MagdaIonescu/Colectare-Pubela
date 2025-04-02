using colectare_date.Data;
using Microsoft.AspNetCore.Mvc;

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
            var dataCautata = new DateTime(2024, 10, 15);
            var colectari = context.Colectari
                .Where(c => c.TimpColectare.Date == dataCautata)
                .ToList();

            return View(colectari);
        }
    }
}
