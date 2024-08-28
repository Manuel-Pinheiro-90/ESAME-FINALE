using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api_Finale.Models
{
    public class Evento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Titolo { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public required string Descrizione { get; set; }

        [Required]
        public DateTime DataInizio { get; set; }

        [Required]
        public DateTime DataFine { get; set; }

        [Required]
        [StringLength(200)]
        public required string Luogo { get; set; }

        [Required]
        public int NumeroPartecipantiMax { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? ImmagineEvento { get; set; }

        
        
        public List<Registrazione> Registrazioni { get; set; } = [];
        public List<Documento> Documenti { get; set; } = [];
        public List<Personaggio> Personaggi { get; set; } = [];
    }
}
