using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiziController : ControllerBase
    {
        private readonly DataContext _context;

        public ServiziController(DataContext context)
        {
            _context = context;
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Servizi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servizio>>> GetServizi()
        {
            return await _context.Servizi.ToListAsync();
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Servizi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Servizio>> GetServizio(int id)
        {
            var servizio = await _context.Servizi.FindAsync(id);

            if (servizio == null)
            {
                return NotFound(new { Message = "Servizio non trovato." });
            }

            return servizio;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Servizi
        [HttpPost]
        public async Task<ActionResult<Servizio>> CreateServizio(Servizio servizio)
        {
            _context.Servizi.Add(servizio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServizio), new { id = servizio.Id }, servizio);
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUT: api/Servizi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServizio(int id, Servizio servizio)
        {
            if (id != servizio.Id)
            {
                return BadRequest(new { Message = "ID del servizio non corrisponde." });
            }

            _context.Entry(servizio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServizioExists(id))
                {
                    return NotFound(new { Message = "Servizio non trovato." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/Servizi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServizio(int id)
        {
            var servizio = await _context.Servizi.FindAsync(id);
            if (servizio == null)
            {
                return NotFound(new { Message = "Servizio non trovato." });
            }

            _context.Servizi.Remove(servizio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServizioExists(int id)
        {
            return _context.Servizi.Any(s => s.Id == id);
        }


    }
}
