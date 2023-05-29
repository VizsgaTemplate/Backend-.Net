using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Varga_Balázs_Ádám_backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kategoriak",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nev = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategoriak", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ingatlanok",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kategoriaid = table.Column<int>(type: "int", nullable: false),
                    leiras = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hirdetesDatuma = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tehermentes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ar = table.Column<int>(type: "int", nullable: false),
                    kepUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingatlanok", x => x.id);
                    table.ForeignKey(
                        name: "FK_ingatlanok_kategoriak_kategoriaid",
                        column: x => x.kategoriaid,
                        principalTable: "kategoriak",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "kategoriak",
                columns: new[] { "id", "nev" },
                values: new object[,]
                {
                    { 1, "Ház" },
                    { 2, "Lakás" },
                    { 3, "Építési telek" },
                    { 4, "Garázs" },
                    { 5, "Mezőgazdasági terület" },
                    { 6, "Ipari ingatlan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingatlanok_kategoriaid",
                table: "ingatlanok",
                column: "kategoriaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingatlanok");

            migrationBuilder.DropTable(
                name: "kategoriak");
        }
    }
}
