using Api_Finale.Models;
using Api_Finale.Service;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utente>>> GetUtenti(int pageNumber = 1, int pageSize = 10)
        {
            var (utenti, totalRecords) = await _utenteService.GetUtenti(pageNumber, pageSize);

            // Aggiungi le informazioni di paginazione nelle intestazioni di risposta
            Response.Headers.Add("X-Total-Count", totalRecords.ToString());

            return Ok(utenti);
        }

        // GET: api/Utenti/5
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
        [HttpPost]
        public async Task<ActionResult<Utente>> CreateUtente(Utente utente)
        {
            try
            {
                var createdUtente = await _utenteService.CreateUtente(utente);
                return CreatedAtAction(nameof(GetUtente), new { id = createdUtente.Id }, createdUtente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/Utenti/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtente(int id, Utente utente)
        {
            try
            {
                var updatedUtente = await _utenteService.UpdateUtente(id, utente);
                return Ok(updatedUtente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Utenti/5
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
