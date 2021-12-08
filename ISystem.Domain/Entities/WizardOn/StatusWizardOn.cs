using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.WizardOn
{
    public class StatusWizardOn
    {
        public int Id { get; set; }

        [Display(Name = "Status")]
        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Nome { get; set; }
        public int Prioridade { get; set; }
    }
}
