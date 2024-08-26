using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Finale.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventi_Utenti_CreatoreId",
                table: "Eventi");

            migrationBuilder.DropIndex(
                name: "IX_Eventi_CreatoreId",
                table: "Eventi");

            migrationBuilder.DropColumn(
                name: "CreatoreId",
                table: "Eventi");

            migrationBuilder.AddColumn<int>(
                name: "UtenteId",
                table: "Eventi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_UtenteId",
                table: "Eventi",
                column: "UtenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventi_Utenti_UtenteId",
                table: "Eventi",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventi_Utenti_UtenteId",
                table: "Eventi");

            migrationBuilder.DropIndex(
                name: "IX_Eventi_UtenteId",
                table: "Eventi");

            migrationBuilder.DropColumn(
                name: "UtenteId",
                table: "Eventi");

            migrationBuilder.AddColumn<int>(
                name: "CreatoreId",
                table: "Eventi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_CreatoreId",
                table: "Eventi",
                column: "CreatoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventi_Utenti_CreatoreId",
                table: "Eventi",
                column: "CreatoreId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
