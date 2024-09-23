namespace Api_Finale.DTO
{
    public class EventoDTO
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public string Luogo { get; set; }
        public int NumeroPartecipantiMax { get; set; }
        public string? ImmagineEvento { get; set; }

        //  conteggio delle registrazioni
        public int NumeroRegistrazioni { get; set; }
        // Conteggio dei documenti e personaggi
        public int NumeroDocumenti { get; set; }
        public int NumeroPersonaggi { get; set; }

        // Lista dei nomi degli utenti registrati
        public List<string> NomiUtentiRegistrati { get; set; } = new List<string>();
    }
}
