using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISystem.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(300)]
        public string Nome { get; set; }
    }
}
