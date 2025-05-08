using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SleepsRenamedDatesField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "Sleeps",
                newName: "WakeUpTime");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Sleeps",
                newName: "SleepStart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WakeUpTime",
                table: "Sleeps",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "SleepStart",
                table: "Sleeps",
                newName: "From");
        }
    }
}
