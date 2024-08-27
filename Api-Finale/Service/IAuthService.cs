using Api_Finale.Models;

namespace Api_Finale.Service
{
    public interface IAuthService
    {
        string Login(string username, string password);
        Utente Register(Utente utente);
    }
}
