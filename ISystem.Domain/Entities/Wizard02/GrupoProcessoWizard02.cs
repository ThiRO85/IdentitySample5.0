using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ISystem.Domain.Entities.Wizard02
{
    public class GrupoProcessoWizard02 : Entity
    {
        public GrupoProcessoWizard02()
        {
            ClassificacaoWizard02 = new Collection<ClassificacaoWizard02>();
            //TipoProcessoWizard02 = new Collection<TipoProcessoWizard02>();
        }

        public virtual ICollection<ClassificacaoWizard02> ClassificacaoWizard02 { get; set; }
        //public virtual ICollection<TipoProcessoWizard02> TipoProcessoWizard02 { get; set; }

        //[NotMapped]
        //public IEnumerable<SelectListItem> ClassificacaoList { get; set; } //Buscar opção!
    }
}
