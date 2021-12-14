using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISystem.WebUI.Controllers
{
    [Authorize] //É preciso estar com os registros de autenticação e autorização devidamente registrados!
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
