using Microsoft.EntityFrameworkCore.Migrations;

namespace movie_project.Migrations
{
    public partial class AddUniqueMovieListUserIdAndName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_MovieLists_ApplicationUserRefId",
            //    table: "MovieLists");

            migrationBuilder.CreateIndex(
                name: "IX_MovieLists_ApplicationUserRefId_Name",
                table: "MovieLists",
                columns: new[] { "ApplicationUserRefId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieLists_ApplicationUserRefId_Name",
                table: "MovieLists");

            migrationBuilder.CreateIndex(
                name: "IX_MovieLists_ApplicationUserRefId",
                table: "MovieLists",
                column: "ApplicationUserRefId");
        }
    }
}
