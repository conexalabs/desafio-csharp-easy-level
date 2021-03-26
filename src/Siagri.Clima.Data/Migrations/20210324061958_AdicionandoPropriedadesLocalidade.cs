using Microsoft.EntityFrameworkCore.Migrations;

namespace Siagri.Clima.Data.Migrations
{
    public partial class AdicionandoPropriedadesLocalidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Localidades");

            migrationBuilder.AlterColumn<string>(
                name: "TemperaturaAtual",
                table: "Localidades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Localidades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Localidades");

            migrationBuilder.AlterColumn<int>(
                name: "TemperaturaAtual",
                table: "Localidades",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Localidades",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
