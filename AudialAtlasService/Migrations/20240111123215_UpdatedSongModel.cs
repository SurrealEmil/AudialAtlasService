using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudialAtlasService.Migrations
{
    public partial class UpdatedSongModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistsArtistId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "ArtistsArtistId",
                table: "Songs",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ArtistsArtistId",
                table: "Songs",
                newName: "IX_Songs_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Songs",
                newName: "ArtistsArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                newName: "IX_Songs_ArtistsArtistId");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistsArtistId",
                table: "Songs",
                column: "ArtistsArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
