using System.ComponentModel.DataAnnotations;

namespace Api_Finale.Models
{
    public class RegistrazioneServizio
    {
        [Key]
        public int RegistrazioneId { get; set; }
        public Registrazione Registrazione { get; set; }

        [Key]
        public int ServizioId { get; set; }
        public Servizio Servizio { get; set; }
    }
}
