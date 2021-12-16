using ISystem.Application.Interfaces;
using ISystem.Domain.Entities.Wizard02;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISystem.WebUI.Controllers
{
    public class Wizard02Controller : Controller
    {
        private readonly IWizard02Service _wizard02Service;

        public Wizard02Controller(IWizard02Service wizard02Service)
        {
            _wizard02Service = wizard02Service;
        }

        //public IActionResult Index()
        //{
        //    var userid = User.Identity.GetUserId();
        //    bool logado = Novax.Login(userid);
        //    if (!logado)
        //    {
        //        ModelState.AddModelError("", "Falha no Login Novax");
        //    }
        //    if (TempData["Message"] != null)
        //    {
        //        ModelState.AddModelError(string.Empty, TempData["Message"].ToString());
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(string nome, string telefone1, string cpf, string email)
        //{
        //    var userid = User.Identity.GetUserId();
        //    bool logado = Novax.Login(userid);
        //    if (!logado)
        //    {
        //        ModelState.AddModelError("", "Falha no Login Novax");
        //    }
        //    var lista = await _wizard02Service.Index(nome, telefone1, cpf, email);

        //    return View(lista);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoCliente(ClienteWizard02 cliente)
        {
            if (!ModelState.IsValid)
                return View("Index");

            await _wizard02Service.NovoCliente(cliente);
            return RedirectToAction("CriarOc", new { id = cliente.Id });
        }

        [NonAction]
        public async Task<List<EventoWizard02>> RegraRenitencia(EventoWizard02 evento, bool reprocessando)
        {
            List<EventoWizard02> listaEvento = await _wizard02Service.RegraRenitencia(evento, reprocessando);
            return listaEvento;
        }

        public async Task<IActionResult> CriarOc(int? id)
        {
            var ocorrencia = await _wizard02Service.CriarOcorrencia(id);
            return RedirectToAction("Atendimento", new { ocorrenciaId = ocorrencia.Id });
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReseteOcorrencia(int id)
        {
            var ocorrencia = await _wizard02Service.ReseteOcorrencia(id);
            return RedirectToAction("Atendimento", new { ocorrenciaId = id });
        }
    }
}
