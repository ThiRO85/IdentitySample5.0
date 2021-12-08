using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.WizardOn
{
    [NotMapped]
    public class OcorrenciaWizardOnView
    {
        public int OcorrenciaId { get; set; }
        public string Cliente { get; set; }
        public string Fila { get; set; }
        public int? ClassificacaoId { get; set; }

        [Display(Name = "Classificação")]
        public string ClassificacaoView { get; set; }
        public string N1 { get; set; }
        public string N2 { get; set; }
        public string N3 { get; set; }
        public bool Finalizado { get; set; }
        public int Prioridade { get; set; }
        public DateTime ProximoAt { get; set; }
        public DateTime DtMovimentacao { get; set; }
        public DateTime DtCriacao { get; set; }
    }
}
