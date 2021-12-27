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
        Task<List<OcorrenciaWizard02View>> Movimentar(OcorrenciaWizard02ViewPesquisa valores);
        Task<string> EditOcorrencia(string ocorrenciaList, string comentario, int? filaId, int? classificacaoId);
        Task<List<FilaWizard02>> Fila();
        Task<List<GrupoProcessoWizard02>> FilaCreateGet();
        Task FilaCreate(FilaWizard02 fila);
        Task<FilaWizard02> FilaEditGet(int? id);
        Task FilaEdit(FilaWizard02 fila);
        Task<GrupoProcessoWizard02> GrupoProcessoCreateGet();
        Task GrupoProcessoCreate(GrupoProcessoWizard02 grupoProcesso, params string[] classificacao);
        Task<List<ClassificacaoWizard02>> GrupoProcessoCreateII();
        Task<GrupoProcessoWizard02> GrupoProcessoEditGet(int? id);
        Task GrupoProcessoEdit(GrupoProcessoWizard02 grupoProcesso, params string[] classificacao);
        Task<List<StatusWizard02>> ClassificacaoCreateGet();
        Task ClassificacaoCreate(ClassificacaoWizard02 classificacao);
        Task<ClassificacaoWizard02> ClassificacaoEditGet(int? id);
        Task ClassificacaoEdit(ClassificacaoWizard02 classificacao);
    }
}
