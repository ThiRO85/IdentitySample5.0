using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.WebUI.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Cpf { get; set; }

        public string SistemaClienteUsuarioId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
