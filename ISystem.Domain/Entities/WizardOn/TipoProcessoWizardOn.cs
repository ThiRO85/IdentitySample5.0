using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ISystem.Domain.Entities.WizardOn
{
    public class TipoProcessoWizardOn : Entity
    {
        public bool Ativo { get; set; }
        public bool Finalizador { get; set; }
        public int? FilaId { get; set; }
        public virtual FilaWizardOn Fila { get; set; }
        public int NrTentativasTp { get; set; }
        public bool Agendamento { get; set; }
        public bool AgendamentoProprio { get; set; }
        public int RetornoEmMin { get; set; }
        public virtual ClasseProcessoWizardOn ClasseProcesso { get; set; }
        public virtual ICollection<GrupoProcessoWizardOn> GrupoProcessoWizardOn { get; set; }
        public virtual ICollection<SubTipoProcessoWizardOn> SubTipoProcessoWizardOn { get; set; }

        public TipoProcessoWizardOn()
        {
            GrupoProcessoWizardOn = new Collection<GrupoProcessoWizardOn>();
            SubTipoProcessoWizardOn = new Collection<SubTipoProcessoWizardOn>();
        }
    }
}
