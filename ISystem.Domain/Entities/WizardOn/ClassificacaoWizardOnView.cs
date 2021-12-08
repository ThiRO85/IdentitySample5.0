using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.WizardOn
{
    [NotMapped]
    public class ClassificacaoWizardOnView
    {
        public int id { get; set; }
        public string text { get; set; }
        public int level { get; set; }
        public bool disabled { get; set; } = false;
    }
}
