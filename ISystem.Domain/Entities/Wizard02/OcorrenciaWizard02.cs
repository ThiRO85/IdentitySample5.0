using ISystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.Wizard02
{
    public class OcorrenciaWizard02
    {
        public OcorrenciaWizard02()
        {
            Eventos = new Collection<EventoWizard02>();
        }

        public int Id { get; set; }
        public virtual ICollection<EventoWizard02> Eventos { get; set; }

        [Column("ClienteWizard02_Id")]
        public int ClienteWizard02Id { get; set; }
        public virtual ClienteWizard02 ClienteWizard02 { get; set; }

        [Column("Campanha_Id")]
        public int CampanhaId { get; set; }
        public virtual CampanhaWizard02 Campanha { get; set; }
        public int Tentativas { get; set; }
        public bool AgendamentoProprio { get; set; }
        public bool Agendamento { get; set; }

        [Column("Fila_Id")]
        public int FilaId { get; set; }
        public virtual FilaWizard02 Fila { get; set; }
        public bool Finalizado { get; set; }
        public DateTime ProximoAt { get; set; } = DateTime.Now;

        [Column("Users_Id")]
        public string UsersId { get; set; }
        public IApplicationUser Users { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DtCriacao { get; set; } = DateTime.Now;
        public int? ApiId { get; set; }
        public bool ModificadoApi { get; set; } = false;
        public bool Callcenter { get; set; } = true; //Campo a mais em relação à WizardOn

        [Column("Status_Id")]
        public int? StatusId { get; set; }
        public StatusWizard02 Status { get; set; }
    }
}
