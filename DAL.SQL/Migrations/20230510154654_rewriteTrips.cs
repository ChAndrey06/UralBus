using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class rewriteTrips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "Trip",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "Trip",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransportId",
                table: "Trip",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_CarrierId",
                table: "Trip",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_DriverId",
                table: "Trip",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TransportId",
                table: "Trip",
                column: "TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Kontragents_CarrierId",
                table: "Trip",
                column: "CarrierId",
                principalTable: "Kontragents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Persons_DriverId",
                table: "Trip",
                column: "DriverId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Transports_TransportId",
                table: "Trip",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Kontragents_CarrierId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Persons_DriverId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Transports_TransportId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_CarrierId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_DriverId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_TransportId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "TransportId",
                table: "Trip");
        }
    }
}
