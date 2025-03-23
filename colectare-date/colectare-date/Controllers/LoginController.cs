using Microsoft.AspNetCore.Mvc;

namespace colectare_date.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string parola)
        {
            if (email == "admin@proiect.ro" && parola == "admin123")
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
