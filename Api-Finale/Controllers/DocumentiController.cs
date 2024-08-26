using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentiController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentiController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Documenti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documento>>> GetDocumenti()
        {
            return await _context.Documenti.Include(d => d.Evento).ToListAsync();
        }

        // GET: api/Documenti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documento>> GetDocumento(int id)
        {
            var documento = await _context.Documenti.Include(d => d.Evento).FirstOrDefaultAsync(d => d.Id == id);

            if (documento == null)
            {
                return NotFound(new { Message = "Documento non trovato." });
            }

            return documento;
        }

        // POST: api/Documenti
        [HttpPost]
        public async Task<ActionResult<Documento>> CreateDocumento(Documento documento)
        {
            _context.Documenti.Add(documento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocumento), new { id = documento.Id }, documento);
        }

        // PUT: api/Documenti/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocumento(int id, Documento documento)
        {
            if (id != documento.Id)
            {
                return BadRequest(new { Message = "ID del documento non corrisponde." });
            }

            _context.Entry(documento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoExists(id))
                {
                    return NotFound(new { Message = "Documento non trovato." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Documenti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            var documento = await _context.Documenti.FindAsync(id);
            if (documento == null)
            {
                return NotFound(new { Message = "Documento non trovato." });
            }

            _context.Documenti.Remove(documento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentoExists(int id)
        {
            return _context.Documenti.Any(d => d.Id == id);
        }
    }
}
