using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudialAtlasService.Migrations
{
    public partial class UpdateIdToSpecifyWhichModelsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistGenre_Artists_ArtistsId",
                table: "ArtistGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUser_Artists_ArtistsId",
                table: "ArtistUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUser_Users_UsersId",
                table: "ArtistUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreUser_Users_UsersId",
                table: "GenreUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistsId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Users_UsersId",
                table: "SongUser");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "SongUser",
                newName: "UsersUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SongUser_UsersId",
                table: "SongUser",
                newName: "IX_SongUser_UsersUserId");

            migrationBuilder.RenameColumn(
                name: "ArtistsId",
                table: "Songs",
                newName: "ArtistsArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ArtistsId",
                table: "Songs",
                newName: "IX_Songs_ArtistsArtistId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "GenreUser",
                newName: "UsersUserId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreUser_UsersId",
                table: "GenreUser",
                newName: "IX_GenreUser_UsersUserId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ArtistUser",
                newName: "UsersUserId");

            migrationBuilder.RenameColumn(
                name: "ArtistsId",
                table: "ArtistUser",
                newName: "ArtistsArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistUser_UsersId",
                table: "ArtistUser",
                newName: "IX_ArtistUser_UsersUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Artists",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "ArtistsId",
                table: "ArtistGenre",
                newName: "ArtistsArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistGenre_Artists_ArtistsArtistId",
                table: "ArtistGenre",
                column: "ArtistsArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUser_Artists_ArtistsArtistId",
                table: "ArtistUser",
                column: "ArtistsArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUser_Users_UsersUserId",
                table: "ArtistUser",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreUser_Users_UsersUserId",
                table: "GenreUser",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistsArtistId",
                table: "Songs",
                column: "ArtistsArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongUser_Users_UsersUserId",
                table: "SongUser",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistGenre_Artists_ArtistsArtistId",
                table: "ArtistGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUser_Artists_ArtistsArtistId",
                table: "ArtistUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUser_Users_UsersUserId",
                table: "ArtistUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreUser_Users_UsersUserId",
                table: "GenreUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistsArtistId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Users_UsersUserId",
                table: "SongUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UsersUserId",
                table: "SongUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_SongUser_UsersUserId",
                table: "SongUser",
                newName: "IX_SongUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "ArtistsArtistId",
                table: "Songs",
                newName: "ArtistsId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ArtistsArtistId",
                table: "Songs",
                newName: "IX_Songs_ArtistsId");

            migrationBuilder.RenameColumn(
                name: "UsersUserId",
                table: "GenreUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreUser_UsersUserId",
                table: "GenreUser",
                newName: "IX_GenreUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "UsersUserId",
                table: "ArtistUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "ArtistsArtistId",
                table: "ArtistUser",
                newName: "ArtistsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistUser_UsersUserId",
                table: "ArtistUser",
                newName: "IX_ArtistUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Artists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ArtistsArtistId",
                table: "ArtistGenre",
                newName: "ArtistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistGenre_Artists_ArtistsId",
                table: "ArtistGenre",
                column: "ArtistsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUser_Artists_ArtistsId",
                table: "ArtistUser",
                column: "ArtistsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUser_Users_UsersId",
                table: "ArtistUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreUser_Users_UsersId",
                table: "GenreUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistsId",
                table: "Songs",
                column: "ArtistsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongUser_Users_UsersId",
                table: "SongUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
