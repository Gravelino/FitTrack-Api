using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameGymFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymFeedback_AspNetUsers_UserId",
                table: "GymFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_GymFeedback_Gyms_GymId",
                table: "GymFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GymFeedback",
                table: "GymFeedback");

            migrationBuilder.RenameTable(
                name: "GymFeedback",
                newName: "GymFeedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_GymFeedback_UserId",
                table: "GymFeedbacks",
                newName: "IX_GymFeedbacks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GymFeedback_GymId",
                table: "GymFeedbacks",
                newName: "IX_GymFeedbacks_GymId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymFeedbacks",
                table: "GymFeedbacks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GymFeedbacks_AspNetUsers_UserId",
                table: "GymFeedbacks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GymFeedbacks_Gyms_GymId",
                table: "GymFeedbacks",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymFeedbacks_AspNetUsers_UserId",
                table: "GymFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_GymFeedbacks_Gyms_GymId",
                table: "GymFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GymFeedbacks",
                table: "GymFeedbacks");

            migrationBuilder.RenameTable(
                name: "GymFeedbacks",
                newName: "GymFeedback");

            migrationBuilder.RenameIndex(
                name: "IX_GymFeedbacks_UserId",
                table: "GymFeedback",
                newName: "IX_GymFeedback_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GymFeedbacks_GymId",
                table: "GymFeedback",
                newName: "IX_GymFeedback_GymId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymFeedback",
                table: "GymFeedback",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GymFeedback_AspNetUsers_UserId",
                table: "GymFeedback",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GymFeedback_Gyms_GymId",
                table: "GymFeedback",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
