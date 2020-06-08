using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class OrderingSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsers_Orders_ProjectId1",
                table: "OrderUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_OrderUsers_ProjectUserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_ProjectUserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_OrderUsers_ProjectId1",
                table: "OrderUsers");

            migrationBuilder.DropColumn(
                name: "ProjectUserId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "OrderUsers");

            migrationBuilder.AddColumn<int>(
                name: "OrderUserId",
                table: "RefreshTokens",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "OrderUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_OrderUserId",
                table: "RefreshTokens",
                column: "OrderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsers_OrderId1",
                table: "OrderUsers",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsers_Orders_OrderId1",
                table: "OrderUsers",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_OrderUsers_OrderUserId",
                table: "RefreshTokens",
                column: "OrderUserId",
                principalTable: "OrderUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsers_Orders_OrderId1",
                table: "OrderUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_OrderUsers_OrderUserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_OrderUserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_OrderUsers_OrderId1",
                table: "OrderUsers");

            migrationBuilder.DropColumn(
                name: "OrderUserId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderUsers");

            migrationBuilder.AddColumn<int>(
                name: "ProjectUserId",
                table: "RefreshTokens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "OrderUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ProjectUserId",
                table: "RefreshTokens",
                column: "ProjectUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsers_ProjectId1",
                table: "OrderUsers",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsers_Orders_ProjectId1",
                table: "OrderUsers",
                column: "ProjectId1",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_OrderUsers_ProjectUserId",
                table: "RefreshTokens",
                column: "ProjectUserId",
                principalTable: "OrderUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
