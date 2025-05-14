using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTrainerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Trainers_TrainerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTrainings_Trainers_TrainerId",
                table: "GroupTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualTrainings_Trainers_TrainerId",
                table: "IndividualTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerComments_Trainers_TrainerId",
                table: "TrainerComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Trainers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Trainers_TrainerId",
                table: "AspNetUsers",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTrainings_Trainers_TrainerId",
                table: "GroupTrainings",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualTrainings_Trainers_TrainerId",
                table: "IndividualTrainings",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerComments_Trainers_TrainerId",
                table: "TrainerComments",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Trainers_TrainerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTrainings_Trainers_TrainerId",
                table: "GroupTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualTrainings_Trainers_TrainerId",
                table: "IndividualTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerComments_Trainers_TrainerId",
                table: "TrainerComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Trainers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Trainers_TrainerId",
                table: "AspNetUsers",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTrainings_Trainers_TrainerId",
                table: "GroupTrainings",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualTrainings_Trainers_TrainerId",
                table: "IndividualTrainings",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerComments_Trainers_TrainerId",
                table: "TrainerComments",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
