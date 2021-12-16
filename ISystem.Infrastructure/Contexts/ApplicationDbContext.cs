using ISystem.Domain.Entities;
using ISystem.Domain.Entities.Wizard02;
using ISystem.Domain.Entities.WizardOn;
using ISystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISystem.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CampanhaWizardOn> CampanhaWizardOn { get; set; }
        public DbSet<ClasseProcessoWizardOn> ClasseProcessoWizardOn { get; set; }
        public DbSet<ClassificacaoWizardOn> ClassificacaoWizardOn { get; set; }
        public DbSet<ClienteWizardOn> ClienteWizardOn { get; set; }
        public DbSet<EventoWizardOn> EventoWizardOn { get; set; }
        public DbSet<FilaWizardOn> FilaWizardOn { get; set; }
        public DbSet<GrupoProcessoWizardOn> GrupoProcessoWizardOn { get; set; }
        public DbSet<OcorrenciaWizardOn> OcorrenciaWizardOn { get; set; }
        public DbSet<ProcessoWizardOn> ProcessoWizardOn { get; set; }
        public DbSet<RegraRenitenciaWizardOn> RegraRenitenciaWizardOn { get; set; }
        public DbSet<StatusWizardOn> StatusWizardOn { get; set; }
        public DbSet<SubTipoProcessoWizardOn> SubTipoProcessoWizardOn { get; set; }
        public DbSet<TipoProcessoWizardOn> TipoProcessoWizardOn { get; set; }

        public DbSet<CampanhaWizard02> CampanhaWizard02 { get; set; }
        public DbSet<ClasseProcessoWizard02> ClasseProcessoWizard02 { get; set; }
        public DbSet<ClassificacaoWizard02> ClassificacaoWizard02 { get; set; }
        public DbSet<ClienteWizard02> ClienteWizard02 { get; set; }
        public DbSet<EventoWizard02> EventoWizard02 { get; set; }
        public DbSet<FilaWizard02> FilaWizard02 { get; set; }
        public DbSet<GrupoProcessoWizard02> GrupoProcessoWizard02 { get; set; }
        public DbSet<Import> Import { get; set; }
        public DbSet<OcorrenciaWizard02> OcorrenciaWizard02 { get; set; }
        public DbSet<ProcessoWizard02> ProcessoWizard02 { get; set; }
        public DbSet<RegraRenitenciaWizard02> RegraRenitenciaWizard02 { get; set; }
        public DbSet<StatusWizard02> StatusWizard02 { get; set; }
        public DbSet<SubTipoProcessoWizard02> SubTipoProcessoWizard02 { get; set; }
        public DbSet<TipoProcessoWizard02> TipoProcessoWizard02 { get; set; }

        public DbSet<Client> Client { get; set; }
        public DbSet<Mantenedor> Mantenedor { get; set; }
        public DbSet<IntegracaoPabx> IntegracaoPabx { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
