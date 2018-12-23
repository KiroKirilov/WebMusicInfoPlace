using Microsoft.EntityFrameworkCore.Migrations;

namespace WMIP.Data.Migrations
{
    public partial class ExtractedUserToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_Comment_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_Review_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Comment_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Review_UserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Comment_UserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Review_UserId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId1",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId2",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId1",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId2",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId1",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId2",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId2",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Comment_UserId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Review_UserId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Comment_UserId",
                table: "Posts",
                column: "Comment_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Review_UserId",
                table: "Posts",
                column: "Review_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_Comment_UserId",
                table: "Posts",
                column: "Comment_UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_Review_UserId",
                table: "Posts",
                column: "Review_UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
