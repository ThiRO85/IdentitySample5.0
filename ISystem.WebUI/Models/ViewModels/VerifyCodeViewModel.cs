using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ISystem.WebUI.Models.ViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar este Browser?")]
        public bool RememberBrowser { get; set; }

        [HiddenInput]
        public string UserId { get; set; }
    }
}
