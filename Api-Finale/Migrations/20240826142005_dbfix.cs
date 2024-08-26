using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Finale.Migrations
{
    /// <inheritdoc />
    public partial class dbfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documenti_Evento_EventoId",
                table: "Documenti");

            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Utenti_UtenteId",
                table: "Evento");

            migrationBuilder.DropForeignKey(
                name: "FK_Personaggi_Evento_EventoId",
                table: "Personaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Personaggi_Utenti_UtenteId",
                table: "Personaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Evento_EventoId",
                table: "Registrazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Personaggi_PersonaggioId",
                table: "Registrazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Utenti_UtenteId",
                table: "Registrazioni");

            migrationBuilder.DropIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evento",
                table: "Evento");

            migrationBuilder.RenameTable(
                name: "Evento",
                newName: "Eventi");

            migrationBuilder.RenameIndex(
                name: "IX_Evento_UtenteId",
                table: "Eventi",
                newName: "IX_Eventi_UtenteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eventi",
                table: "Eventi",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni",
                column: "PersonaggioId",
                unique: true,
                filter: "[PersonaggioId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Documenti_Eventi_EventoId",
                table: "Documenti",
                column: "EventoId",
                principalTable: "Eventi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventi_Utenti_UtenteId",
                table: "Eventi",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id");

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
                name: "FK_Registrazioni_Eventi_EventoId",
                table: "Registrazioni",
                column: "EventoId",
                principalTable: "Eventi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documenti_Eventi_EventoId",
                table: "Documenti");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventi_Utenti_UtenteId",
                table: "Eventi");

            migrationBuilder.DropForeignKey(
                name: "FK_Personaggi_Eventi_EventoId",
                table: "Personaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Personaggi_Utenti_UtenteId",
                table: "Personaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Eventi_EventoId",
                table: "Registrazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Personaggi_PersonaggioId",
                table: "Registrazioni");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrazioni_Utenti_UtenteId",
                table: "Registrazioni");

            migrationBuilder.DropIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eventi",
                table: "Eventi");

            migrationBuilder.RenameTable(
                name: "Eventi",
                newName: "Evento");

            migrationBuilder.RenameIndex(
                name: "IX_Eventi_UtenteId",
                table: "Evento",
                newName: "IX_Evento_UtenteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evento",
                table: "Evento",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Registrazioni_PersonaggioId",
                table: "Registrazioni",
                column: "PersonaggioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documenti_Evento_EventoId",
                table: "Documenti",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Utenti_UtenteId",
                table: "Evento",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personaggi_Evento_EventoId",
                table: "Personaggi",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personaggi_Utenti_UtenteId",
                table: "Personaggi",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrazioni_Evento_EventoId",
                table: "Registrazioni",
                column: "EventoId",
                principalTable: "Evento",
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
    }
}
