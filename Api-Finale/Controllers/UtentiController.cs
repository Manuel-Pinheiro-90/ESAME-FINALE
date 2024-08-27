using Api_Finale.Models;
using Api_Finale.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtentiController : ControllerBase
    {
        private readonly UtenteService _utenteService;

        public UtentiController(UtenteService utenteService)
        {
            _utenteService = utenteService;
        }

        // GET: api/Utenti?pageNumber=1&pageSize=10
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utente>>> GetUtenti(int pageNumber = 1, int pageSize = 10)
        {
            var (utenti, totalRecords) = await _utenteService.GetUtenti(pageNumber, pageSize);

            // Aggiungi le informazioni di paginazione nelle intestazioni di risposta
            Response.Headers.Add("X-Total-Count", totalRecords.ToString());

            return Ok(utenti);
        }

        // GET: api/Utenti/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Utente>> GetUtente(int id)
        {
            try
            {
                var utente = await _utenteService.GetUtente(id);
                return Ok(utente);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // POST: api/Utenti
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Utente>> CreateUtente([FromForm] Utente utente, [FromForm] IFormFile? file)
        {
            try
            {
                // Gestione dell'immagine, se fornita
                if (file != null && file.Length > 0)
                {
                    utente.Foto = _utenteService.ConvertImage(file);
                }

                var createdUtente = await _utenteService.CreateUtente(utente);
                return CreatedAtAction(nameof(GetUtente), new { id = createdUtente.Id }, createdUtente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/Utenti/5
        [Authorize(Roles = "Admin,Utente")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtente(int id, [FromForm] Utente utente, [FromForm] IFormFile? file)
        {
            try
            {
                // Gestione dell'immagine, se fornita
                if (file != null && file.Length > 0)
                {
                    utente.Foto = _utenteService.ConvertImage(file);
                }

                var updatedUtente = await _utenteService.UpdateUtente(id, utente);
                return Ok(updatedUtente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Utenti/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtente(int id)
        {
            try
            {
                await _utenteService.DeleteUtente(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }



    }
}
