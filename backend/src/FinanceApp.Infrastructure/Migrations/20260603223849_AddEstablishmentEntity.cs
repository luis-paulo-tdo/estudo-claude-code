using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEstablishmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Establishment",
                table: "Transactions",
                newName: "EstablishmentId");

            migrationBuilder.CreateTable(
                name: "Establishments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EstablishmentId",
                table: "Transactions",
                column: "EstablishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Establishments_EstablishmentId",
                table: "Transactions",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Establishments_EstablishmentId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Establishments");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_EstablishmentId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "EstablishmentId",
                table: "Transactions",
                newName: "Establishment");
        }
    }
}
