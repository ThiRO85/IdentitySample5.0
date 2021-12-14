using ISystem.Application.Methods;
using ISystem.Domain.Entities.WizardOn;
using ISystem.Domain.Interfaces;
using ISystem.Domain.Validation;
using ISystem.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ISystem.Infrastructure.Repositories
{
    public class WizardOnRepository : IWizardOnRepository
    {
        ApplicationDbContext _context;

        public WizardOnRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteWizardOn>> IndexAsync(string nome, string telefone1, string cpf, string email)
        {
            var lista = await _context.ClienteWizardOn.Where(x =>
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

        public async Task<ClienteWizardOn> NovoClienteAsync(ClienteWizardOn cliente)
        {
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
                throw new DomainExceptionValidation("Preencha pelo menos um dos campos");

            if (_context.ClienteWizardOn.Any(x => x.Cpf == cliente.Cpf) && !string.IsNullOrWhiteSpace(cliente.Cpf))
                throw new DomainExceptionValidation("CPF já cadastrado");

            if (_context.ClienteWizardOn.Any(x => x.Email == cliente.Email) && !string.IsNullOrWhiteSpace(cliente.Email))
                throw new DomainExceptionValidation("Email já cadastrado");

            if (string.IsNullOrWhiteSpace(cliente.Telefone1))
                throw new DomainExceptionValidation("Telefone é obrigatório");

            cliente.DtCriacao = DateTime.Now;
            await _context.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<List<EventoWizardOn>> RegraRenitenciaAsync(EventoWizardOn evento, bool reprocessando)
        {
            RegraRenitenciaWizardOn regra = await _context.RegraRenitenciaWizardOn
                .Where(x => x.Ativo
                && (evento.Ocorrencia.Tentativas == x.Tentativa)
                && evento.Ocorrencia.FilaId == x.ConsiderarFilaId).FirstOrDefaultAsync();

            if (regra == null)
                regra = await _context.RegraRenitenciaWizardOn
               .Where(x => x.Ativo && evento.Ocorrencia.FilaId == x.ConsiderarFilaId)
               .OrderByDescending(o => o.Id)
               .FirstOrDefaultAsync();

            List<EventoWizardOn> listaEvento = new List<EventoWizardOn>();
            if (regra != null)
            {
                if (!reprocessando)
                    evento.Ocorrencia.ProximoAt = DateTime.Now.AddMinutes(regra.IntervaloRetorno);
                    listaEvento.Add(evento);

                if (regra.EnviarParaClassificacaoId != null
                    && !evento.Ocorrencia.Finalizado
                    && (!evento.Ocorrencia.Agendamento || evento.Ocorrencia.Agendamento == regra.ConsiderarAgendamento))
                {
                    var newObject = new EventoWizardOn();
                    PropertyCopier.Copy(evento, newObject);
                    newObject.Id = 0;
                    newObject.Ocorrencia = evento.Ocorrencia;
                    newObject.ClienteWizardOn = evento.ClienteWizardOn;
                    newObject.Classificacao = await _context.ClassificacaoWizardOn.FirstOrDefaultAsync(x => x.Id == regra.EnviarParaClassificacaoId);
                    newObject.Ocorrencia.Finalizado = newObject.Classificacao.Finalizador;
                    newObject.Ocorrencia.StatusId = newObject.Classificacao.StatusId;
                    listaEvento.Add(newObject);
                }
            }
            else
                listaEvento.Add(evento);

            return listaEvento;
        }

        public async Task<OcorrenciaWizardOn> CriarOcorrenciaAsync(int? id)
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

            var ocorrencia = new OcorrenciaWizardOn();

            ocorrencia.FilaId = 1; // Fila Padrão
            ocorrencia.ClienteWizardOnId = (int)id;
            ocorrencia.ProximoAt = DateTime.Now.AddMinutes(60);
            ocorrencia.Finalizado = false;
            //ocorrencia.UsersId = User.Identity.GetUserId();
            ocorrencia.CampanhaId = 1;

            var cliente = await _context.ClienteWizardOn.FirstOrDefaultAsync(x => x.Id == id);

            var evento = new EventoWizardOn();

            PropertyCopier.Copy(cliente, evento);

            evento.ClienteWizardOn = cliente;
            //evento.UsersId = User.Identity.GetUserId();
            evento.FilaId = ocorrencia.FilaId;

            ocorrencia.Eventos.Add(evento);
            await _context.OcorrenciaWizardOn.AddAsync(ocorrencia);
            await _context.SaveChangesAsync();
            return ocorrencia;
        }

        public async Task<OcorrenciaWizardOn> ReseteOcorrenciaAsync(int id)
        {
            var ocorrencia = await _context.OcorrenciaWizardOn.FindAsync(id);
            if (!ocorrencia.Finalizado)
            {
                ocorrencia.ProximoAt = DateTime.Now;
                ocorrencia.Agendamento = false;
                ocorrencia.AgendamentoProprio = false;
                await _context.SaveChangesAsync();
            }
            return ocorrencia;
        }
    }
}
