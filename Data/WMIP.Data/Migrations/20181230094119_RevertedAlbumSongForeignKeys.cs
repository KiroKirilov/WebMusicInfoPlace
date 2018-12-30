using Microsoft.EntityFrameworkCore.Migrations;

namespace WMIP.Data.Migrations
{
    public partial class RevertedAlbumSongForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumsSongs_Albums_AlbumId",
                table: "AlbumsSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumsSongs_Songs_SongId",
                table: "AlbumsSongs");

            migrationBuilder.AlterColumn<int>(
                name: "SongId",
                table: "AlbumsSongs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlbumId",
                table: "AlbumsSongs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumsSongs_Albums_AlbumId",
                table: "AlbumsSongs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumsSongs_Songs_SongId",
                table: "AlbumsSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumsSongs_Albums_AlbumId",
                table: "AlbumsSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumsSongs_Songs_SongId",
                table: "AlbumsSongs");

            migrationBuilder.AlterColumn<int>(
                name: "SongId",
                table: "AlbumsSongs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AlbumId",
                table: "AlbumsSongs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumsSongs_Albums_AlbumId",
                table: "AlbumsSongs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumsSongs_Songs_SongId",
                table: "AlbumsSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
