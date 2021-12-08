using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.WizardOn
{
    public class ClassificacaoWizardOn : Entity
    {
        public bool Finalizador { get; set; }
        public bool Ativo { get; set; }
        public int? FilaId { get; set; }
        public virtual FilaWizardOn Fila { get; set; }
        public bool AgendamentoProprio { get; set; }
        public int RetornoEmMin { get; set; }
        public int? ClassificacaoPaiId { get; set; }
        public ClassificacaoWizardOn ClassificacaoPai { get; set; }
        public virtual ICollection<GrupoProcessoWizardOn> GrupoProcessoWizardOn { get; set; }
        public virtual ICollection<ClassificacaoWizardOn> Classificacoes { get; set; }
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
        public StatusWizardOn Status { get; set; }

        public ClassificacaoWizardOn()
        {
            GrupoProcessoWizardOn = new Collection<GrupoProcessoWizardOn>();
            Classificacoes = new Collection<ClassificacaoWizardOn>();
        }
    }
}
