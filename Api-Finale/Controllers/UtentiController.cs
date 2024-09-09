using Api_Finale.DTO;
using Api_Finale.Models;
using Api_Finale.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<ActionResult<IEnumerable<UtenteDettagliatoDTO>>> GetUtenti()
        {
            var utenti = await _utenteService.GetAllUtenti();
            var utentiDTO = utenti.Select(u => new UtenteDettagliatoDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Foto = u.Foto,
                Ruoli = u.Ruoli.Select(r => new RuoloDTO { Id = r.Id, Nome = r.Nome }).ToList(),
                Personaggi = u.Personaggi.Select(p => new PersonaggioDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descrizione = p.Descrizione
                }).ToList()
            }).ToList();

            return Ok(utentiDTO);
        }


        // GET: api/Utenti/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UtenteDettagliatoDTO>> GetUtente(int id)
        {
            try
            {
                var utente = await _utenteService.GetUtente(id);

                // Mappa l'entità Utente al DTO UtenteDettagliatoDTO
                var utenteDTO = new UtenteDettagliatoDTO
                {
                    Id = utente.Id,
                    Nome = utente.Nome,
                    Email = utente.Email,
                    Foto = utente.Foto,
                    Ruoli = utente.Ruoli.Select(r => new RuoloDTO { Id = r.Id, Nome = r.Nome }).ToList(),
                    Personaggi = utente.Personaggi.Select(p => new PersonaggioDTO { Id = p.Id, Nome = p.Nome, Descrizione = p.Descrizione }).ToList()
                };

                // Restituisci il DTO al posto dell'entità
                return Ok(utenteDTO);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // POST: api/Utenti
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Utente>> CreateUtente([FromForm] UtenteInputDTO utenteDto)
        {
            try
            {
                var utente = new Utente
                {
                    Nome = utenteDto.Nome,
                    Email = utenteDto.Email,
                    PasswordHash = utenteDto.Password // Assumendo che la password venga hashata nel service
                };

                // Gestione dell'immagine, se fornita
                if (utenteDto.Foto != null && utenteDto.Foto.Length > 0)
                {
                    utente.Foto = _utenteService.ConvertImage(utenteDto.Foto);
                }

                var createdUtente = await _utenteService.CreateUtente(utente);
                return CreatedAtAction(nameof(GetUtente), new { id = createdUtente.Id }, createdUtente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,Utente")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtente(int id, [FromForm] UtenteInputDTO utenteDto)
        {
            try
            {
                // Recupera l'utente esistente dal database
                var existingUser = await _utenteService.GetUtente(id);
                if (existingUser == null)
                {
                    return NotFound(new { Message = "Utente non trovato." });
                }

                // Aggiorna il nome e l'email solo se sono forniti e validi
                if (!string.IsNullOrWhiteSpace(utenteDto.Nome))
                {
                    existingUser.Nome = utenteDto.Nome;
                }
                if (!string.IsNullOrWhiteSpace(utenteDto.Email))
                {
                    existingUser.Email = utenteDto.Email;
                }

                // Se la password è stata fornita e non è vuota, aggiorna la password (hashata nel service)
                if (!string.IsNullOrWhiteSpace(utenteDto.Password))
                {
                    existingUser.PasswordHash = utenteDto.Password; // Assumi che il service esegua l'hash
                }

                // Gestione dell'immagine, se fornita
                if (utenteDto.Foto != null && utenteDto.Foto.Length > 0)
                {
                    existingUser.Foto = _utenteService.ConvertImage(utenteDto.Foto);
                }

                // Aggiorna l'utente nel database
                var updatedUtente = await _utenteService.UpdateUtente(id, existingUser);

                return Ok(new { Message = "Utente aggiornato con successo.", updatedUtente });
            }
            catch (Exception ex)
            {
                // Log dell'errore, se necessario, e ritorna un messaggio di errore più utile
                return BadRequest(new { Message = "Si è verificato un errore durante l'aggiornamento dell'utente.", Dettagli = ex.Message });
            }
        }

        // PUT: api/Utenti/5
        /* [Authorize(Roles = "Admin,Utente")]
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateUtente(int id, [FromForm] UtenteInputDTO utenteDto)
         {
             try
             {
                 // Recupera l'utente esistente dal database
                 var existingUser = await _utenteService.GetUtente(id);
                 if (existingUser == null)
                 {
                     return NotFound(new { Message = "Utente non trovato." });
                 }

                 // Aggiorna il nome e l'email
                 existingUser.Nome = utenteDto.Nome;
                 existingUser.Email = utenteDto.Email;

                 // Se la password è stata fornita e non è vuota, aggiorna la password
                 if (!string.IsNullOrWhiteSpace(utenteDto.Password))
                 {
                     existingUser.PasswordHash = utenteDto.Password; // Assumi che il service esegua il hash
                 }


                 /* try
                  {
                      var utente = new Utente
                      {
                          Id = id,
                          Nome = utenteDto.Nome,
                          Email = utenteDto.Email,
                          PasswordHash = utenteDto.Password // Assumendo che la password venga hashata nel service
                      }; */

        // Gestione dell'immagine, se fornita
        /*   if (utenteDto.Foto != null && utenteDto.Foto.Length > 0)
           {
               existingUser.Foto = _utenteService.ConvertImage(utenteDto.Foto);
           }

           var updatedUtente = await _utenteService.UpdateUtente(id, existingUser);
           return Ok(updatedUtente);
       }
       catch (Exception ex)
       {
           return BadRequest(new { Message = ex.Message });
       }
   }*/

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
        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UtenteInputDTO profileDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new { Message = "Utente non autenticato." });
            }

            var utente = await _utenteService.GetUtente(int.Parse(userId));

            if (utente == null)
            {
                return NotFound(new { Message = "Utente non trovato." });
            }

            // Aggiorna i campi solo se validi
            if (!string.IsNullOrWhiteSpace(profileDto.Nome))
            {
                utente.Nome = profileDto.Nome;
            }

            if (!string.IsNullOrWhiteSpace(profileDto.Email))
            {
                utente.Email = profileDto.Email;
            }

            // Gestione dell'immagine opzionale
            if (profileDto.Foto != null && profileDto.Foto.Length > 0)
            {
                utente.Foto = _utenteService.ConvertImage(profileDto.Foto);
            }

            // Aggiorna il profilo nel database
            var updatedUser = await _utenteService.UpdateUtente(utente.Id, utente);

            return Ok(new { Message = "Profilo aggiornato con successo.", updatedUser });
        }





        // PUT: api/Utenti/profile
        /* [Authorize]
         [HttpPut("profile")]
         public async Task<IActionResult> UpdateProfile([FromForm] UtenteInputDTO profileDto)
         {
             var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

             if (userId == null)
             {
                 return Unauthorized(new { Message = "Utente non autenticato." });
             }

             var utente = await _utenteService.GetUtente(int.Parse(userId));

             if (utente == null)
             {
                 return NotFound(new { Message = "Utente non trovato." });
             }


             utente.Nome = profileDto.Nome;
             utente.Email = profileDto.Email;


             if (profileDto.Foto != null && profileDto.Foto.Length > 0)
             {
                 utente.Foto = _utenteService.ConvertImage(profileDto.Foto);
             }

             var updatedUser = await _utenteService.UpdateUtente(utente.Id, utente);

             return Ok(new { Message = "Profilo aggiornato con successo.", updatedUser });
             //crea una select per evitare cicili
         }
     }*/
    }
}
