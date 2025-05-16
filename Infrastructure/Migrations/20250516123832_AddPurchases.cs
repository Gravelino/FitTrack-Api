using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserMemberships",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId",
                table: "UserMemberships",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    MembershipId = table.Column<Guid>(type: "uuid", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GymId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchases_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchases_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Purchases_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberships_PurchaseId",
                table: "UserMemberships",
                column: "PurchaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberships_ExpirationDate",
                table: "UserMemberships",
                column: "ExpirationDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberships_UserId_MembershipId",
                table: "UserMemberships",
                columns: new[] { "UserId", "MembershipId" });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_GymId",
                table: "Purchases",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_MembershipId",
                table: "Purchases",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseDate",
                table: "Purchases",
                column: "PurchaseDate");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMemberships_Purchases_PurchaseId",
                table: "UserMemberships",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMemberships_Purchases_PurchaseId",
                table: "UserMemberships");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_UserMemberships_PurchaseId",
                table: "UserMemberships");

            migrationBuilder.DropIndex(
                name: "IX_UserMemberships_ExpirationDate",
                table: "UserMemberships");

            migrationBuilder.DropIndex(
                name: "IX_UserMemberships_UserId_MembershipId",
                table: "UserMemberships");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserMemberships");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "UserMemberships");
        }
    }
}