using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Api_Finale.DTO;
using Api_Finale.DTO.Api_Finale.DTO;

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
        public async Task<ActionResult<IEnumerable<RegistrazioneDettagliDTO>>> GetRegistrazioni()
        {
            var registrazioni = await _context.Registrazioni
                .Include(r => r.Evento)
                .Include(r => r.Utente)
                .Include(r => r.Personaggio)
                .Include(r => r.RegistrazioniServizi)
                    .ThenInclude(rs => rs.Servizio)
                .Select(r => new RegistrazioneDettagliDTO
                {
                    Id = r.Id,
                    DataRegistrazione = r.DataRegistrazione,
                    CostoTotale = r.CostoTotale + r.RegistrazioniServizi.Sum(rs => rs.Servizio.Costo),
                    Utente = new UtenteDTO
                    {
                        Id = r.Utente.Id,
                        Nome = r.Utente.Nome,
                        Email = r.Utente.Email
                    },
                    Evento = new EventoDTO
                    {
                        Id = r.Evento.Id,
                        Titolo = r.Evento.Titolo,
                        Descrizione = r.Evento.Descrizione,
                        DataInizio = r.Evento.DataInizio,
                        DataFine = r.Evento.DataFine,
                        Luogo = r.Evento.Luogo,
                        NumeroPartecipantiMax = r.Evento.NumeroPartecipantiMax,
                        ImmagineEvento = r.Evento.ImmagineEvento
                    },
                    Personaggio = r.Personaggio != null ? new PersonaggioDTO
                    {
                        Id = r.Personaggio.Id,
                        Nome = r.Personaggio.Nome,
                        Descrizione = r.Personaggio.Descrizione
                    } : null,
                    Servizi = r.RegistrazioniServizi.Select(rs => new ServizioDTO
                    {
                        Id = rs.Servizio.Id,
                        Nome = rs.Servizio.Nome,
                        Descrizione = rs.Servizio.Descrizione,
                        Costo = rs.Servizio.Costo
                    }).ToList()
                })
                .ToListAsync();

            return Ok(registrazioni);
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Authorize]
        [HttpGet("MieRegistrazioni")]
        public async Task<ActionResult<IEnumerable<RegistrazioneDettagliDTO>>> GetMieRegistrazioni()
        {
            // Recupera l'ID dell'utente autenticato dal token JWT
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { Message = "Utente non autenticato." });
            }

            var registrazioni = await _context.Registrazioni
                .Include(r => r.Evento)
                .Include(r => r.Utente)
                .Include(r => r.Personaggio)
                .Include(r => r.RegistrazioniServizi)
                    .ThenInclude(rs => rs.Servizio)
                .Where(r => r.UtenteId == int.Parse(userId))  // Filtra per l'utente loggato
                .Select(r => new RegistrazioneDettagliDTO
                {
                    Id = r.Id,
                    DataRegistrazione = r.DataRegistrazione,
                    CostoTotale = r.CostoTotale + r.RegistrazioniServizi.Sum(rs => rs.Servizio.Costo),
                    Utente = new UtenteDTO
                    {
                        Id = r.Utente.Id,
                        Nome = r.Utente.Nome,
                        Email = r.Utente.Email
                    },
                    Evento = new EventoDTO
                    {
                        Id = r.Evento.Id,
                        Titolo = r.Evento.Titolo,
                        Descrizione = r.Evento.Descrizione,
                        DataInizio = r.Evento.DataInizio,
                        DataFine = r.Evento.DataFine,
                        Luogo = r.Evento.Luogo,
                        NumeroPartecipantiMax = r.Evento.NumeroPartecipantiMax,
                        ImmagineEvento = r.Evento.ImmagineEvento
                    },
                    Personaggio = r.Personaggio != null ? new PersonaggioDTO
                    {
                        Id = r.Personaggio.Id,
                        Nome = r.Personaggio.Nome,
                        Descrizione = r.Personaggio.Descrizione
                    } : null,
                    Servizi = r.RegistrazioniServizi.Select(rs => new ServizioDTO
                    {
                        Id = rs.Servizio.Id,
                        Nome = rs.Servizio.Nome,
                        Descrizione = rs.Servizio.Descrizione,
                        Costo = rs.Servizio.Costo
                    }).ToList()
                })
                .ToListAsync();

            return Ok(registrazioni);
        }


        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Registrazioni/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrazioneDettagliDTO>> GetRegistrazione(int id)
        {
            var registrazione = await _context.Registrazioni
                .Include(r => r.Utente)
                .Include(r => r.Personaggio)
                .Include(r => r.RegistrazioniServizi)
                    .ThenInclude(rs => rs.Servizio)
                .Include(r => r.Evento)
                .Select(r => new RegistrazioneDettagliDTO
                {
                    Id = r.Id,
                    DataRegistrazione = r.DataRegistrazione,
                    CostoTotale = r.CostoTotale + r.RegistrazioniServizi.Sum(rs => rs.Servizio.Costo),
                    Utente = new UtenteDTO
                    {
                        Id = r.Utente.Id,
                        Nome = r.Utente.Nome,
                        Email = r.Utente.Email
                    },
                    Evento = new EventoDTO
                    {
                        Id = r.Evento.Id,
                        Titolo = r.Evento.Titolo,
                        Descrizione = r.Evento.Descrizione,
                        DataInizio = r.Evento.DataInizio,
                        DataFine = r.Evento.DataFine,
                        Luogo = r.Evento.Luogo,
                        NumeroPartecipantiMax = r.Evento.NumeroPartecipantiMax,
                        ImmagineEvento = r.Evento.ImmagineEvento
                    },
                    Personaggio = r.Personaggio != null ? new PersonaggioDTO
                    {
                        Id = r.Personaggio.Id,
                        Nome = r.Personaggio.Nome,
                        Descrizione = r.Personaggio.Descrizione
                    } : null,
                    Servizi = r.RegistrazioniServizi.Select(rs => new ServizioDTO
                    {
                        Id = rs.Servizio.Id,
                        Nome = rs.Servizio.Nome,
                        Descrizione = rs.Servizio.Descrizione,
                        Costo = rs.Servizio.Costo
                    }).ToList()
                })
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registrazione == null)
            {
                return NotFound(new { Message = "Registrazione non trovata." });
            }

            return Ok(registrazione);
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Registrazioni
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RegistrazioneDettagliDTO>> CreateRegistrazione([FromBody] RegistrazioneDTO registrazioneDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { Message = "Utente non autenticato." });
            }

            var registrazione = new Registrazione
            {
                DataRegistrazione = DateTime.Now, ///ho modificato questo controllare se funziona
                CostoTotale = registrazioneDto.CostoTotale,
                UtenteId = int.Parse(userId),
                EventoId = registrazioneDto.EventoId,
                PersonaggioId = registrazioneDto.PersonaggioId
            };

            _context.Registrazioni.Add(registrazione);
            await _context.SaveChangesAsync();

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

            var registrazioneDettagliDTO = await _context.Registrazioni
                .Include(r => r.Utente)
                .Include(r => r.Personaggio)
                .Include(r => r.RegistrazioniServizi)
                    .ThenInclude(rs => rs.Servizio)
                .Include(r => r.Evento)
                .Where(r => r.Id == registrazione.Id)
                .Select(r => new RegistrazioneDettagliDTO
                {
                    Id = r.Id,
                    DataRegistrazione = r.DataRegistrazione,
                    CostoTotale = r.CostoTotale + r.RegistrazioniServizi.Sum(rs => rs.Servizio.Costo),
                    Utente = new UtenteDTO
                    {
                        Id = r.Utente.Id,
                        Nome = r.Utente.Nome,
                        Email = r.Utente.Email
                    },
                    Evento = new EventoDTO
                    {
                        Id = r.Evento.Id,
                        Titolo = r.Evento.Titolo,
                        Descrizione = r.Evento.Descrizione,
                        DataInizio = r.Evento.DataInizio,
                        DataFine = r.Evento.DataFine,
                        Luogo = r.Evento.Luogo,
                        NumeroPartecipantiMax = r.Evento.NumeroPartecipantiMax,
                        ImmagineEvento = r.Evento.ImmagineEvento
                    },
                    Personaggio = r.Personaggio != null ? new PersonaggioDTO
                    {
                        Id = r.Personaggio.Id,
                        Nome = r.Personaggio.Nome,
                        Descrizione = r.Personaggio.Descrizione
                    } : null,
                    Servizi = r.RegistrazioniServizi.Select(rs => new ServizioDTO
                    {
                        Id = rs.Servizio.Id,
                        Nome = rs.Servizio.Nome,
                        Descrizione = rs.Servizio.Descrizione,
                        Costo = rs.Servizio.Costo
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetRegistrazione), new { id = registrazione.Id }, registrazioneDettagliDTO);
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Registrazioni/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistrazione(int id, [FromBody] RegistrazioneDTO registrazioneDto)
        {
            if (id != registrazioneDto.Id)
            {
                return BadRequest(new { Message = "ID della registrazione non corrisponde." });
            }

            var registrazione = await _context.Registrazioni.FindAsync(id);
            if (registrazione == null)
            {
                return NotFound(new { Message = "Registrazione non trovata." });
            }

            registrazione.DataRegistrazione = registrazioneDto.DataRegistrazione;
            registrazione.CostoTotale = registrazioneDto.CostoTotale;
            registrazione.EventoId = registrazioneDto.EventoId;
            registrazione.PersonaggioId = registrazioneDto.PersonaggioId;

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
