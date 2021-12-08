using ISystem.Domain.Entities;
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

        public DbSet<Mantenedor> Mantenedor { get; set; }
        public DbSet<IntegracaoPabx> IntegracaoPabx { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
