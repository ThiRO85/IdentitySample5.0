using System;
using System.ComponentModel.DataAnnotations;

namespace ISystem.Domain.Entities.WizardOn
{
    public class CampanhaWizardOn
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Registros { get; set; }
        public int Trabalhados { get; set; }
        public int Tentativas { get; set; }
        public DateTime DtImportacao { get; set; } = DateTime.Now;
        public bool Ativo { get; set; }

        [Display(Name = "Tentativas Max")]
        public int TentativasMax { get; set; }
        public int Prioridade { get; set; }
        public int? Agendamento { get; set; }
        public int? CPC { get; set; }
        public int? Recusa { get; set; }
        public int? Sucesso { get; set; }
        public int? Target { get; set; }
        public int? NLocalizado { get; set; }
    }
}
