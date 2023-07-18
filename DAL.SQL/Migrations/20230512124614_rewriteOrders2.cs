using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class rewriteOrders2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TripId = table.Column<Guid>(nullable: false),
                    StartTripRoutePointId = table.Column<Guid>(nullable: false),
                    EndTripRoutePointId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    AdultsCount = table.Column<int>(nullable: false),
                    ChildrenCount = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    CreationType = table.Column<string>(nullable: true),
                    Discount = table.Column<decimal>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_TripRoutePoint_StartTripRoutePointId",
                        column: x => x.StartTripRoutePointId,
                        principalTable: "TripRoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_TripRoutePoint_EndTripRoutePointId",
                        column: x => x.EndTripRoutePointId,
                        principalTable: "TripRoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ClientPersonIdentity_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EndTripRoutePointId",
                table: "Orders",
                column: "EndTripRoutePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StartTripRoutePointId",
                table: "Orders",
                column: "StartTripRoutePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TripId",
                table: "Orders",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
