using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            ML.Materia materia = new ML.Materia();

            return View(materia);
        }
        [HttpPost]
        public IActionResult Login(ML.Materia materia)
        {

            return RedirectToAction("Home");

        }
    }
}
