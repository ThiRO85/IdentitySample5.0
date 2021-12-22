using ISystem.Application.Methods;
using ISystem.Domain.Entities.Wizard02;
using ISystem.Domain.Interfaces;
using ISystem.Domain.Validation;
using ISystem.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace ISystem.Infrastructure.Repositories
{
    public class Wizard02Repository : IWizard02Repository
    {
        private readonly ApplicationDbContext _context;

        public Wizard02Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteWizard02>> IndexAsync(string nome, string telefone1, string cpf, string email)
        {
            var lista = await _context.ClienteWizard02.Where(x =>
                        (x.Nome.Contains(nome) || nome == null)
                        && (x.Telefone1.Contains(telefone1) || x.Telefone1 == null)
                        && (x.Telefone2.Contains(telefone1) || x.Telefone2 == null)
                        && (x.Telefone3.Contains(telefone1) || x.Telefone3 == null)
                        && (x.Telefone4.Contains(telefone1) || x.Telefone4 == null)
                        && x.Email.Contains(email)
                        && x.Ativo
                        && x.Cpf.Contains(cpf)).ToListAsync();

            lista.Take(10);
            return lista;
        }

        public async Task<ClienteWizard02> NovoClienteAsync(ClienteWizard02 cliente)
        {
            //ModelStateDictionary model = new ModelStateDictionary(); //Atenção!

            if (cliente.Nome == null)
            {
                cliente.Nome = "";
            }
            if (cliente.Telefone1 == null)
            {
                cliente.Telefone1 = "";
            }
            if (cliente.Email == null)
            {
                cliente.Email = "";
            }
            if (cliente.Cpf == null)
            {
                cliente.Cpf = "";
            }
            if (string.IsNullOrWhiteSpace(cliente.Nome)
              && string.IsNullOrWhiteSpace(cliente.Telefone1)
              && string.IsNullOrWhiteSpace(cliente.Email)
              && string.IsNullOrWhiteSpace(cliente.Cpf))
            {
                throw new DomainExceptionValidation("Preencha pelo menos um dos campos");
                //model.AddModelError("", @"Preencha pelo menos um dos campos");
            }
            if (string.IsNullOrWhiteSpace(cliente.Cpf))
            {
                throw new DomainExceptionValidation("CPF é obrigatório");
            }
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                throw new DomainExceptionValidation("Email é obrigatório");
            }
            if (string.IsNullOrWhiteSpace(cliente.Telefone1))
            {
                throw new DomainExceptionValidation("Telefone é obrigatório");
            }

            cliente.DtCriacao = DateTime.Now;
            await _context.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<List<EventoWizard02>> RegraRenitenciaAsync(EventoWizard02 evento, bool reprocessando)
        {
            RegraRenitenciaWizard02 regra = await _context.RegraRenitenciaWizard02
                .Where(x => x.Ativo
                && (evento.Ocorrencia.Tentativas == x.Tentativa)
                && evento.Ocorrencia.FilaId == x.ConsiderarFilaId).FirstOrDefaultAsync();

            if (regra == null)
                regra = await _context.RegraRenitenciaWizard02
               .Where(x => x.Ativo && evento.Ocorrencia.FilaId == x.ConsiderarFilaId)
               .OrderByDescending(o => o.Id)
               .FirstOrDefaultAsync();

            List<EventoWizard02> listaEvento = new List<EventoWizard02>();
            if (regra != null)
            {
                if (!reprocessando)
                    evento.Ocorrencia.ProximoAt = DateTime.Now.AddMinutes(regra.IntervaloRetorno);
                listaEvento.Add(evento);

                if (regra.EnviarParaClassificacaoId != null
                    && !evento.Ocorrencia.Finalizado
                    && (!evento.Ocorrencia.Agendamento || evento.Ocorrencia.Agendamento == regra.ConsiderarAgendamento))
                {
                    var newObject = new EventoWizard02();
                    PropertyCopier.Copy(evento, newObject);
                    newObject.Id = 0;
                    newObject.Ocorrencia = evento.Ocorrencia;
                    newObject.ClienteWizard02 = evento.ClienteWizard02;
                    newObject.Classificacao = await _context.ClassificacaoWizard02.FirstOrDefaultAsync(x => x.Id == regra.EnviarParaClassificacaoId);
                    newObject.Ocorrencia.Finalizado = newObject.Classificacao.Finalizador;
                    newObject.Ocorrencia.StatusId = newObject.Classificacao.StatusId;
                    listaEvento.Add(newObject);
                }
            }
            else
                listaEvento.Add(evento);

            return listaEvento;
        }

        public async Task<OcorrenciaWizard02> CriarOcorrenciaAsync(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Parâmetro de Busca Nulo");
            //}
            //if (_db.OcorrenciaWizardOn.Any(aa => aa.Finalizado == false && aa.FilaId == 1 && aa.ClienteWizardOnId == id))
            //{
            //    TempData["Message"] = "Já existe uma ocorrência pendente para este cliente, finalize-a antes de criar uma nova ou verifique se você tem acesso à fila";
            //    return RedirectToAction("Index");
            //}

            var ocorrencia = new OcorrenciaWizard02();

            ocorrencia.FilaId = 1; // Fila Padrão
            ocorrencia.ClienteWizard02Id = (int)id;
            ocorrencia.ProximoAt = DateTime.Now.AddMinutes(60);
            ocorrencia.Finalizado = false;
            //ocorrencia.UsersId = User.Identity.GetUserId();
            ocorrencia.CampanhaId = 1;

            var cliente = await _context.ClienteWizard02.FirstOrDefaultAsync(x => x.Id == id);

            var evento = new EventoWizard02();

            PropertyCopier.Copy(cliente, evento);

            evento.ClienteWizard02 = cliente;
            //evento.UsersId = User.Identity.GetUserId();
            evento.FilaId = ocorrencia.FilaId;

            ocorrencia.Eventos.Add(evento);
            await _context.OcorrenciaWizard02.AddAsync(ocorrencia);
            await _context.SaveChangesAsync();
            return ocorrencia;
        }

        public async Task<OcorrenciaWizard02> ReseteOcorrenciaAsync(int id)
        {
            var ocorrencia = await _context.OcorrenciaWizard02.FindAsync(id);
            if (!ocorrencia.Finalizado)
            {
                ocorrencia.ProximoAt = DateTime.Now;
                ocorrencia.Agendamento = false;
                ocorrencia.AgendamentoProprio = false;
                await _context.SaveChangesAsync();
            }
            return ocorrencia;
        }

        public async Task<OcorrenciaWizard02> RoletaAsync()
        {
            //var userId = User.Identity.GetUserId(); //Atenção!
            var data = DateTime.Now;
            var ocorrencia = await _context.OcorrenciaWizard02
                .Include(i => i.Campanha)
                .Include(i => i.ClienteWizard02)
                .Include(i => i.Status)
                .Where(x => x.Finalizado == false
                   //&& ((x.AgendamentoProprio && x.UsersId == userId) || x.AgendamentoProprio == false)
                   && x.ProximoAt <= data
                   //&& x.Fila.Users.Any(a => a.Id == userId) //Atenção!
                   && x.Campanha.Ativo
                   && x.ClienteWizard02.Ativo)
                .OrderBy(x => x.Campanha.Prioridade)
                .ThenBy(x => x.Status.Prioridade)
                .ThenBy(x => x.Eventos.Count)
                .ThenByDescending(x => x.ProximoAt)
                .FirstOrDefaultAsync();

            return ocorrencia;
        }

        public async Task<FilaWizard02> GetFilaAsync(int id)
        {
            var listar = await _context.FilaWizard02.FirstAsync(f => f.Id == id);
            return listar;
        }

        public async Task<List<object>> GetFilaUsuarioAsync(int id)
        {
            //var usuarios = await _context.Users.Where(x => x.Roles.AnyAsync(u => u.RoleId == "1d54255e-c988-42c0-98cb-ae8832d09ae4")
            //&& x.Ativo
            //&& (x.FilaWizard02.Any(o => o.Id != filaid) || x.FilaWizard02.Count == 0))
            //    .Select(p => new
            //    {
            //        p.UserName,
            //        p.Id
            //    })
            //    .OrderBy(p => p.UserName)
            //    .ToListAsync();

            //return usuarios;
            return new List<object>();
        }

        public async Task<string> FilaEditUsuarioAsync(string userIdList, int id, bool removerUsuario)
        {
            var objetoJson = JsonConvert.DeserializeObject(userIdList);
            string userid;
            int adc = 0;
            int jfila = 0;

            //foreach (var item in (IEnumerable)objetoJson)
            //{
            //    userid = item.ToString();

            //    var valor = await _context.FilaWizard02.Include(u => u.Users).FirstAsync(x => x.Id == id);
            //    var user = await _context.Users.FindAsync(userid);
            //    var contem = valor.Users.FirstOrDefault(x => x.Id == userid);

            //    if (removerUsuario)
            //    {
            //        if (contem == null)
            //        {
            //            return Json("Usuário não pertence à fila!");
            //        }
            //        valor.Users.Remove(user);
            //        _context.SaveChanges();
            //        return Json("Usuário removido!");
            //    }
            //    if (contem != null)
            //    {
            //        jfila += 1;
            //    }
            //    else
            //    {
            //        valor.Users.Add(user);
            //        _context.SaveChanges();
            //        adc += 1;
            //    }
            //}
            string a = jfila.ToString();
            string b = adc.ToString();
            string c = b + " Usuários Adicionados" + a + " Usuários já na Fila";
            return c;
        }

        public async Task<ClienteWizard02> IndicadoPorAsync(int? clienteId)
        {
            var indicadoPor = await _context.ClienteWizard02.FirstOrDefaultAsync(x => x.Id == clienteId);
            return indicadoPor;
        }

        public async Task SetIndicacaoAsync(ClienteWizard02 indicadoPor, string nome, string telefone, string email)
        {
            var localizado = await _context.ClienteWizard02.FirstOrDefaultAsync(x => x.Email == email);

            if (localizado != null && !string.IsNullOrWhiteSpace(email))
            {
                OcorrenciaWizard02 ocorrencia = new OcorrenciaWizard02
                {
                    ClienteWizard02Id = localizado.Id,
                    CampanhaId = 3,
                    FilaId = 3
                };

                if (string.IsNullOrWhiteSpace(localizado.Telefone1))
                {
                    localizado.Telefone1 = telefone;
                }
                else if (string.IsNullOrWhiteSpace(localizado.Telefone2))
                {
                    localizado.Telefone2 = telefone;
                }
                else if (string.IsNullOrWhiteSpace(localizado.Telefone3))
                {
                    localizado.Telefone3 = telefone;
                }
                else if (string.IsNullOrWhiteSpace(localizado.Telefone4))
                {
                    localizado.Telefone4 = telefone;
                }

                var evento = new EventoWizard02();
                PropertyCopier.Copy(localizado, evento);
                evento.ClienteWizard02 = localizado;
                //evento.UsersId = User.Identity.GetUserId();
                evento.FilaId = ocorrencia.FilaId;
                ocorrencia.AgendamentoProprio = true;
                ocorrencia.UsersId = evento.UsersId;
                ocorrencia.Eventos.Add(evento);
                await _context.OcorrenciaWizard02.AddAsync(ocorrencia);
                await _context.SaveChangesAsync();
            }
            else
            {
                ClienteWizard02 cliente = new ClienteWizard02
                {
                    Nome = nome,
                    Email = email,
                    Telefone1 = telefone,
                    IndicadoPor = indicadoPor
                };

                OcorrenciaWizard02 ocorrencia = new OcorrenciaWizard02
                {
                    ClienteWizard02 = cliente,
                    CampanhaId = 3,
                    FilaId = 3
                };

                var evento = new EventoWizard02();
                PropertyCopier.Copy(cliente, evento);
                evento.ClienteWizard02 = cliente;
                //evento.UsersId = User.Identity.GetUserId();
                evento.FilaId = ocorrencia.FilaId;
                ocorrencia.AgendamentoProprio = true;
                ocorrencia.UsersId = evento.UsersId;
                ocorrencia.Eventos.Add(evento);
                await _context.ClienteWizard02.AddAsync(cliente);
                await _context.OcorrenciaWizard02.AddAsync(ocorrencia);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> GetRegraAsync(int classificacao)
        {
            var agendamento = await _context.ClassificacaoWizard02.Where(x => x.Id == classificacao).Select(s => new { s.Agendamento }).FirstOrDefaultAsync();
            return agendamento.Agendamento; //Atenção!
        }

        public async Task<List<ClassificacaoWizard02>> GetClassificacaoAsync(int grupo)
        {
            var classificacoes = await _context.ClassificacaoWizard02.Include(i => i.Classificacoes).Where(x => x.Ativo && x.ClassificacaoPaiId == null && x.GrupoProcessoWizard02.Any(a => a.Id == grupo)).ToListAsync();
            return classificacoes;
        }

        public async Task<List<OcorrenciaWizard02View>> GetFilaOcorrenciasAsync()
        {
            //var userId = User.Identity.GetUserId();
            var data = DateTime.Now;

            var query = await _context.OcorrenciaWizard02
                .Include(i => i.Campanha)
                .Include(i => i.ClienteWizard02)
                .Include(i => i.Eventos)
                .Include(i => i.Eventos.Select(s => s.Classificacao))
                .Where(x =>
                   x.Finalizado == false
                   && x.Campanha.Ativo
                   && x.ClienteWizard02.Ativo
                   && x.Eventos.Count > 1
                   //&& x.Fila.Users.Any(a => a.Id == userId)
                   //&& ((x.AgendamentoProprio && x.UsersId == userId) || x.AgendamentoProprio == false)
                   && ((x.ProximoAt <= data
                   //&& ((x.ProximoAt >= data && x.UsersId == userId) || x.ProximoAt <= data)
                   && x.Agendamento) || x.FilaId == 2))
            .OrderBy(x => x.Eventos.Count).ThenBy(x => x.Campanha.Prioridade).ThenByDescending(x => x.ProximoAt).Select(s => new OcorrenciaWizard02View
            {
                OcorrenciaId = s.Id,
                Cliente = s.ClienteWizard02.Nome,
                Fila = s.Fila.Nome,
                Prioridade = s.Campanha.Prioridade,
                ProximoAt = s.ProximoAt,
                DtCriacao = s.DtCriacao,
                DtMovimentacao = (DateTime)s.Eventos.OrderByDescending(o => o.Id).FirstOrDefault().DtEvento,
                ClassificacaoId = s.Eventos.OrderByDescending(o => o.Id).FirstOrDefault().ClassificacaoId
            }).ToListAsync();

            foreach (var item in query)
            {
                if (item.ClassificacaoId != null)
                {
                    var aux2 = _context.ClassificacaoWizard02.FirstOrDefault(x => x.Id == item.ClassificacaoId);
                    var aux = aux2?.ClassificacaoPaiId;
                    var view = "";
                    for (; ; )
                    {
                        if (aux == null)
                        {
                            break;
                        }
                        var pai = _context.ClassificacaoWizard02.FirstOrDefault(x => x.Id == aux);
                        view = pai.Nome + " > " + view;
                        aux = pai.ClassificacaoPaiId;
                    }
                    item.ClassificacaoView = view + aux2?.Nome;
                }
            }
            return query;
        }

        public async Task<List<OcorrenciaWizard02View>> GetClienteOcorrenciasAsync(int id)
        {
            //var userId = User.Identity.GetUserId();
            //var filas = _context.FilaWizard02.Where(x => x.Users.Any(a => a.Id == userId)).ToList();
            List<OcorrenciaWizard02View> query = new List<OcorrenciaWizard02View>();

            //foreach (var item in filas)
            //{
            //    var resultado = await _context.OcorrenciaWizard02.Include(i => i.Campanha).Include(i => i.ClienteWizard02)
            //    .Where(x => x.ClienteWizard02Id == id
            //    && x.FilaId == item.Id
            //    && ((x.UsersId == userId && x.AgendamentoProprio) || !x.AgendamentoProprio)
            //    && x.Campanha.Ativo
            //    && x.ClienteWizard02.Ativo)
            //    .Select(s => new OcorrenciaWizard02View
            //    {
            //        OcorrenciaId = s.Id,
            //        Cliente = s.ClienteWizard02.Nome,
            //        Fila = s.Fila.Nome,
            //        Prioridade = s.Campanha.Prioridade,
            //        ProximoAt = s.ProximoAt,
            //        DtCriacao = s.DtCriacao,
            //        Finalizado = s.Finalizado
            //    })
            //   .ToListAsync();
            //    query.AddRange(resultado);
            //}

            //foreach (var item in query)
            //{
            //    var evento = _context.OcorrenciaWizard02
            //        .Include(i => i.Eventos.Select(s => s.Classificacao))
            //        .FirstOrDefault(x => x.Id == item.OcorrenciaId)?.Eventos.LastOrDefault();

            //    item.DtMovimentacao = (DateTime)evento.DtEvento;
            //    item.ClassificacaoId = evento.ClassificacaoId;

            //    if (item.ClassificacaoId != null)
            //    {
            //        var aux2 = await _context.ClassificacaoWizard02.FirstOrDefaultAsync(x => x.Id == item.ClassificacaoId);
            //        var aux = aux2?.ClassificacaoPaiId;
            //        var view = "";
            //        for (; ; )
            //        {
            //            if (aux == null)
            //            {
            //                break;
            //            }

            //            var pai = await _context.ClassificacaoWizard02.FirstOrDefaultAsync(x => x.Id == aux);
            //            view = pai.Nome + " > " + view;
            //            aux = pai.ClassificacaoPaiId;

            //        }
            //        item.ClassificacaoView = view + aux2?.Nome;
            //    }
            //}
            return query;
        }

        public async Task<List<CampanhaWizard02>> CampanhaAsync()
        {
            var campanhas = await _context.CampanhaWizard02.ToListAsync();
            return campanhas;
        }

        public async Task<CampanhaWizard02> GetCampanhaEditAsync(int? id)
        {
            var campanha = await _context.CampanhaWizard02.FindAsync(id);
            return campanha;
        }

        public async Task CampanhaEditAsync(CampanhaWizard02 campanhaUpdate)
        {
            var campanha = await _context.CampanhaWizard02.FindAsync(campanhaUpdate.Id);

            if (campanha != null)
            {
                campanha.Ativo = campanhaUpdate.Ativo;
                campanha.TentativasMax = campanhaUpdate.TentativasMax;
                campanha.Nome = campanhaUpdate.Nome;
                campanha.Prioridade = campanhaUpdate.Prioridade;
            }            
            await _context.SaveChangesAsync();
        }

        public async Task<ClienteWizard02> ImportarLocalizadoAsync(string email)
        {
            var localizado = await _context.ClienteWizard02.FirstOrDefaultAsync(x => x.Email == email);
            return localizado;
        }

        public async Task ImportarFinallyAsync(List<OcorrenciaWizard02> ocorrencias)
        {
            await _context.OcorrenciaWizard02.AddRangeAsync(ocorrencias);
            await _context.SaveChangesAsync();
        }

        public async Task CampanhaAtualizarAsync()
        {
            var lista = await _context.CampanhaWizard02.Where(x => x.Ativo).ToListAsync();

            var tentativas = await _context.EventoWizard02
                .Include(i => i.Ocorrencia)
                .Include(i => i.Ocorrencia.Campanha)
                .Include(i => i.Classificacao)
                .Where(x => x.Ocorrencia.Campanha.Ativo && x.Classificacao.Tentativa).GroupBy(x => new { x.Ocorrencia.CampanhaId })
                .Select(u => new { u.Key, Count = u.Count() })
                .ToListAsync();

            var registros = await _context.EventoWizard02.Include(i => i.Ocorrencia).Include(i => i.Ocorrencia.Campanha).Include(i => i.Classificacao)
                .Where(b => (b.Ocorrencia.Campanha.Ativo && b.Classificacao.Tentativa) && _context.EventoWizard02
                .Where(w => w.Classificacao.Tentativa && w.Ocorrencia.Campanha.Ativo).GroupBy(y => new { y.Ocorrencia.Id }).Select(u => new { Id = u.Max(z => z.Id) })
                .Where(rl => rl.Id == b.Id)
                .Select(rl => rl.Id)
                .Contains(b.Id))
                .GroupBy(x => new { x.Ocorrencia.CampanhaId })
                .Select(u => new
                {
                    u.Key,
                    trabalhado = u.Count(),
                    sucesso = u.Count(c => c.Classificacao.Sucesso),
                    recusa = u.Count(c => c.Classificacao.Recusa),
                    cpc = u.Count(c => c.Classificacao.CPC),
                    target = u.Count(c => c.Classificacao.Target),
                    nLocalizado = u.Count(c => c.Classificacao.NLocalizado)
                }).ToListAsync();

            foreach (var item in lista)
            {
                item.Registros = await _context.OcorrenciaWizard02.CountAsync(x => x.CampanhaId == item.Id);

                foreach (var item2 in registros)
                {
                    if (item.Id == item2.Key.CampanhaId)
                    {
                        item.Trabalhados = item2.trabalhado;
                        item.Sucesso = item2.sucesso;
                        item.Recusa = item2.recusa;
                        item.CPC = item2.cpc;
                        item.Target = item2.target;
                        item.NLocalizado = item2.nLocalizado;
                    }
                }
                foreach (var item2 in tentativas)
                {
                    if (item.Id == item2.Key.CampanhaId)
                    {
                        item.Tentativas = item2.Count;
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<FilaWizard02>> MovimentarFilasGetAsync()
        {
            var filas = await _context.FilaWizard02.ToListAsync();
            return filas;
        }

        public async Task<List<CampanhaWizard02>> MovimentarCampanhasGetAsync()
        {
            var campanhas = await _context.CampanhaWizard02.ToListAsync();
            return campanhas;
        }
    }
}
