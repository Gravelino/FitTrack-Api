using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2ec017dc-a27f-4277-bf37-90b5063b47cd"), null, "Trainer", "TRAINER" },
                    { new Guid("9ffc304f-1df2-4efe-af2f-726e1d7d1917"), null, "User", "USER" },
                    { new Guid("f63cb887-a8f6-4465-8a49-e3274ce6af99"), null, "Owner", "OWNER" },
                    { new Guid("fdf5ee34-bfc9-4fa3-a888-e820800e6121"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2ec017dc-a27f-4277-bf37-90b5063b47cd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ffc304f-1df2-4efe-af2f-726e1d7d1917"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f63cb887-a8f6-4465-8a49-e3274ce6af99"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fdf5ee34-bfc9-4fa3-a888-e820800e6121"));
        }
    }
}
