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

namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly UtenteService _utenteService;
        public AuthController(IAuthService authService, IConfiguration configuration, UtenteService utenteService) 
        { 
            _authService = authService;
            _configuration = configuration;
            _utenteService = utenteService;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // POST: api/Auth/Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UtenteLoginModel utenteLoginModel)
        {
            var token = _authService.Login(utenteLoginModel.Username, utenteLoginModel.Password);
            if (token == null)
            {
                return Unauthorized(new { Message = "Nome utente o password non validi." });
            }

            return Ok(new { Token = token });
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
        /*[Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var userClaims = User.Claims;
            return Ok(new
            {
                Name = User.Identity.Name,
                Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Roles = userClaims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
            });
        }*/
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
                Registrazioni = utente.Registrazioni,  
                Personaggi = utente.Personaggi,  
                Roles = utente.Ruoli.Select(r => r.Nome).ToList()
            });



        }





    }
}
