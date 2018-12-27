using Microsoft.EntityFrameworkCore.Migrations;

namespace WMIP.Data.Migrations
{
    public partial class ExtractedBaseMusicModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Songs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseStage",
                table: "Songs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ReleaseStage",
                table: "Songs");
        }
    }
}
