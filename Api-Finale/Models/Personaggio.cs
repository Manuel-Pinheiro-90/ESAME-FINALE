using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api_Finale.Models
{
    public class Personaggio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nome { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public required string Descrizione { get; set; }

        // Relazione con Utente
       [Required]
        public int UtenteId { get; set; }
        [JsonIgnore] //risolvi 
        [ForeignKey("UtenteId")]
       public Utente? Utente { get; set; }

        // Relazione opzionale con Evento
       public int? EventoId { get; set; }
        [ForeignKey("EventoId")]
        public Evento? Evento { get; set; }
    }
}
