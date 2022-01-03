using ISystem.Domain.Entities.Wizard02;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISystem.Domain.Interfaces
{
    public interface IWizard02Repository 
    {
        Task<List<ClienteWizard02>> IndexAsync(string nome, string telefone1, string cpf, string email);
        Task NovoClienteAsync(ClienteWizard02 cliente);
        Task<List<EventoWizard02>> RegraRenitenciaAsync(EventoWizard02 evento, bool reprocessando);
        Task<bool> CriarOcAsync(int? id);
        Task<OcorrenciaWizard02> CriarOcorrenciaAsync(int? id);
        Task ReseteOcorrenciaAsync(int id);
        Task<OcorrenciaWizard02> RoletaAsync();
        Task<OcorrenciaWizard02> AtendimentoOcorrenciaGetAsync(int? ocorrenciaId, string userid);
        Task AtendimentoRetornoGetAsync(OcorrenciaWizard02 ocorrencia, string userid);
        Task<ClienteWizard02> AtendimentoEventoClienteAsync(EventoWizard02 evento);
        Task<OcorrenciaWizard02> AtendimentoEventoOcorrenciaAsync(EventoWizard02 evento);
        Task<ClassificacaoWizard02> AtendimentoEventoClassificacaoAsync(EventoWizard02 evento, int? classificacaoId);
        Task AtendimentoListaEventosAsync(List<EventoWizard02> listaEventos);
        Task<ClassificacaoWizard02> AtendimentoPaiAsync(int? aux);
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
        Task<List<OcorrenciaWizard02View>> MovimentarAsync(OcorrenciaWizard02ViewPesquisa valores);
        Task<string> EditOcorrenciaAsync(string ocorrenciaList, string comentario, int? filaId, int? classificacaoId);
        Task<List<FilaWizard02>> FilaAsync();
        Task<List<GrupoProcessoWizard02>> FilaCreateGetAsync(); //Atenção!
        Task FilaCreateAsync(FilaWizard02 fila);
        Task<FilaWizard02> FilaEditGetAsync(int? id);
        Task FilaEditAsync(FilaWizard02 fila);
        Task<GrupoProcessoWizard02> GrupoProcessoCreateGetAsync();
        Task GrupoProcessoCreateAsync(GrupoProcessoWizard02 grupoProcesso, params string[] classificacao);
        Task<List<ClassificacaoWizard02>> GrupoProcessoCreateIIAsync();
        Task<GrupoProcessoWizard02> GrupoProcessoEditGetAsync(int? id);
        Task GrupoProcessoEditAsync(GrupoProcessoWizard02 grupoProcesso, params string[] classificacao);
        Task<List<StatusWizard02>> ClassificacaoCreateGetAsync();
        Task ClassificacaoCreateAsync(ClassificacaoWizard02 classificacao);
        Task<ClassificacaoWizard02> ClassificacaoEditGetAsync(int? id);
        Task ClassificacaoEditAsync(ClassificacaoWizard02 classificacao);
    }
}
