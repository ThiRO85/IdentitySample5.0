using ISystem.Domain.Entities.Wizard02;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISystem.Domain.Interfaces
{
    public interface IWizard02Repository 
    {
        Task<List<ClienteWizard02>> IndexAsync(string nome, string telefone1, string cpf, string email);
        Task<ClienteWizard02> NovoClienteAsync(ClienteWizard02 cliente);
        Task<List<EventoWizard02>> RegraRenitenciaAsync(EventoWizard02 evento, bool reprocessando);
        Task<OcorrenciaWizard02> CriarOcorrenciaAsync(int? id);
        Task<OcorrenciaWizard02> ReseteOcorrenciaAsync(int id);
    }
}
