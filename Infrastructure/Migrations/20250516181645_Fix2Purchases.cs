using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix2Purchases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Memberships_MembershipId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMemberships_Purchases_PurchaseId",
                table: "UserMemberships");

            migrationBuilder.DropIndex(
                name: "IX_UserMemberships_PurchaseId",
                table: "UserMemberships");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_MembershipId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "UserMemberships");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Purchases");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Purchases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId",
                table: "UserMemberships",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Purchases",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "MembershipId",
                table: "Purchases",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberships_PurchaseId",
                table: "UserMemberships",
                column: "PurchaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_MembershipId",
                table: "Purchases",
                column: "MembershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Memberships_MembershipId",
                table: "Purchases",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMemberships_Purchases_PurchaseId",
                table: "UserMemberships",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
