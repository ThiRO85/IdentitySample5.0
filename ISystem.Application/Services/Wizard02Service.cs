using ISystem.Application.Interfaces;
using ISystem.Domain.Entities.Wizard02;
using ISystem.Domain.Interfaces;
using System.Collections.Generic;
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
            var clientes = await _wizard02Repository.IndexAsync(nome, telefone1, cpf, email);
            return clientes;
        }

        public async Task NovoCliente(ClienteWizard02 cliente)
        {
            await _wizard02Repository.NovoClienteAsync(cliente);
        }

        public async Task<List<EventoWizard02>> RegraRenitencia(EventoWizard02 evento, bool reprocessando)
        {
            var eventos = await _wizard02Repository.RegraRenitenciaAsync(evento, reprocessando);
            return eventos;
        }

        public async Task<bool> CriarOc(int? id)
        {
            var ocorrencia = await _wizard02Repository.CriarOcAsync(id);
            return ocorrencia;
        }

        public async Task<OcorrenciaWizard02> CriarOcorrencia(int? id)
        {
            var ocorrencia = await _wizard02Repository.CriarOcorrenciaAsync(id);
            return ocorrencia;
        }

        public async Task ReseteOcorrencia(int id)
        {
            await _wizard02Repository.ReseteOcorrenciaAsync(id);
        }

        public async Task<OcorrenciaWizard02> Roleta()
        {
            var ocorrencia = await _wizard02Repository.RoletaAsync();
            return ocorrencia;
        }

        public async Task<OcorrenciaWizard02> AtendimentoOcorrenciaGet(int? ocorrenciaId, string userid)
        {
            var ocorrencia = await _wizard02Repository.AtendimentoOcorrenciaGetAsync(ocorrenciaId, userid);
            return ocorrencia;
        }

        public async Task AtendimentoRetornoGet(OcorrenciaWizard02 ocorrencia, string userid)
        {
            await _wizard02Repository.AtendimentoRetornoGetAsync(ocorrencia, userid);
        }

        public async Task<ClienteWizard02> AtendimentoEventoCliente(EventoWizard02 evento)
        {
            var eventoCliente = await _wizard02Repository.AtendimentoEventoClienteAsync(evento);
            return eventoCliente;
        }

        public async Task<OcorrenciaWizard02> AtendimentoEventoOcorrencia(EventoWizard02 evento)
        {
            var eventoOcorrencia = await _wizard02Repository.AtendimentoEventoOcorrenciaAsync(evento);
            return eventoOcorrencia;
        }

        public async Task<ClassificacaoWizard02> AtendimentoEventoClassificacao(EventoWizard02 evento, int? classificacaoId)
        {
            var eventoClassificacao = await _wizard02Repository.AtendimentoEventoClassificacaoAsync(evento, classificacaoId);
            return eventoClassificacao;
        }

        public async Task<FilaWizard02> GetFila(int id)
        {
            var listar = await _wizard02Repository.GetFilaAsync(id);
            return listar;
        }

        public async Task<List<object>> GetFilaUsuario(int id)
        {
            var usuarios = await _wizard02Repository.GetFilaUsuarioAsync(id);
            return usuarios;
        }

        public async Task<string> FilaEditUsuario(string userIdList, int id, bool removerUsuario)
        {
            string test = await _wizard02Repository.FilaEditUsuarioAsync(userIdList, id, removerUsuario);
            return test;
        }

        public async Task<ClienteWizard02> IndicadoPor(int? clienteId)
        {
            var indicadoPor = await _wizard02Repository.IndicadoPorAsync(clienteId);
            return indicadoPor;
        }

        public async Task SetIndicacao(ClienteWizard02 indicadoPor, string nome, string telefone, string email)
        {
            await _wizard02Repository.SetIndicacaoAsync(indicadoPor, nome, telefone, email);
        }

        public async Task<bool> GetRegra(int classificacao)
        {
            var agendamento = await _wizard02Repository.GetRegraAsync(classificacao);
            return agendamento;
        }

        public async Task<List<ClassificacaoWizard02>> GetClassificacao(int grupo)
        {
            var classificacoes = await _wizard02Repository.GetClassificacaoAsync(grupo);
            return classificacoes;
        }

        public async Task<List<OcorrenciaWizard02View>> GetFilaOcorrencias()
        {
            var query = await _wizard02Repository.GetFilaOcorrenciasAsync();
            return query;
        }

        public async Task<List<OcorrenciaWizard02View>> GetClienteOcorrencias(int id)
        {
            var query = await _wizard02Repository.GetClienteOcorrenciasAsync(id);
            return query;
        }

        public async Task<List<CampanhaWizard02>> Campanha()
        {
            var campanhas = await _wizard02Repository.CampanhaAsync();
            return campanhas;
        }

        public async Task<CampanhaWizard02> GetCampanhaEdit(int? id)
        {
            var campanha = await _wizard02Repository.GetCampanhaEditAsync(id);
            return campanha;
        }

        public async Task CampanhaEdit(CampanhaWizard02 campanhaUpdate)
        {
            await _wizard02Repository.CampanhaEditAsync(campanhaUpdate);
        }

        public async Task<ClienteWizard02> ImportarLocalizado(string email)
        {
            var localizado = await _wizard02Repository.ImportarLocalizadoAsync(email);
            return localizado;
        }

        public async Task ImportarFinally(List<OcorrenciaWizard02> ocorrencias)
        {
            await _wizard02Repository.ImportarFinallyAsync(ocorrencias);
        }

        public async Task CampanhaAtualizar()
        {
            await _wizard02Repository.CampanhaAtualizarAsync();
        }

        public async Task<List<FilaWizard02>> MovimentarFilasGet()
        {
            var filas = await _wizard02Repository.MovimentarFilasGetAsync();
            return filas;
        }

        public async Task<List<CampanhaWizard02>> MovimentarCampanhasGet()
        {
            var campanhas = await _wizard02Repository.MovimentarCampanhasGetAsync();
            return campanhas;
        }

        public async Task<List<OcorrenciaWizard02View>> Movimentar(OcorrenciaWizard02ViewPesquisa valores)
        {
            var ocorrencias = await _wizard02Repository.MovimentarAsync(valores);
            return ocorrencias;
        }

        public async Task<string> EditOcorrencia(string ocorrenciaList, string comentario, int? filaId, int? classificacaoId)
        {
            var erros = await _wizard02Repository.EditOcorrenciaAsync(ocorrenciaList, comentario, filaId, classificacaoId);
            return erros;
        }

        public async Task<List<FilaWizard02>> Fila()
        {
            var filas = await _wizard02Repository.FilaAsync();
            return filas;
        }

        public async Task<List<GrupoProcessoWizard02>> FilaCreateGet()
        {
            var grupoProcesso = await _wizard02Repository.FilaCreateGetAsync();
            return grupoProcesso;
        }

        public async Task FilaCreate(FilaWizard02 fila)
        {
            await _wizard02Repository.FilaCreateAsync(fila);
        }

        public async Task<FilaWizard02> FilaEditGet(int? id)
        {
            var fila = await _wizard02Repository.FilaEditGetAsync(id);
            return fila;
        }

        public async Task FilaEdit(FilaWizard02 fila)
        {
            await _wizard02Repository.FilaEditAsync(fila);
        }

        public async Task<GrupoProcessoWizard02> GrupoProcessoCreateGet()
        {
            var grupoProcesso = await _wizard02Repository.GrupoProcessoCreateGetAsync();
            return grupoProcesso;
        }

        public async Task GrupoProcessoCreate(GrupoProcessoWizard02 grupoProcesso, params string[] classificacao)
        {
            await _wizard02Repository.GrupoProcessoCreateAsync(grupoProcesso, classificacao);
        }

        public async Task<List<ClassificacaoWizard02>> GrupoProcessoCreateII()
        {
            var classificacoesII = await _wizard02Repository.GrupoProcessoCreateIIAsync();
            return classificacoesII;
        }

        public async Task<GrupoProcessoWizard02> GrupoProcessoEditGet(int? id)
        {
            var grupoProcesso = await _wizard02Repository.GrupoProcessoEditGetAsync(id);
            return grupoProcesso;
        }

        public async Task GrupoProcessoEdit(GrupoProcessoWizard02 grupoProcesso, params string[] classificacao)
        {
            await _wizard02Repository.GrupoProcessoEditAsync(grupoProcesso, classificacao);
        }

        public async Task<List<StatusWizard02>> ClassificacaoCreateGet()
        {
            var status = await _wizard02Repository.ClassificacaoCreateGetAsync();
            return status;
        }

        public async Task ClassificacaoCreate(ClassificacaoWizard02 classificacao)
        {
            await _wizard02Repository.ClassificacaoCreateAsync(classificacao);
        }

        public async Task<ClassificacaoWizard02> ClassificacaoEditGet(int? id)
        {
            var classificacao = await _wizard02Repository.ClassificacaoEditGetAsync(id);
            return classificacao;
        }

        public async Task ClassificacaoEdit(ClassificacaoWizard02 classificacao)
        {
            await _wizard02Repository.ClassificacaoEditAsync(classificacao);
        }

        public async Task AtendimentoListaEventos(List<EventoWizard02> listaEventos)
        {
            await _wizard02Repository.AtendimentoListaEventosAsync(listaEventos);
        }

        public async Task<ClassificacaoWizard02> AtendimentoPai(int? aux)
        {
            var pai = await _wizard02Repository.AtendimentoPaiAsync(aux);
            return pai;
        }        
    }
}
