namespace Api_Finale.DTO
{
    public class UtenteTokenDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<RuoloDTO> Ruoli { get; set; } = new List<RuoloDTO>();
    }
}
