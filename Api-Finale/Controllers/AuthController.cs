using Api_Finale.Models;
using Api_Finale.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Api_Finale.DTO;
using Api_Finale.Context;
using Microsoft.EntityFrameworkCore;

namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly UtenteService _utenteService;
        private readonly DataContext _dataContext;


        public AuthController(IAuthService authService, IConfiguration configuration, UtenteService utenteService, DataContext dataContext) 
        { 
            _authService = authService;
            _configuration = configuration;
            _utenteService = utenteService;
            _dataContext = dataContext;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: api/Auth/Login
        [HttpPost("login")]
        public async Task <IActionResult> Login([FromBody] UtenteLoginModel utenteLoginModel)
        {
           
            var token = _authService.Login(utenteLoginModel.Username, utenteLoginModel.Password);
            if (token == null)

            {
                return Unauthorized(new { Message = "Nome utente o password non validi." });
            }

            var utente = await _dataContext.Utenti.Include (u => u.Ruoli)
                .Where(u => u.Nome == utenteLoginModel.Username ).FirstOrDefaultAsync();
            
            if (utente == null)
            {
                return NotFound(new { Message = "Utente non trovato." });
            }

            var ruoliDto = utente.Ruoli.Select(r => new RuoloDTO
            {
                Id = r.Id,
                Nome = r.Nome,
            }).ToList();



            return Ok(new LoginResponseModel 
            {
                Token = token,
                Utente = new UtenteTokenDTO
            {


             Id = utente.Id,
             Email = utente.Email,
             Nome = utente.Nome,
             Ruoli = ruoliDto

            }
            });
        }


        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: api/Auth/Register
        [HttpPost("register")]
        public IActionResult Register([FromBody] Utente utente)
        {
            var newUser = _authService.Register(utente);
            return CreatedAtAction(nameof(Login), new { id = newUser.Id }, newUser);
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // GET: api/Auth/IsAuthenticated
        [HttpGet("isauthenticated")]
        public IActionResult IsAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { Message = "Utente autenticato.", User.Identity.Name });
            }
            else
            {
                return Unauthorized(new { Message = "Utente non autenticato." });
            }
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Auth/Profile
      
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = User.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                 return Unauthorized(new { Message = "Utente non autenticato" });


            }
            // Recupera l'utente dal database
            var utente = await _utenteService.GetUtente(int.Parse(userId));

            if (utente == null)
            {
                return NotFound(new { Message = "Utente non trovato." });
            }

            // Restituisci i dettagli completi dell'utente, inclusa la foto
            return Ok(new
            {
                Name = utente.Nome,
                Email = utente.Email,
                Foto = utente.Foto, // Includi la foto
                Registrazioni = utente.Registrazioni.Select(r => new
                {
                    r.Id,
                    r.DataRegistrazione,
                    r.CostoTotale,
                    Evento = new
                    {
                        r.Evento.Id,
                        r.Evento.Titolo,
                        r.Evento.DataInizio,
                        r.Evento.DataFine,
                        r.Evento.Luogo,
                    },
                Servizi = r.RegistrazioniServizi.Select(rs => new
                {
                    rs.Servizio.Id,
                    rs.Servizio.Nome,
                    rs.Servizio.Descrizione,
                    rs.Servizio.Costo
                }).ToList()
                }).ToList(),




                Personaggi = utente.Personaggi.Select(p => new
                {
                    p.Id,
                    p.Nome,
                    p.Descrizione
                }).ToList(),
                Roles = utente.Ruoli.Select(r => r.Nome).ToList()
            });



        }





    }
}
