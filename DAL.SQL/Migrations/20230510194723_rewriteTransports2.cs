using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class rewriteTransports2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Kontragents_CarrierIdentityId",
                table: "Transports");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Transports_TransportId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Transports_CarrierIdentityId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "CarrierIdentityId",
                table: "Transports");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransportId",
                table: "Trip",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
            
            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Transports_TransportId",
                table: "Trip",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Transports_TransportId",
                table: "Trip");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransportId",
                table: "Trip",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CarrierIdentityId",
                table: "Transports",
                type: "uuid",
                nullable: true);
            
            migrationBuilder.CreateIndex(
                name: "IX_Transports_CarrierIdentityId",
                table: "Transports",
                column: "CarrierIdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Kontragents_CarrierIdentityId",
                table: "Transports",
                column: "CarrierIdentityId",
                principalTable: "Kontragents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Transports_TransportId",
                table: "Trip",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
