using Microsoft.EntityFrameworkCore.Migrations;

namespace movie_project.Migrations
{
    public partial class StartOfMovieAPI2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Movies_imdbID",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Movies_imdbID",
                table: "Movies",
                column: "imdbID");
        }
    }
}
