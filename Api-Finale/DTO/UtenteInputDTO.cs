namespace Api_Finale.DTO
{
    public class UtenteInputDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
