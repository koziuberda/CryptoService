using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoService.Data.Migrations
{
    public partial class RenameDbEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceInfos");

            migrationBuilder.DropTable(
                name: "CryptoCurrencies");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    SymbolId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssetId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x.SymbolId);
                    table.ForeignKey(
                        name: "FK_Symbols_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Symbols_AssetId",
                table: "Symbols",
                column: "AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Symbols");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.CreateTable(
                name: "CryptoCurrencies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceInfos",
                columns: table => new
                {
                    SymbolId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceInfos", x => x.SymbolId);
                    table.ForeignKey(
                        name: "FK_PriceInfos_CryptoCurrencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CryptoCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceInfos_CurrencyId",
                table: "PriceInfos",
                column: "CurrencyId");
        }
    }
}
