using System.ComponentModel.DataAnnotations;

namespace ISystem.WebUI.Models.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome da Role")]
        public string Name { get; set; }
    }
}
