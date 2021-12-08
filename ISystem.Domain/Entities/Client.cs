using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities
{
    [Table("AspNetClients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string ClientKey { get; set; }
    }
}
