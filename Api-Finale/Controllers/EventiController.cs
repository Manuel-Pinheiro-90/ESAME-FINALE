using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventiController : ControllerBase
    {
        private readonly DataContext _context;

        public EventiController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Eventi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventi()
        {
            return await _context.Eventi.ToListAsync();
        }

        // GET: api/Eventi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
            var evento = await _context.Eventi.FindAsync(id);

            if (evento == null)
            {
                return NotFound(new { Message = "Evento non trovato." });
            }

            return evento;
        }

        // POST: api/Eventi
        [HttpPost]
        public async Task<ActionResult<Evento>> CreateEvento(Evento evento)
        {
            if (evento == null)
            {
                return BadRequest(new { Message = "I dati dell'evento non sono validi." });
            }

            _context.Eventi.Add(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvento), new { id = evento.Id }, evento);
        }

        // PUT: api/Eventi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, Evento evento)
        {
            if (id != evento.Id)
            {
                return BadRequest(new { Message = "ID dell'evento non corrisponde." });
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

        // DELETE: api/Eventi/5
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
