using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api_Finale.Models
{
    public class Registrazione
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime DataRegistrazione { get; set; }

        [Required]
        public decimal CostoTotale { get; set; }

        // Relazione con Utente
        [Required]
        public int UtenteId { get; set; }
        [ForeignKey("UtenteId")]
        public Utente Utente { get; set; }

        [Required]
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        [JsonIgnore]
        public Evento Evento { get; set; }

        // Relazione opzionale con Personaggio
        public int? PersonaggioId { get; set; }
        [ForeignKey("PersonaggioId")]
        public Personaggio Personaggio { get; set; }

        // Relazione molti-a-molti con Servizi tramite tabella associativa
       
        public List<RegistrazioneServizio> RegistrazioniServizi { get; set; } = [];
    }
}
