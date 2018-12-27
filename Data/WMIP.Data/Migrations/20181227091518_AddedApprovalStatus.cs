using Microsoft.EntityFrameworkCore.Migrations;

namespace WMIP.Data.Migrations
{
    public partial class AddedApprovalStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Albums");

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Songs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Albums",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Albums");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Songs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Songs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Albums",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Albums",
                nullable: false,
                defaultValue: false);
        }
    }
}
