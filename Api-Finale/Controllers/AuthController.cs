using Api_Finale.Models;
using Api_Finale.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) 
        { 
            _authService = authService;
        }

        // POST: api/Auth/Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Utente utente)
        {
            var user = _authService.Login(utente.Nome, utente.PasswordHash);
            if (user == null)
            {
                return Unauthorized(new { Message = "Nome utente o password non validi." });
            }

            // Creazione dei claims per l'utente
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Aggiungi i ruoli come claims
            user.Ruoli.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Nome)));

            // Creazione dell'identità utente e autenticazione tramite cookie (scelta provvisoria??)
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Il cookie rimane persistente anche dopo la chiusura del browser
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return Ok(new { Message = "Login effettuato con successo." });
        }

        // POST: api/Auth/Register
        [HttpPost("register")]
        public IActionResult Register([FromBody] Utente utente)
        {
            // Il servizio AuthService gestisce la codifica della password, l'assegnazione del ruolo e la data di registrazione
            var newUser = _authService.Register(utente);
            return CreatedAtAction(nameof(Login), new { id = newUser.Id }, newUser);
        }

        // POST: api/Auth/Logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { Message = "Logout effettuato con successo." });
        }

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

        // GET: api/Auth/Profile
        [Authorize]
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
        }






    }
}
