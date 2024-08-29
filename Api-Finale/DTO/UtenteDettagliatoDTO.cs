namespace Api_Finale.DTO
{
    public class UtenteDettagliatoDTO
    {
       
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
        public string Foto { get; set; }

        public List<RuoloDTO> Ruoli { get; set; }
            public List<PersonaggioDTO> Personaggi { get; set; }
        
    }
}
