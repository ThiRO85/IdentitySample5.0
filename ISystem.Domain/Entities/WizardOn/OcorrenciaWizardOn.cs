using ISystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.WizardOn
{
    public class OcorrenciaWizardOn
    {
        public int Id { get; set; }
        public virtual ICollection<EventoWizardOn> Eventos { get; set; }

        [Column("ClienteWizardOn_Id")]
        public int ClienteWizardOnId { get; set; }
        public virtual ClienteWizardOn ClienteWizardOn { get; set; }

        [Column("Campanha_Id")]
        public int CampanhaId { get; set; }
        public virtual CampanhaWizardOn Campanha { get; set; }
        public int Tentativas { get; set; }
        public bool AgendamentoProprio { get; set; }
        public bool Agendamento { get; set; }

        [Column("Fila_Id")]
        public int FilaId { get; set; }
        public virtual FilaWizardOn Fila { get; set; }
        public bool Finalizado { get; set; }
        public DateTime ProximoAt { get; set; } = DateTime.Now;

        [Column("Users_Id")]
        public string UsersId { get; set; }
        public IApplicationUser Users { get; set; } //Alternativa para inserir ApplicationUser
        public bool Ativo { get; set; } = true;
        public DateTime DtCriacao { get; set; } = DateTime.Now;
        public int? ApiId { get; set; }
        public bool ModificadoApi { get; set; } = false;

        [Column("Status_Id")]
        public int? StatusId { get; set; }
        public StatusWizardOn Status { get; set; }

        public OcorrenciaWizardOn()
        {
            Eventos = new Collection<EventoWizardOn>();
        }
    }
}
