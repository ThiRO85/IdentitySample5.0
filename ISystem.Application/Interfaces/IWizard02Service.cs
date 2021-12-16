using ISystem.Domain.Entities.Wizard02;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISystem.Application.Interfaces
{
    public interface IWizard02Service
    {
        Task<List<ClienteWizard02>> Index(string nome, string telefone1, string cpf, string email);
        Task<ClienteWizard02> NovoCliente(ClienteWizard02 cliente);
        Task<List<EventoWizard02>> RegraRenitencia(EventoWizard02 evento, bool reprocessando);
        Task<OcorrenciaWizard02> CriarOcorrencia(int? id);
        Task<OcorrenciaWizard02> ReseteOcorrencia(int id);
    }
}
