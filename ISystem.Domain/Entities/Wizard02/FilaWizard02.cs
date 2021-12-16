using ISystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.Wizard02
{
    public class FilaWizard02 : Entity
    {
        public FilaWizard02()
        {
            Ocorrencias = new Collection<OcorrenciaWizard02>();
            Users = new Collection<IApplicationUser>();
        }

        public DateTime? DtCriacao { get; set; }
        public bool Ativo { get; set; }

        [Column("GrupoWizard02_Id")]
        public int? GrupoWizard02Id { get; set; }
        public virtual GrupoProcessoWizard02 GrupoWizard02 { get; set; }
        public virtual ICollection<OcorrenciaWizard02> Ocorrencias { get; set; }
        public virtual ICollection<IApplicationUser> Users { get; set; }
    }
}
