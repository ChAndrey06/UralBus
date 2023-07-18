using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class NullableFieldsForTripDrafts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDrafts_Kontragents_CarrierId",
                table: "TripDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDrafts_Persons_DriverId",
                table: "TripDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDrafts_Transports_TransportId",
                table: "TripDrafts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransportId",
                table: "TripDrafts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "TripDrafts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "TripDrafts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TripDrafts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarrierId",
                table: "TripDrafts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_TripDrafts_Kontragents_CarrierId",
                table: "TripDrafts",
                column: "CarrierId",
                principalTable: "Kontragents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDrafts_Persons_DriverId",
                table: "TripDrafts",
                column: "DriverId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDrafts_Transports_TransportId",
                table: "TripDrafts",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDrafts_Kontragents_CarrierId",
                table: "TripDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDrafts_Persons_DriverId",
                table: "TripDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDrafts_Transports_TransportId",
                table: "TripDrafts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransportId",
                table: "TripDrafts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "TripDrafts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "TripDrafts",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TripDrafts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarrierId",
                table: "TripDrafts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 12, 15, 54, 44, 538, DateTimeKind.Utc).AddTicks(7599));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 12, 15, 54, 44, 538, DateTimeKind.Utc).AddTicks(7835));

            migrationBuilder.AddForeignKey(
                name: "FK_TripDrafts_Kontragents_CarrierId",
                table: "TripDrafts",
                column: "CarrierId",
                principalTable: "Kontragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripDrafts_Persons_DriverId",
                table: "TripDrafts",
                column: "DriverId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripDrafts_Transports_TransportId",
                table: "TripDrafts",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
