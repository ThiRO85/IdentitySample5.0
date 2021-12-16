using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities.Wizard02
{
    public class ClienteWizard02 : Entity
    {
        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Cpf { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Rg { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? DtNascimento { get; set; }

        [Column("IndicadoPor_Id")]
        public int? IndicadoPorId { get; set; }
        public ClienteWizard02 IndicadoPor { get; set; }

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
        public bool Ativo { get; set; }
        public DateTime? DtCriacao { get; set; }
        public virtual ICollection<OcorrenciaWizard02> OcorrenciaWizard02s { get; set; }

        public ClienteWizard02()
        {
            OcorrenciaWizard02s = new Collection<OcorrenciaWizard02>();
            Ativo = true;
            DtCriacao = DateTime.Now;
        }
    }
}
