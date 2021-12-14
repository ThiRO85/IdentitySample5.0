using ISystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.WizardOn
{
    public class FilaWizardOn : Entity
    {
        public DateTime? DtCriacao { get; set; }
        public bool Ativo { get; set; }

        [Column("GrupoWizardOn_Id")]
        public int? GrupoWizardOnId { get; set; }
        public virtual GrupoProcessoWizardOn GrupoWizardOn { get; set; }
        public virtual ICollection<OcorrenciaWizardOn> Ocorrencias { get; set; }
        public virtual ICollection<IApplicationUser> Users { get; set; } //Alternativa para inserir ApplicationUser

        public FilaWizardOn()
        {
            Ocorrencias = new Collection<OcorrenciaWizardOn>();
            Users = new Collection<IApplicationUser>();
        }
    }
}
