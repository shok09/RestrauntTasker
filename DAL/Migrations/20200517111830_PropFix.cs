using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class PropFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsers_AspNetUsers_IdentityId",
                table: "OrderUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderUsers_IdentityId",
                table: "OrderUsers");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "OrderUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "OrderUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsers_ApplicationUserId",
                table: "OrderUsers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsers_AspNetUsers_ApplicationUserId",
                table: "OrderUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsers_AspNetUsers_ApplicationUserId",
                table: "OrderUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderUsers_ApplicationUserId",
                table: "OrderUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "OrderUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "OrderUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsers_IdentityId",
                table: "OrderUsers",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsers_AspNetUsers_IdentityId",
                table: "OrderUsers",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
