using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ISystem.Domain.Entities.WizardOn
{
    public class GrupoProcessoWizardOn : Entity
    {
        public virtual ICollection<ClassificacaoWizardOn> ClassificacaoWizardOn { get; set; }
        public virtual ICollection<TipoProcessoWizardOn> TipoProcessoWizardOn { get; set; }

        //[NotMapped]
        //public IEnumerable<SelectListItem> ClassificacaoList { get; set; }

        public GrupoProcessoWizardOn()
        {
            TipoProcessoWizardOn = new Collection<TipoProcessoWizardOn>();
            ClassificacaoWizardOn = new Collection<ClassificacaoWizardOn>();
        }
    }
}
