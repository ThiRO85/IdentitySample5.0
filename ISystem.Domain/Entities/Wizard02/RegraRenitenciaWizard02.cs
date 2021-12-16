using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.Wizard02
{
    public class RegraRenitenciaWizard02
    {
        public int Id { get; set; }
        public int Tentativa { get; set; }
        public int IntervaloRetorno { get; set; }
        public bool ConsiderarAgendamento { get; set; }

        [Column("ConsiderarFila_Id")]
        public int ConsiderarFilaId { get; set; }
        public FilaWizard02 ConsiderarFila { get; set; }

        [Column("EnviarParaClassificacao_Id")]
        public int? EnviarParaClassificacaoId { get; set; }
        public ClassificacaoWizard02 EnviarParaClassificacao { get; set; }
        public bool Ativo { get; set; }
    }
}
