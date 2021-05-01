using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class RemoveMappedForCoord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citys_Coord_coordId",
                table: "Citys");

            migrationBuilder.DropTable(
                name: "Coord");

            migrationBuilder.DropIndex(
                name: "IX_Citys_coordId",
                table: "Citys");

            migrationBuilder.DropColumn(
                name: "coordId",
                table: "Citys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "coordId",
                table: "Citys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coord", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citys_coordId",
                table: "Citys",
                column: "coordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citys_Coord_coordId",
                table: "Citys",
                column: "coordId",
                principalTable: "Coord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
