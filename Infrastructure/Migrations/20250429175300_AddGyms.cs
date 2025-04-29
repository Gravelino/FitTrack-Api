using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGyms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GymId",
                table: "Trainers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GymId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GymId",
                table: "Admins",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gyms_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_GymId",
                table: "Trainers",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GymId",
                table: "AspNetUsers",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_GymId",
                table: "Admins",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_OwnerId",
                table: "Gyms",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Gyms_GymId",
                table: "Admins",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gyms_GymId",
                table: "AspNetUsers",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Gyms_GymId",
                table: "Trainers",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Gyms_GymId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gyms_GymId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Gyms_GymId",
                table: "Trainers");

            migrationBuilder.DropTable(
                name: "Gyms");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_GymId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GymId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Admins_GymId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Admins");
        }
    }
}
