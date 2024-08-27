using Api_Finale.Models;

namespace Api_Finale.Service
{
    public interface IAuthService
    {
        Utente Login(string username, string password);
        Utente Register(Utente utente);
    }
}
