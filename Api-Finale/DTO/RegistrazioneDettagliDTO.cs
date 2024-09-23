namespace Api_Finale.DTO
{
    namespace Api_Finale.DTO
    {
        public class RegistrazioneDettagliDTO
        {
            public int Id { get; set; }
            public DateTime DataRegistrazione { get; set; }
            public decimal CostoTotale { get; set; }

            // Utente associato
            public UtenteDTO Utente { get; set; }

            // Evento associato
            public EventoDTO Evento { get; set; }

            // Personaggio associato, se presente
            public PersonaggioDTO? Personaggio { get; set; }

            // Servizi associati
            public List<ServizioDTO> Servizi { get; set; }
        }

        
    }
}