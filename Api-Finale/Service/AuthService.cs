using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Finale.Service
{
    public class AuthService :IAuthService
    {

        private readonly DataContext _context;
        private readonly IpasswordEncoder _passwordEncoder;

        public AuthService(DataContext context, IpasswordEncoder passwordEncoder)
        {
            _context = context;
            _passwordEncoder = passwordEncoder;
        }

        public Utente Login(string username, string password)
        {
            var encodedPassword = _passwordEncoder.Encode(password);
            var utente = _context.Utenti.Include(u => u.Ruoli)
                                        .FirstOrDefault(u => u.Nome == username && u.PasswordHash == encodedPassword);
            return utente;
        }

        public Utente Register(Utente utente)
        {
            utente.PasswordHash = _passwordEncoder.Encode(utente.PasswordHash);

            utente.DataRegistrazione = DateTime.Now;

            
            var userRole = _context.Ruoli.FirstOrDefault(r => r.Nome == "Utente");
            if (userRole != null)
            {
                utente.Ruoli.Add(userRole);
            }

            _context.Utenti.Add(utente);
            _context.SaveChanges();
            return utente;
        }


    }
}
