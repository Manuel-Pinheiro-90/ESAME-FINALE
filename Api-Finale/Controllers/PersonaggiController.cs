using Api_Finale.Context;
using Api_Finale.DTO;
using Api_Finale.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaggiController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonaggiController(DataContext context)
        {
            _context = context;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Personaggi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaggioDTO>>> GetPersonaggi()
        {
            var personaggi = await _context.Personaggi
                .Select(p => new PersonaggioDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descrizione = p.Descrizione
                })
                .ToListAsync();

            return Ok(personaggi);
        }


        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Authorize]
        [HttpGet("personaggi")]
       
        public async Task<ActionResult<IEnumerable<PersonaggioDTO>>> GetPersonaggiLog()
        {
            // Ottieni l'ID dell'utente dal token JWT
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { Message = "Utente non autenticato." });
            }

            // Filtra i personaggi in base all'utente loggato
            var personaggi = await _context.Personaggi
                .Where(p => p.UtenteId == int.Parse(userId))
                .Select(p => new PersonaggioDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descrizione = p.Descrizione
                })
                .ToListAsync();

            return Ok(personaggi);
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Authorize]
        [HttpGet("personaggio/{id}")]
        public async Task<ActionResult<PersonaggioDTO>> GetPersonaggioLog(int id)
        {
            // Ottieni l'ID dell'utente dal token JWT
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { Message = "Utente non autenticato." });
            }

            var personaggio = await _context.Personaggi
                .Where(p => p.Id == id && p.UtenteId == int.Parse(userId))
                .Select(p => new PersonaggioDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descrizione = p.Descrizione
                })
                .FirstOrDefaultAsync();

            if (personaggio == null)
            {
                return NotFound(new { Message = "Personaggio non trovato o non autorizzato." });
            }

            return Ok(personaggio);
        }






        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Personaggi/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaggioDTO>> GetPersonaggio(int id)
        {
            var personaggio = await _context.Personaggi
                .Where(p => p.Id == id)
                .Select(p => new PersonaggioDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descrizione = p.Descrizione
                })
                .FirstOrDefaultAsync();

            if (personaggio == null)
            {
                return NotFound(new { Message = "Personaggio non trovato." });
            }

            return Ok(personaggio);
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Personaggi
        [HttpPost]
        
        public async Task<ActionResult<PersonaggioDTO>> CreatePersonaggio([FromBody] PersonaggioDTO personaggioDto)
        {
            // Recupera l'ID dell'utente autenticato dal token JWT
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { Message = "Utente non autenticato." });
            }

            var personaggio = new Personaggio
            {
                Nome = personaggioDto.Nome,
                Descrizione = personaggioDto.Descrizione,
                UtenteId = int.Parse(userId)
            };

            _context.Personaggi.Add(personaggio);
            await _context.SaveChangesAsync();

            // Aggiorna il DTO con l'ID generato dal database
            personaggioDto.Id = personaggio.Id;

            return CreatedAtAction(nameof(GetPersonaggio), new { id = personaggio.Id }, personaggioDto);
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Personaggi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonaggio(int id, PersonaggioDTO personaggioDto)
        {
            if (id != personaggioDto.Id)
            {
                return BadRequest(new { Message = "ID del personaggio non corrisponde." });
            }

            var personaggio = await _context.Personaggi.FindAsync(id);
            if (personaggio == null)
            {
                return NotFound(new { Message = "Personaggio non trovato." });
            }

            // Verifica che l'utente sia il proprietario del personaggio
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || personaggio.UtenteId != int.Parse(userId))
            {
                return Unauthorized(new { Message = "Non sei autorizzato a modificare questo personaggio." });
            }

            personaggio.Nome = personaggioDto.Nome;
            personaggio.Descrizione = personaggioDto.Descrizione;

            _context.Entry(personaggio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaggioExists(id))
                {
                    return NotFound(new { Message = "Personaggio non trovato." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaggio(int id)
        {
            // Recupera l'ID dell'utente autenticato dal token JWT
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { Message = "Utente non autenticato." });
            }

            // Trova il personaggio da eliminare
            var personaggio = await _context.Personaggi.FirstOrDefaultAsync(p => p.Id == id && p.UtenteId == int.Parse(userId));

            if (personaggio == null)
            {
                return NotFound(new { Message = "Personaggio non trovato o non autorizzato." });
            }

            // Rimuovi il personaggio dal contesto
            _context.Personaggi.Remove(personaggio);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool PersonaggioExists(int id)
        {
            return _context.Personaggi.Any(p => p.Id == id);
        }



    }
}
