using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class AlteraNomeColunas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Citys");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Citys",
                newName: "CityName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Citys",
                newName: "country");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Citys",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
