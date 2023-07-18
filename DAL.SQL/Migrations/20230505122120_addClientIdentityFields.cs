using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class addClientIdentityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Persons",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationDocumentNumber",
                table: "Persons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationDocumentType",
                table: "Persons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SendAdvertisements",
                table: "Persons",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Persons",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "IdentificationDocumentNumber",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "IdentificationDocumentType",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "SendAdvertisements",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Persons");
        }
    }
}
