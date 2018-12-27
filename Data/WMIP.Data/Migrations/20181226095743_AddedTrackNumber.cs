using Microsoft.EntityFrameworkCore.Migrations;

namespace WMIP.Data.Migrations
{
    public partial class AddedTrackNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackNumber",
                table: "Songs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackNumber",
                table: "Songs");
        }
    }
}
