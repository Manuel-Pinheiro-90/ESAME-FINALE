using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Finale.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personaggi_Eventi_EventoId",
                table: "Personaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Personaggi_Utenti_UtenteId",
                table: "Personaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Personaggi_PersonaggioId",
                table: "Registrazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Utenti_UtenteId",
                table: "Registrazioni");

            migrationBuilder.DropIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni");

            migrationBuilder.CreateIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni",
                column: "PersonaggioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personaggi_Eventi_EventoId",
                table: "Personaggi",
                column: "EventoId",
                principalTable: "Eventi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personaggi_Utenti_UtenteId",
                table: "Personaggi",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrazioni_Personaggi_PersonaggioId",
                table: "Registrazioni",
                column: "PersonaggioId",
                principalTable: "Personaggi",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrazioni_Utenti_UtenteId",
                table: "Registrazioni",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personaggi_Eventi_EventoId",
                table: "Personaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Personaggi_Utenti_UtenteId",
                table: "Personaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Personaggi_PersonaggioId",
                table: "Registrazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Utenti_UtenteId",
                table: "Registrazioni");

            migrationBuilder.DropIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni");

            migrationBuilder.CreateIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni",
                column: "PersonaggioId",
                unique: true,
                filter: "[PersonaggioId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Personaggi_Eventi_EventoId",
                table: "Personaggi",
                column: "EventoId",
                principalTable: "Eventi",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Personaggi_Utenti_UtenteId",
                table: "Personaggi",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrazioni_Personaggi_PersonaggioId",
                table: "Registrazioni",
                column: "PersonaggioId",
                principalTable: "Personaggi",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrazioni_Utenti_UtenteId",
                table: "Registrazioni",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
