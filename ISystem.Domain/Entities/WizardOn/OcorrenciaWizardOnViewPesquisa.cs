using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.WizardOn
{
    [NotMapped]
    public class OcorrenciaWizardOnViewPesquisa
    {
        public List<int> OcorrenciasId { get; set; } = new List<int>();
        //public string Cliente { get; set; }
        public int? FilaId { get; set; }
        public int? CampanhaId { get; set; }
        public int? ClassificacaoId { get; set; }
        public bool Finalizado { get; set; }
        public bool AgendamentoProprio { get; set; }
        public DateTime? DtMovimentacaoInicio { get; set; }
        public DateTime? DtMovimentacaoFim { get; set; }
        public DateTime? DtCriacaoInicio { get; set; }
        public DateTime? DtCriacaoFim { get; set; }
    }
}
