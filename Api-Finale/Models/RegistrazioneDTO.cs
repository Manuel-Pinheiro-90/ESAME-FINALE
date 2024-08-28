namespace Api_Finale.Models
{
    public class RegistrazioneDTO
    {
        public int Id { get; set; }
        public DateTime DataRegistrazione { get; set; }
        public decimal CostoTotale { get; set; }
        public int EventoId { get; set; }  // ID dell'evento a cui vuoi registrarti
        public int? PersonaggioId { get; set; }  // ID del personaggio (opzionale)
        // elenco servizzi associati
        public List<int> ServiziIds { get; set; } = [];
    }
}
