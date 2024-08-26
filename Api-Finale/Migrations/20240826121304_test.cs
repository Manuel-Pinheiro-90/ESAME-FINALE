using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Finale.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruoli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruoli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servizi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servizi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRegistrazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInizio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Luogo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumeroPartecipantiMax = table.Column<int>(type: "int", nullable: false),
                    ImmagineEvento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventi_Utenti_CreatoreId",
                        column: x => x.CreatoreId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UtenteRuoli",
                columns: table => new
                {
                    RuoliId = table.Column<int>(type: "int", nullable: false),
                    UtentiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtenteRuoli", x => new { x.RuoliId, x.UtentiId });
                    table.ForeignKey(
                        name: "FK_UtenteRuoli_Ruoli_RuoliId",
                        column: x => x.RuoliId,
                        principalTable: "Ruoli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtenteRuoli_Utenti_UtentiId",
                        column: x => x.UtentiId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeDocumento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Percorso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documenti_Eventi_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personaggi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: true),
                    UtenteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personaggi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personaggi_Eventi_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventi",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personaggi_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Registrazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRegistrazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CostoTotale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    PersonaggioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrazioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrazioni_Eventi_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrazioni_Personaggi_PersonaggioId",
                        column: x => x.PersonaggioId,
                        principalTable: "Personaggi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Registrazioni_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrazioniServizi",
                columns: table => new
                {
                    RegistrazioneId = table.Column<int>(type: "int", nullable: false),
                    ServizioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrazioniServizi", x => new { x.RegistrazioneId, x.ServizioId });
                    table.ForeignKey(
                        name: "FK_RegistrazioniServizi_Registrazioni_RegistrazioneId",
                        column: x => x.RegistrazioneId,
                        principalTable: "Registrazioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrazioniServizi_Servizi_ServizioId",
                        column: x => x.ServizioId,
                        principalTable: "Servizi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documenti_EventoId",
                table: "Documenti",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_CreatoreId",
                table: "Eventi",
                column: "CreatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Personaggi_EventoId",
                table: "Personaggi",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personaggi_UtenteId",
                table: "Personaggi",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrazioni_EventoId",
                table: "Registrazioni",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni",
                column: "PersonaggioId",
                unique: true,
                filter: "[PersonaggioId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Registrazioni_UtenteId",
                table: "Registrazioni",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrazioniServizi_ServizioId",
                table: "RegistrazioniServizi",
                column: "ServizioId");

            migrationBuilder.CreateIndex(
                name: "IX_UtenteRuoli_UtentiId",
                table: "UtenteRuoli",
                column: "UtentiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documenti");

            migrationBuilder.DropTable(
                name: "RegistrazioniServizi");

            migrationBuilder.DropTable(
                name: "UtenteRuoli");

            migrationBuilder.DropTable(
                name: "Registrazioni");

            migrationBuilder.DropTable(
                name: "Servizi");

            migrationBuilder.DropTable(
                name: "Ruoli");

            migrationBuilder.DropTable(
                name: "Personaggi");

            migrationBuilder.DropTable(
                name: "Eventi");

            migrationBuilder.DropTable(
                name: "Utenti");
        }
    }
}
