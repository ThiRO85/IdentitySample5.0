using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ISystem.Domain.Entities.WizardOn
{
    public class SubTipoProcessoWizardOn : Entity
    {
        public bool Ativo { get; set; }
        public virtual ICollection<TipoProcessoWizardOn> TipoProcessoWizardOn { get; set; }

        public SubTipoProcessoWizardOn()
        {
            TipoProcessoWizardOn = new Collection<TipoProcessoWizardOn>();
        }
    }

}
