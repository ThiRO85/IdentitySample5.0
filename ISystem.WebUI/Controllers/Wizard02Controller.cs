using ISystem.Application.Interfaces;
using ISystem.Application.Methods;
using ISystem.Domain.Entities;
using ISystem.Domain.Entities.Wizard02;
using ISystem.Infrastructure.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        //    var clientes = await _wizard02Service.Index(nome, telefone1, cpf, email);
        //    return View(clientes);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoCliente(ClienteWizard02 cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome)
              && string.IsNullOrWhiteSpace(cliente.Telefone1)
              && string.IsNullOrWhiteSpace(cliente.Email)
              && string.IsNullOrWhiteSpace(cliente.Cpf))
            {
                ModelState.AddModelError("", @"Preencha pelo menos um dos campos");
            }
            if (string.IsNullOrWhiteSpace(cliente.Cpf))
            {
                ModelState.AddModelError("", @"CPF é obrigatório");
            }
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                ModelState.AddModelError("", @"Email é obrigatório");
            }
            if (string.IsNullOrWhiteSpace(cliente.Telefone1))
            {
                ModelState.AddModelError("", @"Telefone é obrigatório");
            }
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
            List<EventoWizard02> eventos = await _wizard02Service.RegraRenitencia(evento, reprocessando);
            return eventos;
        }

        public async Task<IActionResult> CriarOc(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Parâmetro de Busca Nulo");
            //}

            var oc = await _wizard02Service.CriarOc(id);

            if (oc)
            {
                TempData["Message"] = "Já existe uma ocorrência pendente para este cliente, finalize-a antes de criar uma nova ou verifique se você tem acesso à fila";
                return RedirectToAction("Index");
            }

            var ocorrencia = await _wizard02Service.CriarOcorrencia(id);
            return RedirectToAction("Atendimento", new { ocorrenciaId = ocorrencia.Id });
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReseteOcorrencia(int id)
        {
            await _wizard02Service.ReseteOcorrencia(id);
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

        public async Task<IActionResult> Atendimento(int? ocorrenciaId, string telefone, string ligacaoId)
        {
            //var userId = User.Identity.GetUserId();
            var data = DateTime.Now;

            if (ocorrenciaId == null)
            {
                return NotFound();
            }

            //var userid = User.Identity.GetUserId();
            var userid = "teste"; //Paliativo pra não dar erro no método.
            var ocorrencia = await _wizard02Service.AtendimentoOcorrenciaGet(ocorrenciaId, userid);

            if (ocorrencia == null)
            {
                TempData["Message"] = "Ocorrência não localizada ou já finalizada";
                return RedirectToAction("Index");
            }
            else if (ocorrencia.ProximoAt >= data && ocorrencia.UsersId != userid)
            {
                var tempo = ocorrencia.ProximoAt - data;
                TempData["Message"] = "Ocorrência em intervalo de tabulação, restam: " + tempo.Minutes + " Minutos";
                return RedirectToAction("Index");
            }
            //else if (ocorrencia.UsersId != userId && ocorrencia.AgendamentoProprio)
            //{
            //    TempData["Message"] = "Ocorrência com Agendamento Próprio. Você não tem permissão para atuar";
            //    return RedirectToAction("Index");
            //}
            else if (!ocorrencia.Campanha.Ativo)
            {
                TempData["Message"] = "Campanha inativa";
                return RedirectToAction("Index");
            }
            else if (!ocorrencia.ClienteWizard02.Ativo)
            {
                TempData["Message"] = "Cliente inativo";
                return RedirectToAction("Index");
            }
            //else if (ocorrencia.UsersId != userId && ocorrencia.AgendamentoProprio)
            //{
            //    TempData["Message"] = "Ocorrência com Agendamento Próprio. Você não tem permissão para atuar";
            //    return RedirectToAction("Index");
            //}

            await _wizard02Service.AtendimentoRetornoGet(ocorrencia, userid);
            var retorno = ocorrencia.Eventos.OrderByDescending(x => x.Id).FirstOrDefault();
            retorno.DtAberturaEvento = DateTime.Now;
            retorno.TelefoneDiscador = telefone;
            retorno.LigacaoId = ligacaoId;
            return View(retorno);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atendimento(EventoWizard02 eventoWizard02, int? classificacaoId, bool finalizado)
        {
            var data = DateTime.Now;
            var dataMenos20 = DateTime.Now.AddMinutes(-60);

            eventoWizard02.ClienteWizard02 = await _wizard02Service.AtendimentoEventoCliente(eventoWizard02);
            eventoWizard02.Ocorrencia = await _wizard02Service.AtendimentoEventoOcorrencia(eventoWizard02);

            //var userid = User.Identity.GetUserId();

            //if (eventoWizard02.Ocorrencia != null && eventoWizard02.Ocorrencia.ProximoAt.AddMinutes(60) >= data && eventoWizard02.Ocorrencia.UsersId != userid)
            //{
            //    var tempo = eventoWizard02.Ocorrencia.ProximoAt.AddMinutes(60) - data;
            //    TempData["Message"] = "Ocorrência em intevalo de tabulação. Restam " + tempo.Minutes + " minutos";
            //    return RedirectToAction("Index");
            //}
            if (eventoWizard02.ClienteWizard02 == null)
            {
                TempData["Message"] = "Falha ao Localizar o Cliente. Não registrado";
                return RedirectToAction("Index");
            }
            if (classificacaoId == null || classificacaoId == 0)
            {
                ModelState.AddModelError(string.Empty, @"Selecione uma Classificação");
            }
            else
            {
                eventoWizard02.Classificacao = await _wizard02Service.AtendimentoEventoClassificacao(eventoWizard02, classificacaoId);

                if (eventoWizard02.Classificacao.Classificacoes.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, @"Selecione uma Classificação válida");
                }
                if (string.IsNullOrWhiteSpace(eventoWizard02.Telefone1) &&
                    string.IsNullOrWhiteSpace(eventoWizard02.Telefone2) &&
                    string.IsNullOrWhiteSpace(eventoWizard02.Telefone3) &&
                    string.IsNullOrWhiteSpace(eventoWizard02.Telefone4))
                {
                    ModelState.AddModelError(string.Empty, @"Preencha um telefone");
                }
                if (!string.IsNullOrWhiteSpace(eventoWizard02.Email))
                {
                    Regex regex = new Regex(
                            @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9_]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$");
                    Match match = regex.Match(eventoWizard02.Email);
                    if (!match.Success)
                    {
                        ModelState.AddModelError(string.Empty, @"Formato de e-mail incorreto");
                    }
                }
                if (string.IsNullOrWhiteSpace(eventoWizard02.CodCliente))
                {
                    ModelState.AddModelError(string.Empty, @"Lead Id é obrigatório");
                }
                if (eventoWizard02.Classificacao.Agendamento)
                {
                    if (eventoWizard02.DtAgendado == null)
                    {
                        ModelState.AddModelError("",
                            @"Favor Selecionar uma data para agendamento");
                    }
                    if (eventoWizard02.DtAgendado <= data)
                    {
                        ModelState.AddModelError("",
                            @"Favor Selecionar uma data para agendamento maior que a data atual");
                    }
                }
                if (string.IsNullOrWhiteSpace(eventoWizard02.Comentario))
                    ModelState.AddModelError("", @"Comentario é obrigatório");

                if (string.IsNullOrWhiteSpace(eventoWizard02.Nome))
                    ModelState.AddModelError("", @"Nome é obrigatório");

                if (string.IsNullOrWhiteSpace(eventoWizard02.Email))
                    ModelState.AddModelError("", @"Email é obrigatório");

                if (string.IsNullOrWhiteSpace(eventoWizard02.Telefone1) && string.IsNullOrWhiteSpace(eventoWizard02.Telefone2) && string.IsNullOrWhiteSpace(eventoWizard02.Telefone3) && string.IsNullOrWhiteSpace(eventoWizard02.Telefone4))
                    ModelState.AddModelError("", @"Pelo menos um telefone é obrigatório");

                int[] regra1 = new int[] { 87, 89, 90, 91, 93, 97, 98, 99, 101, 102, 103, 104, 164, 166, 168 };

                if (regra1.Any(x => x == eventoWizard02.ClassificacaoId))
                {
                    if (string.IsNullOrWhiteSpace(eventoWizard02.Cpf))
                        ModelState.AddModelError("", @"Cpf é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.Rg))
                        ModelState.AddModelError("", @"Rg é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.Unidade))
                        ModelState.AddModelError("", @"Unidade é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.MotivoInteresse))
                        ModelState.AddModelError("", @"MotivoInteresse é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.NivelIdioma))
                        ModelState.AddModelError("", @"NivelIdioma é obrigatório");

                    if (eventoWizard02.DtNascimento == null)
                        ModelState.AddModelError("", @"DtNascimento é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.Cep))
                        ModelState.AddModelError("", @"Cep é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.Logradouro))
                        ModelState.AddModelError("", @"Logradouro é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.Numero))
                        ModelState.AddModelError("", @"Numero é obrigatório");
                }

                int[] regra2 = new int[] { 87, 89, 93, 97, 98, 102, 164, 166, 168 };

                if (regra2.Any(x => x == eventoWizard02.ClassificacaoId))
                {
                    if (eventoWizard02.DtPromessa == null)
                        ModelState.AddModelError("", @"DtPromessa é obrigatório");
                }

                int[] regra3 = new int[] { 87, 89, 90, 91, 93, 97, 98, 99, 101, 102, 103, 104, 164, 166, 168 };

                if (regra3.Any(x => x == eventoWizard02.ClassificacaoId))
                {
                    if (string.IsNullOrWhiteSpace(eventoWizard02.HorarioCurso))
                        ModelState.AddModelError("", @"HorarioCurso é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.DiaSemana))
                        ModelState.AddModelError("", @"DiaSemana é obrigatório");
                }

                int[] regra4 = new int[] { 165, 167, 169 };

                if (regra4.Any(x => x == eventoWizard02.ClassificacaoId))
                {
                    if (eventoWizard02.DtAgendamentoVisita == null)
                        ModelState.AddModelError("", @"Data Agendamento da Visita é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.MotivoInteresse))
                        ModelState.AddModelError("", @"MotivoInteresse é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.NivelIdioma))
                        ModelState.AddModelError("", @"NivelIdioma é obrigatório");

                    if (string.IsNullOrWhiteSpace(eventoWizard02.Unidade))
                        ModelState.AddModelError("", @"Unidade é obrigatório");

                    if (eventoWizard02.DtNascimento == null)
                        ModelState.AddModelError("", @"DtNascimento é obrigatório");
                }
            }
            if (ModelState.IsValid)
            {
                if (eventoWizard02.Nome == null)
                {
                    eventoWizard02.Nome = "";
                }
                if (eventoWizard02.Telefone1 == null)
                {
                    eventoWizard02.Telefone1 = "";
                }
                if (eventoWizard02.Email == null)
                {
                    eventoWizard02.Email = "";
                }
                if (eventoWizard02.Cpf == null)
                {
                    eventoWizard02.Cpf = "";
                }

                var idclient = eventoWizard02.ClienteWizard02.Id;
                PropertyCopier.Copy(eventoWizard02, eventoWizard02.ClienteWizard02);
                eventoWizard02.ClienteWizard02.Id = idclient;
                eventoWizard02.DtEvento = DateTime.Now;
                //eventoWizard02.UsersId = userid;
                eventoWizard02.Ocorrencia.Finalizado = eventoWizard02.Classificacao.Finalizador;

                if (string.IsNullOrWhiteSpace(eventoWizard02.Telefone1)
                    && string.IsNullOrWhiteSpace(eventoWizard02.Telefone2)
                    && string.IsNullOrWhiteSpace(eventoWizard02.Telefone3)
                    && string.IsNullOrWhiteSpace(eventoWizard02.Telefone4))
                {
                    eventoWizard02.Ocorrencia.Finalizado = true;
                }

                eventoWizard02.Ocorrencia.Agendamento = eventoWizard02.Classificacao.Agendamento;
                eventoWizard02.Ocorrencia.AgendamentoProprio = eventoWizard02.Classificacao.AgendamentoProprio;

                if (eventoWizard02.Ocorrencia.ApiId != null)
                {
                    eventoWizard02.Ocorrencia.ModificadoApi = true;
                }
                if (eventoWizard02.Classificacao.Agendamento)
                {
                    eventoWizard02.Ocorrencia.ProximoAt = (DateTime)eventoWizard02.DtAgendado;
                }
                else
                {
                    eventoWizard02.Ocorrencia.ProximoAt = DateTime.Now.AddMinutes(eventoWizard02.Classificacao.RetornoEmMin);
                }

                if (eventoWizard02.Classificacao.FilaId != null)
                {
                    eventoWizard02.Ocorrencia.Fila = eventoWizard02.Classificacao.Fila;
                }

                eventoWizard02.Fila = eventoWizard02.Ocorrencia.Fila;
                eventoWizard02.Ocorrencia.StatusId = eventoWizard02.Classificacao.StatusId;
                eventoWizard02.Ocorrencia.Tentativas = eventoWizard02.Ocorrencia.Tentativas + 1;

                if (eventoWizard02.Classificacao.ZeraTentativa)
                {
                    eventoWizard02.Ocorrencia.Tentativas = 0;
                }
                if (eventoWizard02.Ocorrencia.Eventos.Any(a => a.ClassificacaoId == 16) || eventoWizard02.Ocorrencia.Eventos.Any(a => a.ClassificacaoId == 17) || eventoWizard02.Ocorrencia.Eventos.Any(a => a.ClassificacaoId == 41) || eventoWizard02.Ocorrencia.Eventos.Any(a => a.ClassificacaoId == 72))
                {
                    if ((eventoWizard02.Ocorrencia.Campanha.TentativasMax * 2) <= eventoWizard02.Ocorrencia.Tentativas && eventoWizard02.Ocorrencia.FilaId != 2)
                    {
                        eventoWizard02.Ocorrencia.Finalizado = true;
                    }
                }
                else if (eventoWizard02.Ocorrencia.Campanha.TentativasMax <= eventoWizard02.Ocorrencia.Tentativas && eventoWizard02.Ocorrencia.FilaId != 2)
                {
                    eventoWizard02.Ocorrencia.Finalizado = true;
                }

                List<EventoWizard02> listaEventos = await _wizard02Service.RegraRenitencia(eventoWizard02, false);
                await _wizard02Service.AtendimentoListaEventos(listaEventos);

                if (listaEventos.FirstOrDefault().Ocorrencia.Finalizado || finalizado)
                {
                    TempData["Message"] = "Registrado, Ocorrencia => " + eventoWizard02.Ocorrencia.Id;
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", @"Registrado");
            }
            foreach (var item in eventoWizard02.Ocorrencia.Eventos)
            {
                item.ClassificacaoView = "";
                var aux = item.Classificacao?.ClassificacaoPaiId;
                for ( ; ; )
                {
                    if (aux == null)
                    {
                        break;
                    }
                    var pai = await _wizard02Service.AtendimentoPai(aux);
                    item.ClassificacaoView = pai.Nome + " > " + item.ClassificacaoView;
                    aux = pai.ClassificacaoPaiId;
                }
                item.ClassificacaoView += item.Classificacao?.Nome;
            }
            return View(eventoWizard02);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Controle()
        {
            var filas = await _wizard02Service.MovimentarFilasGet();
            ViewBag.Filas = new SelectList(filas, "Id", "Nome");
            return View();
        }

        public async Task<JsonResult> GetFila(int id)
        {
            var listar = await _wizard02Service.GetFila(id);
            var fila = new List<object>
            {
                new
                {
                    listar.Id,
                    listar.Nome,
                    listar.DtCriacao,
                    QtdUsuario = listar.Users.Count,
                    Status = listar.Ativo
                }
            };

            var usuario = new List<object>();

            //foreach (var item in listar.Users)
            //{
            //    usuario.Add(new
            //    {
            //        item.Id,
            //        item.Nome,
            //        item.UserName
            //    });
            //}
            return Json(new { fila, usuario }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        //[Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetFilaUsuario(int id)
        {
            List<object> lista = new List<object>
            {
                new
                {
                    usuarios = await _wizard02Service.GetFilaUsuario(id)
                }
            };
            return Json(new { lista }, new Newtonsoft.Json.JsonSerializerSettings());
        }

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

        public async Task<JsonResult> GetRegra(int classificacao)
        {
            var agendamento = await _wizard02Service.GetRegra(classificacao);
            return Json(agendamento, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<JsonResult> GetClassificacao(int grupo)
        {
            List<ClassificacaoWizard02View> retorno = new List<ClassificacaoWizard02View>();
            var classificacoes = await _wizard02Service.GetClassificacao(grupo);
            retorno.Add(new ClassificacaoWizard02View() { id = 0, text = "-- Selecione uma opção --", disabled = false, level = 0 });
            retorno.AddRange(DefinirClassificacaoWizard02View(classificacoes, 0, grupo, null));
            return Json(new { retorno }, new Newtonsoft.Json.JsonSerializerSettings());
        }

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

        public async Task<JsonResult> GetFilaOcorrencias()
        {
            var query = await _wizard02Service.GetFilaOcorrencias();
            query.OrderByDescending(x => x.DtMovimentacao).ToList();
            return Json(new { query }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<JsonResult> GetClienteOcorrencias(int id)
        {
            var query = await _wizard02Service.GetClienteOcorrencias(id);
            query.OrderBy(i => i.Prioridade).ThenByDescending(i => i.ProximoAt);
            return Json(new { query }, new Newtonsoft.Json.JsonSerializerSettings()); //return Json(new { query }, JsonRequestBehavior.AllowGet);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Campanha()
        {
            var campanhas = await _wizard02Service.Campanha();
            return View(campanhas.OrderByDescending(x => x.Id));
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CampanhaEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaWizard02 = await _wizard02Service.GetCampanhaEdit(id);

            if (listaWizard02 == null)
            {
                return NotFound();
            }
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
            ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            ViewBag.CampanhaId = new SelectList(campanhas, "Id", "Nome");
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
            ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            ViewBag.CampanhaId = new SelectList(campanhas, "Id", "Nome");

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
            var filas = await _wizard02Service.Fila();
            return View(filas);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> FilaCreate()
        {
            var grupoProcesso = await _wizard02Service.FilaCreateGet();
            ViewBag.GrupoWizard02Id = new SelectList(grupoProcesso, "Id", "Nome");
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
            ViewBag.GrupoWizard02Id = new SelectList(grupoProcesso, "Id", "Nome", fila.GrupoWizard02Id);
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
            ViewBag.GrupoWizard02Id = new SelectList(grupoProcesso, "Id", "Nome", fila.GrupoWizard02Id);
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
            ViewBag.GrupoWizard02Id = new SelectList(grupoProcesso, "Id", "Nome", fila.GrupoWizard02Id);
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
            ViewBag.ClassificacaoPaiId = new SelectList(classificacoesII.OrderBy(o => o.ClassificacaoView), "Id", "ClassificacaoView");
            var filas = await _wizard02Service.MovimentarFilasGet();
            ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            var status = await _wizard02Service.ClassificacaoCreateGet();
            ViewBag.StatusId = new SelectList(status, "Id", "Nome");
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
            ViewBag.ClassificacaoPaiId = new SelectList(classificacoesII.OrderBy(o => o.ClassificacaoView), "Id", "ClassificacaoView");
            var filas = await _wizard02Service.MovimentarFilasGet();
            ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
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
            ViewBag.ClassificacaoPaiId = new SelectList(classificacoesII.OrderBy(o => o.ClassificacaoView), "Id", "ClassificacaoView");
            var filas = await _wizard02Service.MovimentarFilasGet();
            ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            var status = await _wizard02Service.ClassificacaoCreateGet();
            ViewBag.StatusId = new SelectList(status, "Id", "Nome");
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
            ViewBag.ClassificacaoPaiId = new SelectList(classificacoesII.OrderBy(o => o.ClassificacaoView), "Id", "ClassificacaoView");
            var filas = await _wizard02Service.MovimentarFilasGet();
            ViewBag.FilaId = new SelectList(filas, "Id", "Nome");
            var status = await _wizard02Service.ClassificacaoCreateGet();
            ViewBag.StatusId = new SelectList(status, "Id", "Nome");
            return View(classificacao);
        }
    }
}
