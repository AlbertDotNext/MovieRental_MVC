using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatingCastTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Cast_CastId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_CastId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "CastId",
                table: "Movie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CastId",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_CastId",
                table: "Movie",
                column: "CastId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Cast_CastId",
                table: "Movie",
                column: "CastId",
                principalTable: "Cast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
