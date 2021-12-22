using ISystem.Application.Interfaces;
using ISystem.Domain.Entities;
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
            {
                return View("Index");
            }              
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

        public async Task<IActionResult> Roleta()
        {
            var ocorrencia = await _wizard02Service.Roleta();

            if (ocorrencia == null)
            {
                TempData["Message"] = "Sem ocorrência pendente em suas filas";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Atendimento", new { ocorrenciaId = ocorrencia.Id });
        }

        //public async Task<JsonResult> GetFila(int id)
        //{
        //    var listar = await _wizard02Service.GetFila(id);
        //    var fila = new List<object>
        //    {
        //        new
        //        {
        //            listar.Id,
        //            listar.Nome,
        //            listar.DtCriacao,
        //            QtdUsuario = listar.Users.Count,
        //            Status = listar.Ativo
        //        }
        //    };

        //    var usuario = new List<object>();

        //    foreach (var item in listar.Users)
        //    {
        //        usuario.Add(new
        //        {
        //            item.Id,
        //            item.Nome,
        //            item.UserName
        //        });
        //    }
        //    return Json(new { fila, usuario }, JsonRequestBehavior.AllowGet);
        //}

        //[Authorize(Roles = "Admin")]
        //public async Task<JsonResult> GetFilaUsuario(int id)
        //{
        //    List<object> lista = new List<object>
        //    {
        //        new
        //        {
        //            usuarios = await _wizard02Service.GetFilaUsuario(id)
        //        }
        //    };
        //    return Json(new { lista }, JsonRequestBehavior.AllowGet);
        //}

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> FilaEditUsuario(string userIdList, int id, bool removerUsuario)
        {
            string teste = await _wizard02Service.FilaEditUsuario(userIdList, id, removerUsuario);
            return Json(teste);
        }

        [HttpPost]
        public async Task<JsonResult> SetIndicacao(int? clienteId, string nome, string telefone, string email)
        {
            bool sucesso = false;

            if (clienteId == null)
            {
                ModelState.AddModelError("", "Cliente não encontrado");
            }

            var indicadoPor = await _wizard02Service.IndicadoPor(clienteId);

            if (indicadoPor == null)
            {
                ModelState.AddModelError("", "Cliente não encontrado");
            }
            if (string.IsNullOrWhiteSpace(nome))
            {
                ModelState.AddModelError("", "Informe um nome para indicar");
            }
            if (ModelState.IsValid)
            {
                await _wizard02Service.SetIndicacao(indicadoPor, nome, telefone, email);
                sucesso = true;
            }
            return Json(sucesso);
        }

        //public async Task<JsonResult> GetRegra(int classificacao)
        //{            
        //    var agendamento = await _wizard02Service.GetRegra(classificacao);
        //    return Json(agendamento, JsonRequestBehavior.AllowGet);
        //}

        //public async Task<JsonResult> GetClassificacao(int grupo)
        //{
        //    List<ClassificacaoWizard02View> retorno = new List<ClassificacaoWizard02View>();
        //    var classificacoes = await _wizard02Service.GetClassificacao(grupo);
        //    retorno.Add(new ClassificacaoWizard02View() { id = 0, text = "-- Selecione uma opção --", disabled = false, level = 0 });
        //    retorno.AddRange(DefinirClassificacaoWizard02View(classificacoes, 0, grupo, null));
        //    return Json(new { retorno }, JsonRequestBehavior.AllowGet);
        //}

        [NonAction]
        public List<ClassificacaoWizard02View> DefinirClassificacaoWizard02View(ICollection<ClassificacaoWizard02> resultado, int level, int grupo, string pai)
        {
            List<ClassificacaoWizard02View> retorno = new List<ClassificacaoWizard02View>();

            foreach (var item in resultado)
            {
                ClassificacaoWizard02View filho = new ClassificacaoWizard02View
                {
                    id = item.Id,
                    level = level
                };
                if (!string.IsNullOrWhiteSpace(pai))
                {
                    filho.text = pai + " > " + item.Nome;
                }
                else
                {
                    filho.text = item.Nome;
                }
                if (item.Classificacoes.Where(x => x.GrupoProcessoWizard02.Any(a => a.Id == grupo)).Count() > 0)
                {
                    filho.disabled = true;
                }
                retorno.Add(filho);

                if (filho.disabled)
                {
                    retorno.AddRange(DefinirClassificacaoWizard02View(item.Classificacoes.Where(x => x.GrupoProcessoWizard02.Any(a => a.Id == grupo)).ToList(), filho.level + 1, grupo, filho.text));
                }
            }
            return retorno;
        }

        //public async Task<JsonResult> GetFilaOcorrencias()
        //{ 
        //    var query = await _wizard02Service.GetFilaOcorrencias();
        //    query.OrderByDescending(x => x.DtMovimentacao).ToList();
        //    return Json(new { query }, JsonRequestBehavior.AllowGet);
        //}

        //public async Task<JsonResult> GetClienteOcorrencias(int id)
        //{
        //    var query = await _wizard02Service.GetClienteOcorrencias(id);
        //    query.OrderBy(i => i.Prioridade).ThenByDescending(i => i.ProximoAt);
        //    return Json(new { query }, JsonRequestBehavior.AllowGet);
        //}

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Campanha()
        {
            var campanhas = await _wizard02Service.Campanha();
            return View(campanhas.OrderByDescending(x => x.Id));
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CampanhaEdit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var listaWizard02 = await _wizard02Service.GetCampanhaEdit(id);

            //if (listaWizard02 == null)
            //{
            //    return HttpNotFound();
            //}
            return View(listaWizard02);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CampanhaEdit(CampanhaWizard02 campanhaUpdate)
        {
            if (ModelState.IsValid)
            {
                await _wizard02Service.CampanhaEdit(campanhaUpdate);
                return RedirectToAction("Campanha");
            }
            return View(campanhaUpdate);
        }

        //---------------------------- Fim da Tabulação ------------------------//

        //[Authorize(Roles = "Admin")]
        public ActionResult Importar()
        {
            var listaErros = new List<Erros>();
            return View(listaErros);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CampanhaAtualizar()
        {
            await _wizard02Service.CampanhaAtualizar();
            return RedirectToAction("Campanha");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Movimentar()
        {
            var filas = await _wizard02Service.MovimentarFilasGet();
            var campanhas = await _wizard02Service.MovimentarCampanhasGet();
            //ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            //ViewBag.CampanhaId = new SelectList(campanhas, "Id", "Nome");
            return View();
        }
    }
}
