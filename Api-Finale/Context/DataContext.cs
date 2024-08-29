using Api_Finale.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Finale.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Documento> Documenti { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Personaggio> Personaggi { get; set; }
        public DbSet<Registrazione> Registrazioni { get; set; }
        public DbSet<RegistrazioneServizio> RegistrazioniServizi { get; set; }
        public DbSet<Ruolo> Ruoli { get; set; }
        public DbSet<Servizio> Servizi { get; set; }
        public DbSet<Utente> Utenti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<RegistrazioneServizio>()
                .HasKey(rs => new { rs.RegistrazioneId, rs.ServizioId });

            //  relazioni Utente -> Ruolo (Many-to-Many)
            modelBuilder.Entity<Utente>()
                .HasMany(u => u.Ruoli)
                .WithMany(r => r.Utenti)
                .UsingEntity(j => j.ToTable("UtenteRuoli"));

            //  relazione Evento -> Documento (One-to-Many)
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Documenti)
                .WithOne(d => d.Evento)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            // relazione Evento -> Registrazione (One-to-Many)
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Registrazioni)
                .WithOne(r => r.Evento)
                .HasForeignKey(r => r.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            // relazione Utente -> Registrazione (One-to-Many)
            modelBuilder.Entity<Utente>()
                .HasMany(u => u.Registrazioni)
                .WithOne(r => r.Utente)
                .HasForeignKey(r => r.UtenteId)
                .OnDelete(DeleteBehavior.Restrict);  

            

            //relazione Utente -> Personaggio (One-to-Many)
            modelBuilder.Entity<Utente>()
                .HasMany(u => u.Personaggi)
               .WithOne(p => p.Utente)
                .HasForeignKey(p => p.UtenteId)
                .OnDelete(DeleteBehavior.Restrict); 

            //relazione Personaggio -> Evento (Many-to-One, opzionale)
            modelBuilder.Entity<Personaggio>()
                .HasOne(p => p.Evento)
                .WithMany(e => e.Personaggi)
                .HasForeignKey(p => p.EventoId)
                .OnDelete(DeleteBehavior.SetNull);  // Evita la cancellazione a cascata

            // relazione Registrazione -> Personaggio (One-to-One, opzionale)
            modelBuilder.Entity<Registrazione>()
                .HasOne(r => r.Personaggio)
                .WithOne()
                .HasForeignKey<Registrazione>(r => r.PersonaggioId)
                .OnDelete(DeleteBehavior.SetNull);

            // relazione Servizio -> Registrazione (Many-to-Many tramite tabella associativa)
            modelBuilder.Entity<Servizio>()
                .HasMany(s => s.RegistrazioniServizi)
                .WithOne(rs => rs.Servizio)
                .HasForeignKey(rs => rs.ServizioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Registrazione>()
                .HasMany(r => r.RegistrazioniServizi)
                .WithOne(rs => rs.Registrazione)
                .HasForeignKey(rs => rs.RegistrazioneId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
