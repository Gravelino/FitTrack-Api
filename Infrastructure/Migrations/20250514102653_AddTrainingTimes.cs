using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTime_AspNetUsers_UserId",
                table: "TrainingTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingTime",
                table: "TrainingTime");

            migrationBuilder.RenameTable(
                name: "TrainingTime",
                newName: "TrainingTimes");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingTime_UserId",
                table: "TrainingTimes",
                newName: "IX_TrainingTimes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingTimes",
                table: "TrainingTimes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTimes_AspNetUsers_UserId",
                table: "TrainingTimes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingTimes_AspNetUsers_UserId",
                table: "TrainingTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingTimes",
                table: "TrainingTimes");

            migrationBuilder.RenameTable(
                name: "TrainingTimes",
                newName: "TrainingTime");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingTimes_UserId",
                table: "TrainingTime",
                newName: "IX_TrainingTime_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingTime",
                table: "TrainingTime",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingTime_AspNetUsers_UserId",
                table: "TrainingTime",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
