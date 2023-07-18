using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class prefillRoutePoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoutePoints",
                columns: new[] { "Id", "Address", "CreatedAt", "DeletedAt", "IsDeleted", "Latitude", "Longitude", "Title" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Москва, Россия", new DateTime(2023, 4, 28, 8, 49, 22, 294, DateTimeKind.Utc).AddTicks(880), null, false, "55.755826", "37.6172999", "Москва" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Санкт-Петербург, Россия", new DateTime(2023, 4, 28, 8, 49, 22, 294, DateTimeKind.Utc).AddTicks(910), null, false, "59.9342802", "30.3350986", "Санкт-Петербург" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "RoutePoints",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));
        }
    }
}
