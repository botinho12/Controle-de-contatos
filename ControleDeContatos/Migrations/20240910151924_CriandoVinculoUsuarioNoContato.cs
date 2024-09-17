using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeContatos.Migrations
{
    /// <inheritdoc />
    public partial class CriandoVinculoUsuarioNoContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "CONTATOS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CONTATOS_UsuarioId",
                table: "CONTATOS",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CONTATOS_Usuarios_UsuarioId",
                table: "CONTATOS",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CONTATOS_Usuarios_UsuarioId",
                table: "CONTATOS");

            migrationBuilder.DropIndex(
                name: "IX_CONTATOS_UsuarioId",
                table: "CONTATOS");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CONTATOS");
        }
    }
}
