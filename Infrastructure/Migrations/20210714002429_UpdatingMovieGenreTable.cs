using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatingMovieGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGnre_Genre_GenreId",
                table: "MovieGnre");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGnre_Movie_MovieId",
                table: "MovieGnre");

            migrationBuilder.RenameTable(
                name: "MovieGnre",
                newName: "MovieGenre");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGnre_MovieId",
                table: "MovieGenre",
                newName: "IX_MovieGenre_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGnre_GenreId",
                table: "MovieGenre",
                newName: "IX_MovieGenre_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Genre_GenreId",
                table: "MovieGenre",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Movie_MovieId",
                table: "MovieGenre",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Genre_GenreId",
                table: "MovieGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Movie_MovieId",
                table: "MovieGenre");

            migrationBuilder.RenameTable(
                name: "MovieGenre",
                newName: "MovieGnre");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenre_MovieId",
                table: "MovieGnre",
                newName: "IX_MovieGnre_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenre_GenreId",
                table: "MovieGnre",
                newName: "IX_MovieGnre_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGnre_Genre_GenreId",
                table: "MovieGnre",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGnre_Movie_MovieId",
                table: "MovieGnre",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
