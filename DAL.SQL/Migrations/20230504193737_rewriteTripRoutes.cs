using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class rewriteTripRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripRoutePoints_Trips_TriptId",
                table: "TripRoutePoints");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TravelTime",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "TriptId",
                table: "TripRoutePoints",
                newName: "TripRouteId");

            migrationBuilder.RenameIndex(
                name: "IX_TripRoutePoints_TriptId",
                table: "TripRoutePoints",
                newName: "IX_TripRoutePoints_TripRouteId");

            migrationBuilder.AddColumn<decimal>(
                name: "MinutesFromStart",
                table: "TripRoutePoints",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TripRouteId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_Trips_TripRouteId",
                        column: x => x.TripRouteId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TripRouteId",
                table: "Trip",
                column: "TripRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripRoutePoints_Trips_TripRouteId",
                table: "TripRoutePoints",
                column: "TripRouteId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripRoutePoints_Trips_TripRouteId",
                table: "TripRoutePoints");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropColumn(
                name: "MinutesFromStart",
                table: "TripRoutePoints");

            migrationBuilder.RenameColumn(
                name: "TripRouteId",
                table: "TripRoutePoints",
                newName: "TriptId");

            migrationBuilder.RenameIndex(
                name: "IX_TripRoutePoints_TripRouteId",
                table: "TripRoutePoints",
                newName: "IX_TripRoutePoints_TriptId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Trips",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "Trips",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Trips",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TravelTime",
                table: "Trips",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);


            migrationBuilder.AddForeignKey(
                name: "FK_TripRoutePoints_Trips_TriptId",
                table: "TripRoutePoints",
                column: "TriptId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
