namespace ISystem.Domain.Entities.Wizard02
{
    public class ProcessoWizard02 //Método vindo da WizardOn
    {
        public int Id { get; set; }
        public GrupoProcessoWizard02 GrupoProcessoWizardOn { get; set; }
        public TipoProcessoWizard02 TipoProcessoWizardOn { get; set; }
    }
}
