using Microsoft.EntityFrameworkCore.Migrations;

namespace WMIP.Data.Migrations
{
    public partial class AddedIsRejected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Songs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Albums",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Albums");
        }
    }
}
