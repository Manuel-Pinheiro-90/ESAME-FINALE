using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Finale.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "UtenteId",
                table: "Personaggi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "UtenteId",
                table: "Personaggi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Personaggi_Eventi_EventoId",
                table: "Personaggi",
                column: "EventoId",
                principalTable: "Eventi",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personaggi_Utenti_UtenteId",
                table: "Personaggi",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id");
        }
    }
}
