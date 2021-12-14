using ISystem.Domain.Entities;
using ISystem.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ISystem.Infrastructure.Identity
{
    public class ApplicationUserManager /*: UserManager<ApplicationUser>*/
    {
        /*public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            // Configurando validator para nome de usuario
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };
            // Configuração de Lockout
            manager.UserLockoutEnabledByDefault = false;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(0);
            manager.MaxFailedAccessAttemptsBeforeLockout = 100;

            // Registrando os providers para Two Factor.
            manager.RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            manager.RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            //// Definindo a classe de serviço de e-mail
            //manager.EmailService = new EmailService();

            //// Definindo a classe de serviço de SMS
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

        // Metodo para login async que guarda os dados Client conectado
        public async Task<IdentityResult> SignInClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException("clientKey");
            }

            var client = user.Clients.FirstOrDefault(c => c.ClientKey == clientKey);
            if (client == null)
            {
                client = new Client { ClientKey = clientKey };
                user.Clients.Add(client);
            }

            var result = await UpdateAsync(user);
            user.CurrentClientId = client.Id.ToString();
            return result;
        }

        // Metodo para login async que remove os dados Client conectado
        public async Task<IdentityResult> SignOutClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException("clientKey");
            }

            var client = user.Clients.FirstOrDefault(c => c.ClientKey == clientKey);
            if (client != null)
            {
                user.Clients.Remove(client);
            }

            user.CurrentClientId = null;
            return await UpdateAsync(user);
        }*/
    }
}
