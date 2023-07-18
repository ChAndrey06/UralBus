using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class addPopular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PopularDestination",
                table: "TripRoute",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PopularDestinationPictureId",
                table: "TripRoute",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopularDestinatonDescription",
                table: "TripRoute",
                type: "text",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.CreateIndex(
                name: "IX_TripRoute_PopularDestinationPictureId",
                table: "TripRoute",
                column: "PopularDestinationPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripRoute_Files_PopularDestinationPictureId",
                table: "TripRoute",
                column: "PopularDestinationPictureId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripRoute_Files_PopularDestinationPictureId",
                table: "TripRoute");

            migrationBuilder.DropIndex(
                name: "IX_TripRoute_PopularDestinationPictureId",
                table: "TripRoute");

            migrationBuilder.DropColumn(
                name: "PopularDestination",
                table: "TripRoute");

            migrationBuilder.DropColumn(
                name: "PopularDestinationPictureId",
                table: "TripRoute");

            migrationBuilder.DropColumn(
                name: "PopularDestinatonDescription",
                table: "TripRoute");

             }
    }
}
