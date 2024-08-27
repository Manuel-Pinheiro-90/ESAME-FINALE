using Api_Finale.Context;
using Api_Finale.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("CON")));

// Recupera la chiave segreta dal file di configurazione
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

// Configura l'autenticazione JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,  // imposta a true se vuoi validare l'emittente
        ValidateAudience = false,  // imposta a true se vuoi validare il pubblico
        ValidateLifetime = true,  // valida la scadenza del token
        ClockSkew = TimeSpan.FromMinutes(3)  // elimina la tolleranza della scadenza del token
    };
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()  // ("http://localhost:4200") // Cambia con l'indirizzo del frontend in sviluppo se non funziona
                   .AllowAnyMethod()
         
                   .AllowAnyHeader();
                  // .AllowCredentials();
        });
});



builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IpasswordEncoder, PasswordEncoder>();
builder.Services.AddScoped<UtenteService>();
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins"); // Usa "AllowSpecificOrigins" se hai specificato un'origine
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
