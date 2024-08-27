using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Finale.Context;
using Api_Finale.Models;


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
        public async Task<ActionResult<Registrazione>> GetRegistrazione(int id)
        {
            var registrazione = await _context.Registrazioni
                .Include(r => r.Evento)
                .Include(r => r.Utente)
                .Include(r => r.Personaggio)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registrazione == null)
            {
                return NotFound(new { Message = "Registrazione non trovata." });
            }

            return registrazione;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: api/Registrazioni
        [HttpPost]
        public async Task<ActionResult<Registrazione>> CreateRegistrazione([FromBody]  Registrazione registrazione, [FromQuery] List<int> serviziIds)
        {
            _context.Registrazioni.Add(registrazione);
            await _context.SaveChangesAsync();

            // Associa i servizi selezionati alla registrazione
            foreach (var servizioId in serviziIds)
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
