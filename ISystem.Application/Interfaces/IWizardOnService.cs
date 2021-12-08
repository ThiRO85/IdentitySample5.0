using ISystem.Domain.Entities.WizardOn;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISystem.Application.Interfaces
{
    public interface IWizardOnService
    {
        Task<List<ClienteWizardOn>> Index(string nome, string telefone1, string cpf, string email);
        Task<ClienteWizardOn> NovoCliente(ClienteWizardOn cliente);
        Task<List<EventoWizardOn>> RegraRenitencia(EventoWizardOn evento, bool reprocessando);
    }
}
