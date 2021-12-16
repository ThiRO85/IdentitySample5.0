using ISystem.Application.Interfaces;
using ISystem.Domain.Entities.Wizard02;
using ISystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISystem.Application.Services
{
    public class Wizard02Service : IWizard02Service
    {
        private readonly IWizard02Repository _wizard02Repository;

        public Wizard02Service(IWizard02Repository wizard02Repository)
        {
            _wizard02Repository = wizard02Repository;
        }

        public async Task<List<ClienteWizard02>> Index(string nome, string telefone1, string cpf, string email)
        {
            var list = await _wizard02Repository.IndexAsync(nome, telefone1, cpf, email);
            return list;
        }

        public async Task<ClienteWizard02> NovoCliente(ClienteWizard02 cliente)
        {
            var novoCliente = await _wizard02Repository.NovoClienteAsync(cliente);
            return novoCliente;
        }

        public async Task<List<EventoWizard02>> RegraRenitencia(EventoWizard02 evento, bool reprocessando)
        {
            var regraRenitencia = await _wizard02Repository.RegraRenitenciaAsync(evento, reprocessando);
            return regraRenitencia;
        }

        public async Task<OcorrenciaWizard02> CriarOcorrencia(int? id)
        {
            var ocorrencia = await _wizard02Repository.CriarOcorrenciaAsync(id);
            return ocorrencia;
        }

        public async Task<OcorrenciaWizard02> ReseteOcorrencia(int id)
        {
            var ocorrencia = await _wizard02Repository.ReseteOcorrenciaAsync(id);
            return ocorrencia;
        }
    }
}
