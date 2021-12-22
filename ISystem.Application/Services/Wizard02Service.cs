﻿using ISystem.Application.Interfaces;
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
            var list = await _wizard02Repository.IndexAsync(nome, telefone1, cpf, email);
            return list;
        }

        public async Task<ClienteWizard02> NovoCliente(ClienteWizard02 cliente)
        {
            var novoCliente = await _wizard02Repository.NovoClienteAsync(cliente);
            return novoCliente;
        }

        public async Task<List<EventoWizard02>> RegraRenitencia(EventoWizard02 evento, bool reprocessando)
        {
            var regraRenitencia = await _wizard02Repository.RegraRenitenciaAsync(evento, reprocessando);
            return regraRenitencia;
        }

        public async Task<OcorrenciaWizard02> CriarOcorrencia(int? id)
        {
            var ocorrencia = await _wizard02Repository.CriarOcorrenciaAsync(id);
            return ocorrencia;
        }

        public async Task<OcorrenciaWizard02> ReseteOcorrencia(int id)
        {
            var ocorrencia = await _wizard02Repository.ReseteOcorrenciaAsync(id);
            return ocorrencia;
        }

        public async Task<OcorrenciaWizard02> Roleta()
        {
            var ocorrencia = await _wizard02Repository.RoletaAsync();
            return ocorrencia;
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
    }
}
