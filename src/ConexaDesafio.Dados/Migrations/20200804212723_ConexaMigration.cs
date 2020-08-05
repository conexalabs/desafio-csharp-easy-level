using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConexaDesafio.Dados.Migrations
{
    public partial class ConexaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CidadesTemperatura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CidadesTemperatura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temperaturas",
                columns: table => new
                {
                    TemperaturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemperaturaCelsius = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    CidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperaturas", x => x.TemperaturaId);
                    table.ForeignKey(
                        name: "FK_Temperaturas_CidadesTemperatura_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "CidadesTemperatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Temperaturas_CidadeId",
                table: "Temperaturas",
                column: "CidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temperaturas");

            migrationBuilder.DropTable(
                name: "CidadesTemperatura");
        }
    }
}
