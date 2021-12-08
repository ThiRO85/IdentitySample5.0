using ISystem.Application.Interfaces;
using ISystem.Application.Methods;
using ISystem.Domain.Entities.WizardOn;
using ISystem.Infrastructure.Contexts;
using ISystem.Infrastructure.Methods;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISystem.WebUI.Controllers
{
    public class WizardOnController : Controller
    {
        private readonly IWizardOnService _wizardOnService;

        public WizardOnController(IWizardOnService wizardOnService)
        {
            _wizardOnService = wizardOnService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Index(string nome, string telefone1, string cpf, string email)
        //{
        //    var userid = User.Identity.GetUserId();
        //    bool logado = Novax.Login(userid);
        //    if (!logado)
        //    {
        //        ModelState.AddModelError("", "Falha no Login Novax");
        //    }
        //    var lista = await _wizardOnService.Index(nome, telefone1, cpf, email);

        //    return View(lista);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NovoCliente(ClienteWizardOn cliente)
        {
            if (!ModelState.IsValid)
                return View("Index");

            await _wizardOnService.NovoCliente(cliente);

            return RedirectToAction("CriarOc", new { id = cliente.Id });
        }

        [NonAction]
        public async Task<List<EventoWizardOn>> RegraRenitencia(EventoWizardOn evento, bool reprocessando)
        {
            List<EventoWizardOn> listaEvento = await _wizardOnService.RegraRenitencia(evento, reprocessando);
            return listaEvento;
        }
    }
}
