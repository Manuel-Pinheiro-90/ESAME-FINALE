using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Finale.Service
{
    public class UtenteService
    {


        private readonly DataContext _context;

        public UtenteService(DataContext context)
        {
            _context = context;
        }

        // Metodo per creare un nuovo utente con validazione
        public async Task<Utente> CreateUtente(Utente utente)
        {
            // Verifica se l'email è già in uso
            if (await _context.Utenti.AnyAsync(u => u.Email == utente.Email))
            {
                throw new Exception("Email già in uso.");
            }

            _context.Utenti.Add(utente);
            await _context.SaveChangesAsync();
            return utente;
        }

        // Metodo per aggiornare un utente
        public async Task<Utente> UpdateUtente(int id, Utente utente)
        {
            var existingUser = await _context.Utenti.FindAsync(id);
            if (existingUser == null)
            {
                throw new Exception("Utente non trovato.");
            }

            // Logica di aggiornamento
            existingUser.Nome = utente.Nome;
            existingUser.Email = utente.Email;
            existingUser.PasswordHash = utente.PasswordHash;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        // Metodo per eliminare un utente
        public async Task DeleteUtente(int id)
        {
            var utente = await _context.Utenti.FindAsync(id);
            if (utente == null)
            {
                throw new Exception("Utente non trovato.");
            }

            _context.Utenti.Remove(utente);
            await _context.SaveChangesAsync();
        }

        // Metodo per ottenere un utente per ID
        public async Task<Utente> GetUtente(int id)
        {
            var utente = await _context.Utenti.FindAsync(id);
            if (utente == null)
            {
                throw new Exception("Utente non trovato.");
            }

            return utente;
        }

        // Metodo per ottenere tutti gli utenti con paginazione
        public async Task<(IEnumerable<Utente>, int)> GetUtenti(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Utenti.CountAsync();
            var utenti = await _context.Utenti
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (utenti, totalRecords);
        }
    }
}
