using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ISystem.Domain.Entities.Wizard02
{
    public class TipoProcessoWizard02 : Entity //Método vindo da WizardOn
    {
        public TipoProcessoWizard02()
        {
            GrupoProcessoWizard02 = new Collection<GrupoProcessoWizard02>();
            SubTipoProcessoWizard02 = new Collection<SubTipoProcessoWizard02>();
        }

        public bool Ativo { get; set; }
        public bool Finalizador { get; set; }
        public int? FilaId { get; set; }
        public virtual FilaWizard02 Fila { get; set; }
        public int NrTentativasTp { get; set; }
        public bool Agendamento { get; set; }
        public bool AgendamentoProprio { get; set; }
        public int RetornoEmMin { get; set; }
        public virtual ClasseProcessoWizard02 ClasseProcesso { get; set; }
        public virtual ICollection<GrupoProcessoWizard02> GrupoProcessoWizard02 { get; set; }
        public virtual ICollection<SubTipoProcessoWizard02> SubTipoProcessoWizard02 { get; set; }
    }
}
