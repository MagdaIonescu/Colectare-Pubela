using colectare_date.Data;
using colectare_date.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_date.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext context;
        public LoginController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string parola)
        {
            var utilizator = await context.Utilizatori.FirstOrDefaultAsync(u => u.Email == email && u.Parola == parola);

            if (utilizator != null)
            {
                HttpContext.Session.SetString("admin", "true");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Eroare = "Email sau parola gresite.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
