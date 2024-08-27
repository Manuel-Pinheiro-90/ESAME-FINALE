using Api_Finale.Context;
using Api_Finale.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("CON")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/auth/login";  // Percorso di login
        options.LogoutPath = "/api/auth/logout";  // Percorso di logout
        options.Cookie.Name = "UserAuthCookie";  // Nome del cookie di autenticazione
        options.ExpireTimeSpan = TimeSpan.FromDays(7);  // Tempo di scadenza del cookie
        options.SlidingExpiration = true;  // Rinnovo automatico del cookie
    });

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()  // ("http://localhost:4200") // Cambia con l'indirizzo del frontend in sviluppo se non funziona
                   .AllowAnyMethod()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});



builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IpasswordEncoder, PasswordEncoder>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
