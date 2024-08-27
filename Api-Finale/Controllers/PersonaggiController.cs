using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<Personaggio>>> GetPersonaggi()
        {
            return await _context.Personaggi
                .Include(p => p.Utente)
                .Include(p => p.Evento)
                .ToListAsync();
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Personaggi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personaggio>> GetPersonaggio(int id)
        {
            var personaggio = await _context.Personaggi
                .Include(p => p.Utente)
                .Include(p => p.Evento)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (personaggio == null)
            {
                return NotFound(new { Message = "Personaggio non trovato." });
            }

            return personaggio;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Personaggi
        [HttpPost]
        public async Task<ActionResult<Personaggio>> CreatePersonaggio(Personaggio personaggio)
        {
            _context.Personaggi.Add(personaggio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonaggio), new { id = personaggio.Id }, personaggio);
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Personaggi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonaggio(int id, Personaggio personaggio)
        {
            if (id != personaggio.Id)
            {
                return BadRequest(new { Message = "ID del personaggio non corrisponde." });
            }

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
        // DELETE: api/Personaggi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaggio(int id)
        {
            var personaggio = await _context.Personaggi.FindAsync(id);
            if (personaggio == null)
            {
                return NotFound(new { Message = "Personaggio non trovato." });
            }

            _context.Personaggi.Remove(personaggio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaggioExists(int id)
        {
            return _context.Personaggi.Any(p => p.Id == id);
        }



    }
}
