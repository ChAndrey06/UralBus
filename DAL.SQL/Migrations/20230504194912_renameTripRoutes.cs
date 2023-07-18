using System;
using DAL.Entities.TripRoute;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class renameTripRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Trips",
                newName: nameof(TripRoute));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name:nameof(TripRoute) ,
                newName: "Trips");
        }
    }
}
