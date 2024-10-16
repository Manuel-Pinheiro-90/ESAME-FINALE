﻿using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Finale.Service
{
    public class UtenteService
    {
        private readonly DataContext _context;
        private readonly IpasswordEncoder _passwordEncoder;

        public UtenteService(DataContext context, IpasswordEncoder passwordEncoder)
        {
            _context = context;
            _passwordEncoder = passwordEncoder;
        }
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public async Task<Utente> CreateUtente(Utente utente)
        {
            // Verifica se l'email è già in uso
            if (await _context.Utenti.AnyAsync(u => u.Email == utente.Email))
            {
                throw new Exception("Email già in uso.");
            }

            // Codifica la password
            utente.PasswordHash = _passwordEncoder.Encode(utente.PasswordHash);

            // Imposta la data di registrazione
            utente.DataRegistrazione = DateTime.Now;

            // Assegna il ruolo in base al parametro fornito (se nessun ruolo è specificato, assegna il ruolo 'Utente')
            if (utente.Ruoli == null || !utente.Ruoli.Any())
            {
                var userRole = await _context.Ruoli.FirstOrDefaultAsync(r => r.Nome == "Utente");
                if (userRole != null)
                {
                    utente.Ruoli.Add(userRole);
                }
            }

            _context.Utenti.Add(utente);
            await _context.SaveChangesAsync();
            return utente;
        }
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            // Verifica se la password è stata modificata
            if (!string.IsNullOrEmpty(utente.PasswordHash) && utente.PasswordHash != existingUser.PasswordHash)
            {
                existingUser.PasswordHash = _passwordEncoder.Encode(utente.PasswordHash);
            }

            // Aggiorna l'immagine se fornita
            if (!string.IsNullOrEmpty(utente.Foto))
            {
                existingUser.Foto = utente.Foto;
            }

            await _context.SaveChangesAsync();
            return existingUser;
        }
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Metodo per eliminare un utente
        public async Task DeleteUtente(int id)
        {var registrazioni = await _context.Registrazioni.Where(r => r.UtenteId == id).ToListAsync();
            if(registrazioni.Any())
            
            {
                _context.Registrazioni.RemoveRange(registrazioni);
            }

            var utente = await _context.Utenti.FindAsync(id);
            if (utente == null)
            {
                throw new Exception("Utente non trovato.");
            }

            _context.Utenti.Remove(utente);
            await _context.SaveChangesAsync();
        }
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Metodo per ottenere un utente per ID
        public async Task<Utente> GetUtente(int id)
        {
            var utente = await _context.Utenti
                .Include(u=> u.Ruoli)
                .Include(u=> u.Registrazioni) ///////modifica 
                .ThenInclude(r => r.Evento)
                .Include(u => u.Registrazioni)
            .ThenInclude(r => r.RegistrazioniServizi)  // Include la relazione tra registrazione e servizi
                .ThenInclude(rs => rs.Servizio)
                .Include(u => u.Personaggi)  
                .FirstOrDefaultAsync(u => u.Id == id);

            if (utente == null)
            {
                throw new Exception("Utente non trovato.");
            }

            return utente;
        }
        public async Task<List<Utente>> GetAllUtenti()
        {
            return await _context.Utenti
                .Include(u => u.Ruoli) 
                .Include(u => u.Personaggi) 
                .ToListAsync();
        }


        // Metodo per convertire un'immagine in stringa Base64
        public string ConvertImage(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
       
        
        }

        ////////////////////////////////////////////////////
        public async Task<Ruolo> GetRuoloByName(string nome)
        {
            return await _context.Ruoli.FirstOrDefaultAsync(r => r.Nome == nome);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



    }
}
