﻿// <auto-generated />
using System;
using Api_Finale.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api_Finale.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240826121557_test2")]
    partial class test2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api_Finale.Models.Documento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<string>("NomeDocumento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Percorso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.ToTable("Documenti");
                });

            modelBuilder.Entity("Api_Finale.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatoreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataFine")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInizio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImmagineEvento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Luogo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("NumeroPartecipantiMax")
                        .HasColumnType("int");

                    b.Property<string>("Titolo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CreatoreId");

                    b.ToTable("Eventi");
                });

            modelBuilder.Entity("Api_Finale.Models.Personaggio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UtenteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.HasIndex("UtenteId");

                    b.ToTable("Personaggi");
                });

            modelBuilder.Entity("Api_Finale.Models.Registrazione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CostoTotale")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataRegistrazione")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonaggioId")
                        .HasColumnType("int");

                    b.Property<int>("UtenteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.HasIndex("PersonaggioId")
                        .IsUnique()
                        .HasFilter("[PersonaggioId] IS NOT NULL");

                    b.HasIndex("UtenteId");

                    b.ToTable("Registrazioni");
                });

            modelBuilder.Entity("Api_Finale.Models.RegistrazioneServizio", b =>
                {
                    b.Property<int>("RegistrazioneId")
                        .HasColumnType("int");

                    b.Property<int>("ServizioId")
                        .HasColumnType("int");

                    b.HasKey("RegistrazioneId", "ServizioId");

                    b.HasIndex("ServizioId");

                    b.ToTable("RegistrazioniServizi");
                });

            modelBuilder.Entity("Api_Finale.Models.Ruolo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Ruoli");
                });

            modelBuilder.Entity("Api_Finale.Models.Servizio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Costo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Servizi");
                });

            modelBuilder.Entity("Api_Finale.Models.Utente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataRegistrazione")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Utenti");
                });

            modelBuilder.Entity("RuoloUtente", b =>
                {
                    b.Property<int>("RuoliId")
                        .HasColumnType("int");

                    b.Property<int>("UtentiId")
                        .HasColumnType("int");

                    b.HasKey("RuoliId", "UtentiId");

                    b.HasIndex("UtentiId");

                    b.ToTable("UtenteRuoli", (string)null);
                });

            modelBuilder.Entity("Api_Finale.Models.Documento", b =>
                {
                    b.HasOne("Api_Finale.Models.Evento", "Evento")
                        .WithMany("Documenti")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");
                });

            modelBuilder.Entity("Api_Finale.Models.Evento", b =>
                {
                    b.HasOne("Api_Finale.Models.Utente", "Creatore")
                        .WithMany("EventiCreati")
                        .HasForeignKey("CreatoreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creatore");
                });

            modelBuilder.Entity("Api_Finale.Models.Personaggio", b =>
                {
                    b.HasOne("Api_Finale.Models.Evento", "Evento")
                        .WithMany("Personaggi")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Api_Finale.Models.Utente", "Utente")
                        .WithMany("Personaggi")
                        .HasForeignKey("UtenteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("Api_Finale.Models.Registrazione", b =>
                {
                    b.HasOne("Api_Finale.Models.Evento", "Evento")
                        .WithMany("Registrazioni")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api_Finale.Models.Personaggio", "Personaggio")
                        .WithOne()
                        .HasForeignKey("Api_Finale.Models.Registrazione", "PersonaggioId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Api_Finale.Models.Utente", "Utente")
                        .WithMany("Registrazioni")
                        .HasForeignKey("UtenteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Personaggio");

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("Api_Finale.Models.RegistrazioneServizio", b =>
                {
                    b.HasOne("Api_Finale.Models.Registrazione", "Registrazione")
                        .WithMany("RegistrazioniServizi")
                        .HasForeignKey("RegistrazioneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api_Finale.Models.Servizio", "Servizio")
                        .WithMany("RegistrazioniServizi")
                        .HasForeignKey("ServizioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Registrazione");

                    b.Navigation("Servizio");
                });

            modelBuilder.Entity("RuoloUtente", b =>
                {
                    b.HasOne("Api_Finale.Models.Ruolo", null)
                        .WithMany()
                        .HasForeignKey("RuoliId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api_Finale.Models.Utente", null)
                        .WithMany()
                        .HasForeignKey("UtentiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api_Finale.Models.Evento", b =>
                {
                    b.Navigation("Documenti");

                    b.Navigation("Personaggi");

                    b.Navigation("Registrazioni");
                });

            modelBuilder.Entity("Api_Finale.Models.Registrazione", b =>
                {
                    b.Navigation("RegistrazioniServizi");
                });

            modelBuilder.Entity("Api_Finale.Models.Servizio", b =>
                {
                    b.Navigation("RegistrazioniServizi");
                });

            modelBuilder.Entity("Api_Finale.Models.Utente", b =>
                {
                    b.Navigation("EventiCreati");

                    b.Navigation("Personaggi");

                    b.Navigation("Registrazioni");
                });
#pragma warning restore 612, 618
        }
    }
}
