using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Siagri.Clima.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    LocalidadeId = table.Column<Guid>(nullable: false),
                    Cidade = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    TemperaturaAtual = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.LocalidadeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Localidades_LocalidadeId",
                table: "Localidades",
                column: "LocalidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localidades");
        }
    }
}
