using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class addTripDrafts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientPhoneNumber",
                table: "Orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "TripDrafts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TripRouteId = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransportId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    EndDayOfWeek = table.Column<int>(type: "integer", nullable: true),
                    StartTimeOfDay = table.Column<string>(type: "text", nullable: true),
                    EndTimeOfDay = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripDrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripDrafts_Kontragents_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Kontragents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripDrafts_Persons_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripDrafts_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripDrafts_TripRoute_TripRouteId",
                        column: x => x.TripRouteId,
                        principalTable: "TripRoute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0,DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0,DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_TripDrafts_CarrierId",
                table: "TripDrafts",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_TripDrafts_DriverId",
                table: "TripDrafts",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TripDrafts_TransportId",
                table: "TripDrafts",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_TripDrafts_TripRouteId",
                table: "TripDrafts",
                column: "TripRouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripDrafts");

            migrationBuilder.AlterColumn<string>(
                name: "ClientPhoneNumber",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 19, 39, 59, 137, DateTimeKind.Utc).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 19, 39, 59, 137, DateTimeKind.Utc).AddTicks(330));
        }
    }
}
