using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class TransportDriverAndCarrierRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarrierIdentityId",
                table: "Transports",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CarrierIdentityId",
                table: "Persons",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Persons",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 20, 35, 32, 794, DateTimeKind.Utc).AddTicks(4094));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 20, 35, 32, 794, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.CreateIndex(
                name: "IX_Transports_CarrierIdentityId",
                table: "Transports",
                column: "CarrierIdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_DriverId",
                table: "Transports",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CarrierIdentityId",
                table: "Persons",
                column: "CarrierIdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Kontragents_CarrierIdentityId",
                table: "Persons",
                column: "CarrierIdentityId",
                principalTable: "Kontragents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Kontragents_CarrierIdentityId",
                table: "Transports",
                column: "CarrierIdentityId",
                principalTable: "Kontragents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Persons_DriverId",
                table: "Transports",
                column: "DriverId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Kontragents_CarrierIdentityId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Kontragents_CarrierIdentityId",
                table: "Transports");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Persons_DriverId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_CarrierIdentityId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_DriverId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CarrierIdentityId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CarrierIdentityId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "CarrierIdentityId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Persons");

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
