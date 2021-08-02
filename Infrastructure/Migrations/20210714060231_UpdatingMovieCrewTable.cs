using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatingMovieCrewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieCrew_CrewId",
                table: "MovieCrew");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCrew",
                table: "MovieCrew",
                columns: new[] { "CrewId", "MovieId", "Department", "Job" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCrew",
                table: "MovieCrew");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCrew_CrewId",
                table: "MovieCrew",
                column: "CrewId");
        }
    }
}
