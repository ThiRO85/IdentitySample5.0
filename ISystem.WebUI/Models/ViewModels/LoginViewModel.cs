using System.ComponentModel.DataAnnotations;

namespace ISystem.WebUI.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "Formato inválido de email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo "
            + "{1} caracteres.", MinimumLength = 10)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

        //[Required]
        //[Display(Name = "Usuário")]
        //public string UserName { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Senha")]
        //public string Password { get; set; }

        //[Display(Name = "Lembrar login?")]
        //public bool RememberMe { get; set; }
    }
}
