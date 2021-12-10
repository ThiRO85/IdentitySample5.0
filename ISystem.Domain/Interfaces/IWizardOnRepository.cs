using ISystem.Domain.Entities.WizardOn;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISystem.Domain.Interfaces
{
    public interface IWizardOnRepository
    {
        Task<List<ClienteWizardOn>> IndexAsync(string nome, string telefone1, string cpf, string email);
        Task<ClienteWizardOn> NovoClienteAsync(ClienteWizardOn cliente);
        Task<List<EventoWizardOn>> RegraRenitenciaAsync(EventoWizardOn evento, bool reprocessando);
    }
}
