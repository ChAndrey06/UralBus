using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class addFileToNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "News",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_FileId",
                table: "News",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Files_FileId",
                table: "News",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Files_FileId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_FileId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "News");
        }
    }
}
