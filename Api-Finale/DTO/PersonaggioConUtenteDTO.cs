namespace Api_Finale.DTO
{
    public class PersonaggioConUtenteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        // Dettagli del creatore (utente)
        public string CreatoreNome { get; set; }
        public string CreatoreFoto { get; set; }
    }
}
