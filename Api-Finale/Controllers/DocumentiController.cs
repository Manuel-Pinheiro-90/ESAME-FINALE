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
       
       /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      
      [HttpPost]
        public async Task<ActionResult<Documento>> CreateDocumento([FromForm] IFormFile file, [FromForm] string nomeDocumento, [FromForm] int eventoId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { Message = "File non caricato o vuoto." });
            }

            // Salva il file in una directory personalizzata,  "uploads"
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Genera un nome di file unico per evitare conflitti
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Salva il file sul server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Crea un nuovo oggetto Documento con il percorso del file
            var documento = new Documento
            {
                NomeDocumento = nomeDocumento,
                Percorso = filePath,  // Salva il percorso del file
                EventoId = eventoId
            };

            _context.Documenti.Add(documento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocumento), new { id = documento.Id }, documento);
        }
        
        
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DELETE: api/Documenti/5
        [HttpDelete("{id}")]
public async Task<IActionResult> DeleteDocumento(int id)
{
    var documento = await _context.Documenti.FindAsync(id);
    if (documento == null)
    {
        return NotFound(new { Message = "Documento non trovato." });
    }

    // Elimina il file fisico dal server
    var filePath = documento.Percorso;
    if (System.IO.File.Exists(filePath))
    {
        System.IO.File.Delete(filePath);
    }

    _context.Documenti.Remove(documento);
    await _context.SaveChangesAsync();

    return NoContent();
}
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Documenti/5/file
        [HttpGet("{id}/file")]
        public async Task<IActionResult> GetDocumentoFile(int id)
        {
            var documento = await _context.Documenti.FindAsync(id);

            if (documento == null)
            {
                return NotFound(new { Message = "Documento non trovato." });
            }

            var filePath = documento.Percorso;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { Message = "File non trovato sul server." });
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileType = "application/pdf";  //  specificare il tipo di file, PDF

            return File(fileBytes, fileType, Path.GetFileName(filePath));
        }

        //  verificare se un documento esiste
        private bool DocumentoExists(int id)
        {
            return _context.Documenti.Any(d => d.Id == id);
        }

    }
}
