using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToGym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Gyms",
                newName: "Address_Street");

            migrationBuilder.AddColumn<string>(
                name: "Address_Building",
                table: "Gyms",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Gyms",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Gyms",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "Gyms",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Building",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Gyms");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Gyms",
                newName: "Address");
        }
    }
}
