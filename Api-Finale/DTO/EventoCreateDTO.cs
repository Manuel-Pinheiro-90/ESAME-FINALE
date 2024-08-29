namespace Api_Finale.DTO
{
    public class EventoCreateDTO
    {
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public string Luogo { get; set; }
        public int NumeroPartecipantiMax { get; set; }

        public IFormFile ImmagineFile { get; set; }
    }
}
