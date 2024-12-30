using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("MarcasAutos")]
    public class MarcasAutosEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = default!;

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [MaxLength(150)]
        public string? Pais { get; set; }
    }
}
