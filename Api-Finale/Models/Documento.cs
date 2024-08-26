using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Finale.Models
{
    public class Documento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string NomeDocumento { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public required string Percorso { get; set; }

        // Relazione con Evento
        [Required]
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }
    }
}
