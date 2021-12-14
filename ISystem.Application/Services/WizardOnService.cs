using ISystem.Application.Interfaces;
using ISystem.Domain.Entities.WizardOn;
using ISystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISystem.Application.Services
{
    public class WizardOnService : IWizardOnService
    {
        private readonly IWizardOnRepository _wizardOnRepository;

        public WizardOnService(IWizardOnRepository wizardOnRepository)
        {
            _wizardOnRepository = wizardOnRepository;
        }

        public async Task<List<ClienteWizardOn>> Index(string nome, string telefone1, string cpf, string email)
        {
            var lista = await _wizardOnRepository.IndexAsync(nome, telefone1, cpf, email);
            return lista;
        }

        public async Task<ClienteWizardOn> NovoCliente(ClienteWizardOn cliente)
        {
            var novoCliente = await _wizardOnRepository.NovoClienteAsync(cliente);
            return novoCliente;
        }

        public async Task<List<EventoWizardOn>> RegraRenitencia(EventoWizardOn evento, bool reprocessando)
        {
            var regraRenitencia = await _wizardOnRepository.RegraRenitenciaAsync(evento, reprocessando);
            return regraRenitencia;
        }

        public async Task<OcorrenciaWizardOn> CriarOcorrencia(int? id)
        {
            var ocorrencia = await _wizardOnRepository.CriarOcorrenciaAsync(id);
            return ocorrencia;
        }

        public async Task<OcorrenciaWizardOn> ReseteOcorrencia(int id)
        {
            var ocorrencia = await _wizardOnRepository.ReseteOcorrenciaAsync(id);
            return ocorrencia;
        }        
    }
}
