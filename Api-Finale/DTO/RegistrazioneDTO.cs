namespace Api_Finale.DTO
{
  
        public class RegistrazioneDTO
        {
            public int Id { get; set; }
            public DateTime DataRegistrazione { get; set; }
            public decimal CostoTotale { get; set; }
            public int EventoId { get; set; }  // ID dell'evento a cui vuoi registrarti

            // Informazioni sul personaggio associato (se esiste)
            public int? PersonaggioId { get; set; }

            // Lista degli ID dei servizi opzionali selezionati
            public List<int>? ServiziIds { get; set; }
        }
    
}
