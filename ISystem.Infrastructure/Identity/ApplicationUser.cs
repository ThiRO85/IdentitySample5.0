using ISystem.Domain.Entities;
using ISystem.Domain.Entities.WizardOn;
using ISystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ISystem.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public ApplicationUser()
        {
            Clients = new Collection<Client>();

            FilaWizardOn = new Collection<FilaWizardOn>();
            //FilaWizard02 = new Collection<FilaWizard02>();
        }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<FilaWizardOn> FilaWizardOn { get; set; }
        //public virtual ICollection<FilaWizard02> FilaWizard02 { get; set; }

        [NotMapped]
        public string CurrentClientId { get; set; }
        public bool Ativo { get; internal set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Nome { get; internal set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string SistemaClienteUsuarioId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Cpf { get; internal set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> manager, ClaimsIdentity ext = null)
        //{
        //    // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType

        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

        //    var claims = new List<Claim>();

        //    if (!string.IsNullOrEmpty(CurrentClientId))
        //    {
        //        claims.Add(new Claim("AspNet.Identity.ClientId", CurrentClientId));
        //    }

        //    //  Adicione novos Claims aqui //
        //    // Adicionando Claims externos capturados no login

        //    if (ext != null)
        //    {
        //        await SetExternalProperties(userIdentity, ext);
        //    }

        //    // Gerenciamento de Claims para informaçoes do usuario
        //    //claims.Add(new Claim("AdmRoles", "True"));

        //    userIdentity.AddClaims(claims);

        //    return userIdentity;
        //}

        //private async Task SetExternalProperties(ClaimsIdentity identity, ClaimsIdentity ext)
        //{
        //    if (ext != null)
        //    {
        //        var ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
        //        // Adicionando Claims Externos no Identity
        //        foreach (var c in ext.Claims)
        //        {
        //            if (!c.Type.StartsWith(ignoreClaim))
        //                if (!identity.HasClaim(c.Type, c.Value))
        //                    identity.AddClaim(c);
        //        }
        //    }
        //}
    }
}
