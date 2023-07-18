using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class rewriteDrivers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Kontragents_CarrierId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Kontragents_CarrierIdentityId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CarrierIdentityId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CarrierIdentityId",
                table: "Persons");

          
            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Kontragents_CarrierId",
                table: "Persons",
                column: "CarrierId",
                principalTable: "Kontragents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Kontragents_CarrierId",
                table: "Persons");

            migrationBuilder.AddColumn<Guid>(
                name: "CarrierIdentityId",
                table: "Persons",
                type: "uuid",
                nullable: true);
            
            migrationBuilder.CreateIndex(
                name: "IX_Persons_CarrierIdentityId",
                table: "Persons",
                column: "CarrierIdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Kontragents_CarrierId",
                table: "Persons",
                column: "CarrierId",
                principalTable: "Kontragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Kontragents_CarrierIdentityId",
                table: "Persons",
                column: "CarrierIdentityId",
                principalTable: "Kontragents",
                principalColumn: "Id");
        }
    }
}
