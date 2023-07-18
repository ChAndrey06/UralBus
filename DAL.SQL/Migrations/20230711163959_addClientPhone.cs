using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQL.Migrations
{
    /// <inheritdoc />
    public partial class addClientPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<string>(
                name: "ClientPhoneNumber",
                table: "Orders",
                type: "text",
                nullable: true);
            
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Patronymic", "PhoneNumber", "Roles", "Sex" },
                values: new object[] { new Guid("82448fb7-cbb9-4054-b871-695de72fd61d"), DateTime.UtcNow, DateTime.UtcNow, null, "not@user.com", "Заказ оператора", false, "", "e10adc3949ba59abbe56e057f20f883e", "", "+0000000000", "Client", null });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Blacklisted", "CreatedAt", "DeletedAt", "Discriminator", "IdentificationDocumentNumber", "IdentificationDocumentType", "IsDeleted", "SendAdvertisements", "UserId" },
                values: new object[] { new Guid("968f48a5-50ba-4eea-b312-e1072dd062b6"), false, DateTime.UtcNow, null, "Client", null, null, false, false, new Guid("82448fb7-cbb9-4054-b871-695de72fd61d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("968f48a5-50ba-4eea-b312-e1072dd062b6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("82448fb7-cbb9-4054-b871-695de72fd61d"));

            migrationBuilder.DropColumn(
                name: "ClientPhoneNumber",
                table: "Orders");
        }
    }
}
