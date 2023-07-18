using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class rewriteOrdersOperator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OperatorId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OperatorId",
                table: "Orders",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Persons_OperatorId",
                table: "Orders",
                column: "OperatorId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OperatorPersonIdentity_OperatorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OperatorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "Orders");
        }
    }
}
