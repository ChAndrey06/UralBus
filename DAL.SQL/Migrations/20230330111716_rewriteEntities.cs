using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class rewriteEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Persons_ClientPersonId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ClientPersonId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientPersonId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ContractEmail",
                table: "Kontragents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractPhone",
                table: "Kontragents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Persons_CarrierId",
                table: "Orders",
                column: "CarrierId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Persons_CarrierId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ContractEmail",
                table: "Kontragents");

            migrationBuilder.DropColumn(
                name: "ContractPhone",
                table: "Kontragents");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientPersonId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientPersonId",
                table: "Orders",
                column: "ClientPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Persons_ClientPersonId",
                table: "Orders",
                column: "ClientPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
