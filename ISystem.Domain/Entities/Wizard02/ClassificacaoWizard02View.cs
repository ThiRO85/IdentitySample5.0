using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.Wizard02
{
    [NotMapped]
    public class ClassificacaoWizard02View
    {
        public int id { get; set; }
        public string text { get; set; }
        public int level { get; set; }
        public bool disabled { get; set; } = false;
    }
}
