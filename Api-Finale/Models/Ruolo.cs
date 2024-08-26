using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Finale.Models
{
    public class Ruolo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nome { get; set; }

        // Relazione molti-a-molti con Utenti
        public List<Utente> Utenti { get; set; } = [];
    }
}
