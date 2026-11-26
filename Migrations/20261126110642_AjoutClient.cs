using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionStock.Migrations;

/// <inheritdoc />
public partial class _20261126110642_AjoutClient : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Clients",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                Ville = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Age = table.Column<int>(type: "int", nullable: false),
                DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clients", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Clients");
    }
}
