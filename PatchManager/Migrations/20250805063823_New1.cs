using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatchManager.Migrations
{
    /// <inheritdoc />
    public partial class New1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checksum",
                table: "Patches");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { 1, "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "admin" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CurrentPatchVersion", "PasswordHash", "ServerIp", "Username" },
                values: new object[,]
                {
                    { 1, "1.0.0", "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad", "192.168.1.10", "customer_a" },
                    { 2, "1.1.0", "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad", "192.168.1.11", "customer_b" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CustomerId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Product A" },
                    { 2, 2, "Product B" }
                });

            migrationBuilder.InsertData(
                table: "Patches",
                columns: new[] { "Id", "Description", "FileName", "ProductId", "ReleasedOn", "Version" },
                values: new object[,]
                {
                    { 1, "Initial release", "patch_1.0.0.zip", 1, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1.0.0" },
                    { 2, "Minor bug fixes", "patch_1.1.0.zip", 2, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "1.1.0" }
                });

            migrationBuilder.InsertData(
                table: "PatchStatuses",
                columns: new[] { "Id", "CustomerId", "PatchId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 1, "Installed", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, "Pending", new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PatchStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PatchStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Checksum",
                table: "Patches",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
