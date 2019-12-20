using Microsoft.EntityFrameworkCore.Migrations;

namespace movie_project.Migrations
{
    public partial class AddWatchedToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "Watched",
                table: "Movies",
                type: "BIT(1)",
                nullable: false,
                defaultValue: 0ul);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Watched",
                table: "Movies");
        }
    }
}
