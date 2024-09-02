using Api_Finale.Context;
using Api_Finale.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_Finale.Service
{
    public class AuthService :IAuthService
    {

        private readonly DataContext _context;
        private readonly IpasswordEncoder _passwordEncoder;
        private readonly string _jwtSecret;  // La chiave segreta per firmare i token
        private readonly int _jwtLifespan;   // La durata del token in minuti
        private readonly string _JwtIssuer;
        private readonly string _JwtAudience;
        public AuthService(DataContext context, IpasswordEncoder passwordEncoder, IConfiguration configuration)
        {
            _context = context;
            _passwordEncoder = passwordEncoder;
            _jwtSecret = configuration["Jwt:Key"];
            _jwtLifespan = int.Parse(configuration["JwtSettings:TokenLifespan"]);
            _JwtIssuer = configuration["Jwt:Issuer"];
            _JwtAudience = configuration["Jwt:Audience"]!;
        }
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string Login(string username, string password)
        {
            var encodedPassword = _passwordEncoder.Encode(password);
            var utente = _context.Utenti.Include(u => u.Ruoli)
                                        .FirstOrDefault(u => u.Nome == username && u.PasswordHash == encodedPassword);
            if (utente == null)
            {
                return null;  // Login fallito
            }

            // Genera il token JWT
            return GenerateJwtToken(utente);

        }

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string GenerateJwtToken(Utente utente)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, utente.Nome),
                new Claim(ClaimTypes.Email, utente.Email),
                new Claim(ClaimTypes.NameIdentifier, utente.Id.ToString())
            };

            utente.Ruoli.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Nome)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _JwtIssuer,
                audience: "prova.it",
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtLifespan),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Utente Register(Utente utente)
        {
            utente.PasswordHash = _passwordEncoder.Encode(utente.PasswordHash);
            utente.DataRegistrazione = DateTime.Now;

            var userRole = _context.Ruoli.FirstOrDefault(r => r.Nome == "Utente");
            if (userRole != null)
            {
                utente.Ruoli.Add(userRole);
            }

            _context.Utenti.Add(utente);
            _context.SaveChanges();

            var utenteproizione = _context.Utenti
                .Where(u => u.Id == utente.Id)
            .Select(u => new Utente
            {
                Id = u.Id,
                Nome = u.Nome,
                PasswordHash = u.PasswordHash,
                Email = u.Email,

                DataRegistrazione = u.DataRegistrazione


            })
            .FirstOrDefault();
            return utenteproizione;

        }
       
        


    }
}
