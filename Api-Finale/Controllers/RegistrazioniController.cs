using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrazioniController : ControllerBase
    {
        private readonly DataContext _context;

        public RegistrazioniController(DataContext context)
        {
            _context = context;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Registrazioni
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registrazione>>> GetRegistrazioni()
        {
            return await _context.Registrazioni
                .Include(r => r.Evento)
                .Include(r => r.Utente)
                .Include(r => r.Personaggio)
                .ToListAsync();
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Registrazioni/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRegistrazione(int id)
        {
            var registrazione = await _context.Registrazioni
                .Include(r => r.Utente)
                .Include(r => r.Personaggio)
                .Include(r => r.RegistrazioniServizi)
                    .ThenInclude(rs => rs.Servizio)
                .Include(r => r.Evento)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registrazione == null)
            {
                return NotFound(new { Message = "Registrazione non trovata." });
            }

            // Seleziona solo i campi necessari per evitare cicli di riferimento
            return Ok(new
            {
                registrazione.Id,
                registrazione.DataRegistrazione,
                registrazione.CostoTotale,
                Utente = new
                {
                    registrazione.Utente.Id,
                    registrazione.Utente.Nome,
                    registrazione.Utente.Email
                },
                Evento = new
                {
                    registrazione.Evento.Id,
                    registrazione.Evento.Titolo,
                    registrazione.Evento.DataInizio,
                    registrazione.Evento.Luogo
                },
                Personaggio = registrazione.Personaggio != null ? new
                {
                    registrazione.Personaggio.Id,
                    registrazione.Personaggio.Nome,
                    registrazione.Personaggio.Descrizione
                } : null,
                Servizi = registrazione.RegistrazioniServizi.Select(rs => new
                {
                    rs.Servizio.Id,
                    rs.Servizio.Nome,
                    rs.Servizio.Costo
                }).ToList()
            });
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Registrazioni
        [Authorize]
        [HttpPost]
public async Task<ActionResult<Registrazione>> CreateRegistrazione([FromBody] RegistrazioneDTO registrazioneDto)
{
    // Recupera l'ID dell'utente autenticato dal token JWT
    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    if (userId == null)
    {
        return Unauthorized(new { Message = "Utente non autenticato." });
    }

    // Crea una nuova registrazione associata all'utente autenticato
    var registrazione = new Registrazione
    {
        DataRegistrazione = registrazioneDto.DataRegistrazione,
        CostoTotale = registrazioneDto.CostoTotale,
        UtenteId = int.Parse(userId),  // Imposta l'ID utente automaticamente
        EventoId = registrazioneDto.EventoId,
        PersonaggioId = registrazioneDto.PersonaggioId
    };

    _context.Registrazioni.Add(registrazione);
    await _context.SaveChangesAsync();

    // Associa i servizi selezionati alla registrazione
    foreach (var servizioId in registrazioneDto.ServiziIds)
    {
        var registrazioneServizio = new RegistrazioneServizio
        {
            RegistrazioneId = registrazione.Id,
            ServizioId = servizioId
        };
        _context.RegistrazioniServizi.Add(registrazioneServizio);
    }
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetRegistrazione), new { id = registrazione.Id }, registrazione);
}

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Registrazioni/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistrazione(int id, Registrazione registrazione)
        {
            if (id != registrazione.Id)
            {
                return BadRequest(new { Message = "ID della registrazione non corrisponde." });
            }

            _context.Entry(registrazione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrazioneExists(id))
                {
                    return NotFound(new { Message = "Registrazione non trovata." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/Registrazioni/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistrazione(int id)
        {
            var registrazione = await _context.Registrazioni.FindAsync(id);
            if (registrazione == null)
            {
                return NotFound(new { Message = "Registrazione non trovata." });
            }

            _context.Registrazioni.Remove(registrazione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrazioneExists(int id)
        {
            return _context.Registrazioni.Any(r => r.Id == id);
        }
    }
}
