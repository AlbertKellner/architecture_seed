using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class TodoList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Farmacias_Usuarios_UsuarioEntityId",
                table: "Farmacias");

            migrationBuilder.DropForeignKey(
                name: "FK_Laboratorios_Usuarios_UsuarioEntityId",
                table: "Laboratorios");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Usuarios_UsuarioEntityId",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_UsuarioEntityId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_UsuarioEntityId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_UsuarioEntityId",
                table: "Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Laboratorios_UsuarioEntityId",
                table: "Laboratorios");

            migrationBuilder.DropIndex(
                name: "IX_Farmacias_UsuarioEntityId",
                table: "Farmacias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_UsuarioEntityId",
                table: "Pacientes",
                column: "UsuarioEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_UsuarioEntityId",
                table: "Medicos",
                column: "UsuarioEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Laboratorios_UsuarioEntityId",
                table: "Laboratorios",
                column: "UsuarioEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Farmacias_UsuarioEntityId",
                table: "Farmacias",
                column: "UsuarioEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Farmacias_Usuarios_UsuarioEntityId",
                table: "Farmacias",
                column: "UsuarioEntityId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Laboratorios_Usuarios_UsuarioEntityId",
                table: "Laboratorios",
                column: "UsuarioEntityId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Usuarios_UsuarioEntityId",
                table: "Medicos",
                column: "UsuarioEntityId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_UsuarioEntityId",
                table: "Pacientes",
                column: "UsuarioEntityId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
