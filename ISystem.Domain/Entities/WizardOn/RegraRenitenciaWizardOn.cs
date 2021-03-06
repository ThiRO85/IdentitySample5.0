using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.WizardOn
{
    public class RegraRenitenciaWizardOn
    {
        public int Id { get; set; }
        public int Tentativa { get; set; }
        public int IntervaloRetorno { get; set; }
        public bool ConsiderarAgendamento { get; set; }

        [Column("ConsiderarFila_Id")]
        public int ConsiderarFilaId { get; set; }
        public FilaWizardOn ConsiderarFila { get; set; }

        [Column("EnviarParaClassificacao_Id")]
        public int? EnviarParaClassificacaoId { get; set; }
        public ClassificacaoWizardOn EnviarParaClassificacao { get; set; }
        public bool Ativo { get; set; }
    }
}
