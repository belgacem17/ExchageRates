using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchageRates.Migrations
{
    public partial class InitialeCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeName = table.Column<int>(type: "int", nullable: false),
                    DateExchange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExchangeRates = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchange", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exchange");
        }
    }
}
