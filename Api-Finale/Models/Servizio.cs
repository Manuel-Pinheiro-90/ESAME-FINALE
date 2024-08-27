using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Finale.Models
{
    public class Servizio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nome { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public required string Descrizione { get; set; }

        [Required]
        public decimal Costo { get; set; }

       
        public List<RegistrazioneServizio> RegistrazioniServizi { get; set; } = [];
    }
}
