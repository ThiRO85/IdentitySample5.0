using ISystem.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.Wizard02
{
    public class EventoWizard02 : Entity
    {
        [Column(TypeName = "varchar")]
        [StringLength(300)]
        [Display(Name = "Lead Id")]
        public string CodCliente { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Cpf { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Cep { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Cidade { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Estado { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Logradouro { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Numero { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Complemento { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Bairro { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Telefone1 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Telefone2 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Telefone3 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Telefone4 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Comentario { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        [Display(Name = "Valor Desejado")]
        public string ValorDesejado { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Rg { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DtNascimento { get; set; }

        [Display(Name = "Data Promessa de Pagto")]
        public DateTime? DtPromessa { get; set; } //Campo a mais em relação à WizardOn

        [Display(Name = "Data Agendamento da Visita")]
        public DateTime? DtAgendamentoVisita { get; set; }

        [Display(Name = "Id Unidade")]
        public string CodUnidade { get; set; } //Campo a mais em relação à WizardOn

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Unidade { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        [Display(Name = "Horário do Curso")]
        public string HorarioCurso { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        [Display(Name = "Dia da Semana")]
        public string DiaSemana { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        [Display(Name = "Motivo do Interesse")]
        public string MotivoInteresse { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        [Display(Name = "Nível do Idioma")]
        public string NivelIdioma { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Idioma { get; set; } //Campo a mais em relação à WizardOn

        [NotMapped]
        [Display(Name = "Classificação")]
        public string ClassificacaoView { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string CampanhaHub { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux1 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux2 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux3 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux4 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux5 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux6 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux7 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux8 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux9 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Aux10 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Display(Name = "Telefone Trabalhado")]
        public string TelefoneTrabalhado { get; set; }

        //public virtual MailingWizard02 MailingWizard02 { get; set; }
        public virtual ClienteWizard02 ClienteWizard02 { get; set; }
        public ClasseProcessoWizard02 ClasseProcessoWizard02 { get; set; } //Campo vindo de WizardOn
        public TipoProcessoWizard02 TipoProcessoWizard02 { get; set; } //Campo vindo de WizardOn
        public SubTipoProcessoWizard02 SubTipoProcessoWizard02 { get; set; } //Campo vindo de WizardOn
        public DateTime? DtAgendado { get; set; }
        public string TelefoneDiscador { get; set; }
        public string LigacaoId { get; set; }

        [Column("Classificacao_Id")]
        public int? ClassificacaoId { get; set; }
        public virtual ClassificacaoWizard02 Classificacao { get; set; }

        [Column("Users_Id")]
        public string UsersId { get; set; }
        public IApplicationUser Users { get; set; }

        [Column("Ocorrencia_Id")]
        public int? OcorrenciaId { get; set; }
        public virtual OcorrenciaWizard02 Ocorrencia { get; set; }

        [Column("Fila_Id")]
        public int FilaId { get; set; }
        public virtual FilaWizard02 Fila { get; set; }
        public DateTime? DtEvento { get; set; }
        public DateTime DtAberturaEvento { get; set; }
        public bool EnviadoApi { get; set; } = false;

        public EventoWizard02()
        {
            DtEvento = DateTime.Now;
            DtAberturaEvento = DateTime.Now;
        }
    }
}
