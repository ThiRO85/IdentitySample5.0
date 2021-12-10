using Microsoft.AspNetCore.Mvc;

namespace ISystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Limpar()
        {
            return View();
        }

        //public IActionResult SignOut()
        //{
        //    ViewBag.Message = "Sair";
        //    return View();
        //}
    }
}
