using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.WebUI.Models.ViewModels
{
    public class AdminViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        public bool Ativo { get; set; }
        public string SistemaClienteUsuarioId { get; set; }
        public string Ramal { get; set; }
        public string Posicao { get; set; }
        public string Senha { get; set; }
        public int? Mantenedor { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Cpf { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
