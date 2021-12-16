using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ISystem.Domain.Entities.Wizard02
{
    public class SubTipoProcessoWizard02 : Entity //Método vindo da WizardOn
    {
        public SubTipoProcessoWizard02()
        {
            TipoProcessoWizard02 = new Collection<TipoProcessoWizard02>();
        }

        public bool Ativo { get; set; }
        public virtual ICollection<TipoProcessoWizard02> TipoProcessoWizard02 { get; set; }
    }
}
