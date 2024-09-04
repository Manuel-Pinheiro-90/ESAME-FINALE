using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Api_Finale.DTO;
using Api_Finale.Service;
namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventiController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UtenteService _utenteService;

        public EventiController(DataContext context, UtenteService utenteService)
        {
            _context = context;
            _utenteService = utenteService;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Eventi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetEventi()
        {
            var eventi = await _context.Eventi
                .Include(e => e.Registrazioni)
                .ThenInclude(r => r.Utente)  // Includi gli utenti associati alle registrazioni
                .Include(e => e.Documenti)
                .Include(e => e.Personaggi)
                .Select(e => new EventoDTO
                {
                    Id = e.Id,
                    Titolo = e.Titolo,
                    Descrizione = e.Descrizione,
                    DataInizio = e.DataInizio,
                    DataFine = e.DataFine,
                    Luogo = e.Luogo,
                    NumeroPartecipantiMax = e.NumeroPartecipantiMax,
                    ImmagineEvento = e.ImmagineEvento,
                    NumeroRegistrazioni = e.Registrazioni.Count(),
                    NumeroDocumenti = e.Documenti.Count(),
                    NumeroPersonaggi = e.Personaggi.Count(),
                    // Aggiungi i nomi degli utenti registrati
                    NomiUtentiRegistrati = e.Registrazioni.Select(r => r.Utente.Nome).ToList()
                })
                .ToListAsync();

            return Ok(eventi);
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Eventi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDTO>> GetEvento(int id)
        {
            var evento = await _context.Eventi
                .Include(e => e.Registrazioni)
                .ThenInclude(r => r.Utente)  // Includi gli utenti associati alle registrazioni
                .Include(e => e.Documenti)
                .Include(e => e.Personaggi)
                .Where(e => e.Id == id)
                .Select(e => new EventoDTO
                {
                    Id = e.Id,
                    Titolo = e.Titolo,
                    Descrizione = e.Descrizione,
                    DataInizio = e.DataInizio,
                    DataFine = e.DataFine,
                    Luogo = e.Luogo,
                    NumeroPartecipantiMax = e.NumeroPartecipantiMax,
                    ImmagineEvento = e.ImmagineEvento,
                    NumeroRegistrazioni = e.Registrazioni.Count(),
                    NumeroDocumenti = e.Documenti.Count(),
                    NumeroPersonaggi = e.Personaggi.Count(),
                    // Aggiungi i nomi degli utenti registrati
                    NomiUtentiRegistrati = e.Registrazioni.Select(r => r.Utente.Nome).ToList()
                })
                .FirstOrDefaultAsync();

            if (evento == null)
            {
                return NotFound(new { Message = "Evento non trovato." });
            }

            return Ok(evento);
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Eventi
      [Authorize]
        [HttpPost]
        public async Task<ActionResult<Evento>> CreateEvento([FromForm] EventoCreateDTO eventoDTO)
        {
            if (eventoDTO == null)
            {
                return BadRequest(new { Message = "I dati dell'evento non sono validi." });
            }

            var evento = new Evento
            {
                Titolo = eventoDTO.Titolo,
                Descrizione = eventoDTO.Descrizione,
                DataInizio = eventoDTO.DataInizio,
                DataFine = eventoDTO.DataFine,
                Luogo = eventoDTO.Luogo,
                NumeroPartecipantiMax = eventoDTO.NumeroPartecipantiMax



            };
            // Se un'immagine è stata caricata, converti l'immagine in Base64 e salvala nel modello
            if (eventoDTO.ImmagineFile != null && eventoDTO.ImmagineFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await eventoDTO.ImmagineFile.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    evento.ImmagineEvento = Convert.ToBase64String(fileBytes);  // Salva come stringa Base64
                }
            }

            // Salva l'evento nel database
            _context.Eventi.Add(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvento), new { id = evento.Id }, evento);




        }


        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetEventImage(int id)
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null || string.IsNullOrEmpty(evento.ImmagineEvento))
            {
                return NotFound();
            }

            byte[] imageBytes = Convert.FromBase64String(evento.ImmagineEvento);
            // Determina il MIME type corretto in base al prefisso della stringa Base64
            string mimeType;
            if (evento.ImmagineEvento.StartsWith("/9j/")) // JPEG
            {
                mimeType = "image/jpeg";
            }
            else if (evento.ImmagineEvento.StartsWith("iVBORw0KGgo")) // PNG
            {
                mimeType = "image/png";
            }
            else if (evento.ImmagineEvento.StartsWith("R0lGOD")) // GIF
            {
                mimeType = "image/gif";
            }
            else
            {
                mimeType = "application/octet-stream"; // MIME type generico
            }

            return File(imageBytes, mimeType);
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PUT: api/Eventi/5
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, [FromForm] EventoCreateDTO eventoDto)
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null)
            {
                return NotFound(new { Message = "Evento non trovato." });
            }

            evento.Titolo = eventoDto.Titolo;
            evento.Descrizione = eventoDto.Descrizione;
            evento.DataInizio = eventoDto.DataInizio;
            evento.DataFine = eventoDto.DataFine;
            evento.Luogo = eventoDto.Luogo;
            evento.NumeroPartecipantiMax = eventoDto.NumeroPartecipantiMax;

            // Gestione dell'immagine
            if (eventoDto.ImmagineFile != null && eventoDto.ImmagineFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await eventoDto.ImmagineFile.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    evento.ImmagineEvento = Convert.ToBase64String(fileBytes);
                }
            }

            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
                {
                    return NotFound(new { Message = "Evento non trovato." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/Eventi/5
    [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null)
            {
                return NotFound(new { Message = "Evento non trovato." });
            }

            _context.Eventi.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoExists(int id)
        {
            return _context.Eventi.Any(e => e.Id == id);
        }


    }
}
