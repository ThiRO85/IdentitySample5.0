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
        Task<OcorrenciaWizard02> Roleta();
        Task<FilaWizard02> GetFila(int id);
        Task<List<object>> GetFilaUsuario(int id);
        Task<string> FilaEditUsuario(string userIdList, int id, bool removerUsuario);
        Task<ClienteWizard02> IndicadoPor(int? clienteId);
        Task SetIndicacao(ClienteWizard02 indicadoPor, string nome, string telefone, string email);
        Task<bool> GetRegra(int classificacao);
        Task<List<ClassificacaoWizard02>> GetClassificacao(int grupo);
        Task<List<OcorrenciaWizard02View>> GetFilaOcorrencias();
        Task<List<OcorrenciaWizard02View>> GetClienteOcorrencias(int id);
        Task<List<CampanhaWizard02>> Campanha();
        Task<CampanhaWizard02> GetCampanhaEdit(int? id);
        Task CampanhaEdit(CampanhaWizard02 campanhaUpdate);
        Task<ClienteWizard02> ImportarLocalizado(string email);
        Task ImportarFinally(List<OcorrenciaWizard02> ocorrencias);
        Task CampanhaAtualizar();
        Task<List<FilaWizard02>> MovimentarFilasGet();
        Task<List<CampanhaWizard02>> MovimentarCampanhasGet();
    }
}
