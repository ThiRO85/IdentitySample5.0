namespace ISystem.Domain.Entities.WizardOn
{
    public class ProcessoWizardOn
    {
        public int Id { get; set; }
        public GrupoProcessoWizardOn GrupoProcessoWizardOn { get; set; }
        public TipoProcessoWizardOn TipoProcessoWizardOn { get; set; }
    }
}
