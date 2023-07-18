using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class TripFieldNamesFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripRoutePoints_RoutePoints_RoutePointtId",
                table: "TripRoutePoints");

            migrationBuilder.RenameColumn(
                name: "RoutePointtId",
                table: "TripRoutePoints",
                newName: "RoutePointId");

            migrationBuilder.RenameIndex(
                name: "IX_TripRoutePoints_RoutePointtId",
                table: "TripRoutePoints",
                newName: "IX_TripRoutePoints_RoutePointId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripRoutePoints_RoutePoints_RoutePointId",
                table: "TripRoutePoints",
                column: "RoutePointId",
                principalTable: "RoutePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripRoutePoints_RoutePoints_RoutePointId",
                table: "TripRoutePoints");

            migrationBuilder.RenameColumn(
                name: "RoutePointId",
                table: "TripRoutePoints",
                newName: "RoutePointtId");

            migrationBuilder.RenameIndex(
                name: "IX_TripRoutePoints_RoutePointId",
                table: "TripRoutePoints",
                newName: "IX_TripRoutePoints_RoutePointtId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripRoutePoints_RoutePoints_RoutePointtId",
                table: "TripRoutePoints",
                column: "RoutePointtId",
                principalTable: "RoutePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
