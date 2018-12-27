using Microsoft.EntityFrameworkCore.Migrations;

namespace WMIP.Data.Migrations
{
    public partial class AddedMissedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Albums",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseStage",
                table: "Albums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PostId",
                table: "Ratings",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Posts_PostId",
                table: "Ratings",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Posts_PostId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_PostId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ReleaseStage",
                table: "Albums");
        }
    }
}
