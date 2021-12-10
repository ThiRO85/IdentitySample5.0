using System.ComponentModel.DataAnnotations;

namespace ISystem.WebUI.Models.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número do Telefone")]
        public string Number { get; set; }
    }
}
