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
        Task<OcorrenciaWizard02> RoletaAsync();
        Task<FilaWizard02> GetFilaAsync(int id);
        Task<List<object>> GetFilaUsuarioAsync(int id);
        Task<string> FilaEditUsuarioAsync(string userIdList, int id, bool removerUsuario);
        Task<ClienteWizard02> IndicadoPorAsync(int? clienteId);
        Task SetIndicacaoAsync(ClienteWizard02 indicadoPor, string nome, string telefone, string email);
        Task<bool> GetRegraAsync(int classificacao);
        Task<List<ClassificacaoWizard02>> GetClassificacaoAsync(int grupo);
        Task<List<OcorrenciaWizard02View>> GetFilaOcorrenciasAsync();
        Task<List<OcorrenciaWizard02View>> GetClienteOcorrenciasAsync(int id);
        Task<List<CampanhaWizard02>> CampanhaAsync();
        Task<CampanhaWizard02> GetCampanhaEditAsync(int? id);
        Task CampanhaEditAsync(CampanhaWizard02 campanhaUpdate);
        Task<ClienteWizard02> ImportarLocalizadoAsync(string email); //Método Importar!
        Task ImportarFinallyAsync(List<OcorrenciaWizard02> ocorrencias); //Método Importar!
        Task CampanhaAtualizarAsync();
        Task<List<FilaWizard02>> MovimentarFilasGetAsync(); //Método Movimentar Get!
        Task<List<CampanhaWizard02>> MovimentarCampanhasGetAsync(); //Método Movimentar Get!
    }
}
