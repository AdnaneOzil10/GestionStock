using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionStock.Migrations;

/// <inheritdoc />
public partial class _20261126110305_InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Produits",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Prix = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Quantite = table.Column<int>(type: "int", nullable: false),
                DateAjout = table.Column<DateTime>(type: "datetime2", nullable: false),
                CategorieId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Produits", x => x.Id);
                table.ForeignKey(
                    name: "FK_Produits_Categories_CategorieId",
                    column: x => x.CategorieId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Produits_CategorieId",
            table: "Produits",
            column: "CategorieId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Produits");

        migrationBuilder.DropTable(
            name: "Categories");
    }
}
