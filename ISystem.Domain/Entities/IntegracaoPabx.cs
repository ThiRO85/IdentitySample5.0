using ISystem.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities
{
    public class IntegracaoPabx
    {
        public int Id { get; set; }
        public string Ramal { get; set; }
        public string Senha { get; set; }
        public string Posicao { get; set; }
        public bool Ativo { get; set; }
        public Mantenedor Mantenedor { get; set; }

        [NotMapped]
        public IApplicationUser Users { get; set; } //[NotMapped] incluído para conseguir gerar as tabelas. Não existia originalmente!
    }
}
