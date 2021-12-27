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

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Movimentar(OcorrenciaWizard02ViewPesquisa valores, string ocorrencaIdList)
        {
            foreach (var item in ocorrencaIdList.Replace(";", "\n").Replace("\r", "").Split('\n'))
            {
                bool result = int.TryParse(item, out int i);
                if (result)
                {
                    valores.OcorrenciasId.Add(i);
                }
            }

            var filas = await _wizard02Service.MovimentarFilasGet();
            var campanhas = await _wizard02Service.MovimentarCampanhasGet();
            //ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            //ViewBag.CampanhaId = new SelectList(campanhas, "Id", "Nome");

            if ((valores.DtCriacaoFim == null && valores.DtCriacaoInicio != null) || ((valores.DtCriacaoInicio == null && valores.DtCriacaoFim != null)))
            {
                ModelState.AddModelError(string.Empty, @"Data Início de Criação ou Fim não está preenchida. Preencha as duas ou nenhuma para seguir");
            }
            else if (valores.DtCriacaoInicio != null && valores.DtCriacaoFim != null)
            {
                if (valores.DtCriacaoInicio > valores.DtCriacaoFim)
                {
                    ModelState.AddModelError(string.Empty, @"Data Início de Criação maior que a Data Fim ");
                }
            }

            if ((valores.DtMovimentacaoInicio == null && valores.DtMovimentacaoFim != null) || ((valores.DtMovimentacaoFim == null && valores.DtMovimentacaoInicio != null)))
            {
                ModelState.AddModelError(string.Empty, @"Data Início de Movimentação ou Fim não esta preenchida. Preencha as duas ou nenhuma para seguir");
            }
            else if (valores.DtMovimentacaoInicio != null && valores.DtMovimentacaoFim != null)
            {
                if (valores.DtMovimentacaoInicio > valores.DtMovimentacaoFim)
                {
                    ModelState.AddModelError(string.Empty, @"Data Início de Movimentação maior que a Data Fim ");
                }
            }

            if ((valores.OcorrenciasId.Count == 0 || valores.OcorrenciasId == null)
                && valores.DtCriacaoInicio == null
                && valores.DtMovimentacaoInicio == null
                && valores.FilaId == null
                && valores.CampanhaId == null)
            {
                ModelState.AddModelError(string.Empty, @"Campos de pesquisa insuficientes para seguir");
            }

            if (ModelState.IsValid)
            {
                var ocorrencias = await _wizard02Service.Movimentar(valores);
                return View(ocorrencias);
            }
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditOcorrencia(string ocorrenciaList, string comentario, int? filaId, int? classificacaoId)
        {
            string erros = await _wizard02Service.EditOcorrencia(ocorrenciaList, comentario, filaId, classificacaoId);
            return Json(erros);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Fila()
        {
            var filas = _wizard02Service.Fila();
            return View(filas);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> FilaCreate()
        {
            var grupoProcesso = await _wizard02Service.FilaCreateGet();
            //ViewBag.GrupoWizard02Id = new SelectList(grupoProcesso, "Id", "Nome");
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilaCreate(FilaWizard02 fila)
        {
            if (ModelState.IsValid)
            {
                await _wizard02Service.FilaCreate(fila);
                return RedirectToAction("Fila");
            }
            var grupoProcesso = await _wizard02Service.FilaCreateGet();
            //ViewBag.GrupoWizard02Id = new SelectList(grupoProcesso, "Id", "Nome", fila.GrupoWizard02Id);
            return View(fila);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> FilaEdit(int? id)
        {
            if (id == null)
            {
                return NotFound(); //Atenção! Retorna um StatusCode originalmente.
            }
            FilaWizard02 fila = await _wizard02Service.FilaEditGet(id);
            if (fila == null)
            {
                return NotFound();
            }
            var grupoProcesso = await _wizard02Service.FilaCreateGet();
            //ViewBag.GrupoWizard02Id = new SelectList(grupoProcesso, "Id", "Nome", fila.GrupoWizard02Id);
            return View(fila);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilaEdit(FilaWizard02 fila)
        {
            if (ModelState.IsValid)
            {
                await _wizard02Service.FilaEdit(fila);
                return RedirectToAction("Fila");
            }
            var grupoProcesso = await _wizard02Service.FilaCreateGet();
            //ViewBag.GrupoWizard02Id = new SelectList(grupoProcesso, "Id", "Nome", fila.GrupoWizard02Id);
            return View(fila);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> GrupoProcesso()
        {
            var grupoProcesso = await _wizard02Service.FilaCreateGet();
            return View(grupoProcesso);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GrupoProcessoCreate()
        {
            var grupoProcesso = await _wizard02Service.GrupoProcessoCreateGet();
            return View(grupoProcesso);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> GrupoProcessoCreate(GrupoProcessoWizard02 grupoProcesso, params string[] classificacao)
        {
            if (ModelState.IsValid)
            {
                await _wizard02Service.GrupoProcessoCreate(grupoProcesso, classificacao);
                return RedirectToAction("GrupoProcesso");
            }
            var classificacoesII = await _wizard02Service.GrupoProcessoCreateII();
            return View(new GrupoProcessoWizard02()
            /*{
                Nome = grupoProcesso.Nome,
                ClassificacaoList = classificacoesII.ToList().OrderBy(o => o.ClassificacaoView).Select(x => new SelectListItem()
                {
                    Selected = grupoProcesso.ClassificacaoWizard02.Any(a => a.Id == x.Id),
                    Text = x.ClassificacaoView,
                    Value = x.Id.ToString()
                }),
            }*/);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GrupoProcessoEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var grupo = await _wizard02Service.GrupoProcessoEditGet(id);
            if (grupo == null)
            {
                return NotFound();
            }
            var classificacoesII = await _wizard02Service.GrupoProcessoCreateII();
            return View(new GrupoProcessoWizard02()
            /*{
                Id = grupo.Id,
                Nome = grupoProcesso.Nome,
                ClassificacaoList = classificacoesII.ToList().OrderBy(o => o.ClassificacaoView).Select(x => new SelectListItem()
                {
                    Selected = grupoProcesso.ClassificacaoWizard02.Any(a => a.Id == x.Id),
                    Text = x.ClassificacaoView,
                    Value = x.Id.ToString()
                }),
            }*/);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> GrupoProcessoEdit(GrupoProcessoWizard02 grupoProcesso, params string[] classificacao)
        {
            var grupo = await _wizard02Service.GrupoProcessoEditGet(grupoProcesso.Id);

            if (ModelState.IsValid)
            {
                grupo.Nome = grupoProcesso.Nome;
                await _wizard02Service.GrupoProcessoEdit(grupoProcesso, classificacao);
                return RedirectToAction("GrupoProcesso");
            }
            var classificacoesII = await _wizard02Service.GrupoProcessoCreateII();
            return View(new GrupoProcessoWizard02()
            /*{
                Id = grupo.Id,
                Nome = grupo.Nome,
                ClassificacaoList = classificacoesII.ToList().OrderBy(o => o.ClassificacaoView).Select(x => new SelectListItem()
                {
                    Selected = grupo.ClassificacaoWizard02.Any(a => a.Id == x.Id),
                    Text = x.ClassificacaoView,
                    Value = x.Id.ToString()
                }),
            }*/);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Classificacao()
        {
            var classificacoesII = await _wizard02Service.GrupoProcessoCreateII();
            var classificacoes = classificacoesII.OrderBy(o => o.ClassificacaoView);
            return View(classificacoes);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ClassificacaoCreate()
        {
            var classificacoesII = await _wizard02Service.GrupoProcessoCreateII();
            //ViewBag.ClassificacaoPaiId = new SelectList(classificacoesII.OrderBy(o => o.ClassificacaoView), "Id", "ClassificacaoView");
            var filas = await _wizard02Service.MovimentarFilasGet();
            //ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            var status = await _wizard02Service.ClassificacaoCreateGet();
            //ViewBag.StatusId = new SelectList(status, "Id", "Nome");
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> ClassificacaoCreate(ClassificacaoWizard02 classificacao)
        {
            if (classificacao.StatusId == null || classificacao.StatusId == 0)
            {
                ModelState.AddModelError("", "Status é obrigatório!");
            }
            if (ModelState.IsValid)
            {
                await _wizard02Service.ClassificacaoCreate(classificacao);
                return RedirectToAction("Classificacao");
            }
            var classificacoesII = await _wizard02Service.GrupoProcessoCreateII();
            //ViewBag.ClassificacaoPaiId = new SelectList(classificacoesII.OrderBy(o => o.ClassificacaoView), "Id", "ClassificacaoView");
            var filas = await _wizard02Service.MovimentarFilasGet();
            //ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            return View(classificacao);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ClassificacaoEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ClassificacaoWizard02 classificacao = await _wizard02Service.ClassificacaoEditGet(id);
            if (classificacao == null)
            {
                return NotFound();
            }
            var classificacoesII = await _wizard02Service.GrupoProcessoCreateII();
            //ViewBag.ClassificacaoPaiId = new SelectList(classificacoesII.OrderBy(o => o.ClassificacaoView), "Id", "ClassificacaoView");
            var filas = await _wizard02Service.MovimentarFilasGet();
            //ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            var status = await _wizard02Service.ClassificacaoCreateGet();
            //ViewBag.StatusId = new SelectList(status, "Id", "Nome");
            return View(classificacao);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> ClassificacaoEdit(ClassificacaoWizard02 classificacao)
        {
            if (classificacao.StatusId == null || classificacao.StatusId == 0)
            {
                ModelState.AddModelError("", "Status é obrigatório!");
            }
            if (ModelState.IsValid)
            {
                await _wizard02Service.ClassificacaoEdit(classificacao);
                return RedirectToAction("Classificacao");
            }
            var classificacoesII = await _wizard02Service.GrupoProcessoCreateII();
            //ViewBag.ClassificacaoPaiId = new SelectList(classificacoesII.OrderBy(o => o.ClassificacaoView), "Id", "ClassificacaoView");
            var filas = await _wizard02Service.MovimentarFilasGet();
            //ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            var status = await _wizard02Service.ClassificacaoCreateGet();
            //ViewBag.StatusId = new SelectList(status, "Id", "Nome");
            return View(classificacao);
        }
    }
}
