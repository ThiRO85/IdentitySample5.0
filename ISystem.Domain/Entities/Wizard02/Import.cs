using System;

namespace ISystem.Domain.Entities.Wizard02
{
    public class Import
    {
        public Import()
        {
            FilaId = 3;
            StatusId = 2;
            UsersId = "4d03dcf8-bb2b-41d0-9b2b-e63ce100759d";
            DtCriacao = DateTime.Now;
        }

        public int? CodCliente { get; set; }
        public string Uf { get; set; }
        public string Unidade { get; set; }
        public string CodUnidade { get; set; }
        public string Idioma { get; set; }
        public bool EnviadoSponte { get; set; }
        public DateTime? DtCriacaoCliente { get; set; }
        public string Nome { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Telefone3 { get; set; }
        public string Telefone4 { get; set; }
        public int FilaId { get; set; }
        public int StatusId { get; set; }
        public string UsersId { get; set; }
        public string NomeCampanha { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public DateTime DtCriacao { get; set; }
    }
}
