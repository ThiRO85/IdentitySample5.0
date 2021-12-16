using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.Wizard02
{
    public class ClassificacaoWizard02 : Entity
    {
        public ClassificacaoWizard02()
        {
            GrupoProcessoWizard02 = new Collection<GrupoProcessoWizard02>();
            Classificacoes = new Collection<ClassificacaoWizard02>();
        }

        public bool Finalizador { get; set; }
        public bool Ativo { get; set; }
        public int? FilaId { get; set; }
        public virtual FilaWizard02 Fila { get; set; }
        public bool AgendamentoProprio { get; set; }
        public int RetornoEmMin { get; set; }
        public int? ClassificacaoPaiId { get; set; }
        public ClassificacaoWizard02 ClassificacaoPai { get; set; }
        public virtual ICollection<GrupoProcessoWizard02> GrupoProcessoWizard02 { get; set; }
        public virtual ICollection<ClassificacaoWizard02> Classificacoes { get; set; }
        public bool Agendamento { get; set; }
        public bool CPC { get; set; } = false;
        public bool Recusa { get; set; } = false;
        public bool Sucesso { get; set; } = false;
        public bool Target { get; set; } = false;
        public bool NLocalizado { get; set; } = false;
        public bool Tentativa { get; set; } = false;
        public bool ZeraTentativa { get; set; } = false;
        public bool EnviaEmail { get; set; } = false;

        [NotMapped]
        public string ClassificacaoView { get; set; }
        public int? StatusLeadApiId { get; set; }
        public int? MotivosPerdaApiId { get; set; }

        [Column("Status_Id")]
        public int? StatusId { get; set; }
        public StatusWizard02 Status { get; set; }
    }
}
