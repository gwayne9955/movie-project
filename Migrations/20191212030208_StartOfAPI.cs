using Microsoft.EntityFrameworkCore.Migrations;

namespace movie_project.Migrations
{
    public partial class StartOfAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_MovieList_MovieListRefId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_AspNetUsers_ApplicationUserRefId",
                table: "MovieList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieList",
                table: "MovieList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.RenameTable(
                name: "MovieList",
                newName: "MovieLists");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movies");

            migrationBuilder.RenameIndex(
                name: "IX_MovieList_ApplicationUserRefId",
                table: "MovieLists",
                newName: "IX_MovieLists_ApplicationUserRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_MovieListRefId",
                table: "Movies",
                newName: "IX_Movies_MovieListRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieLists",
                table: "MovieLists",
                column: "MovieListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "imdbID");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieLists_AspNetUsers_ApplicationUserRefId",
                table: "MovieLists",
                column: "ApplicationUserRefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieLists_MovieListRefId",
                table: "Movies",
                column: "MovieListRefId",
                principalTable: "MovieLists",
                principalColumn: "MovieListId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieLists_AspNetUsers_ApplicationUserRefId",
                table: "MovieLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieLists_MovieListRefId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieLists",
                table: "MovieLists");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movie");

            migrationBuilder.RenameTable(
                name: "MovieLists",
                newName: "MovieList");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_MovieListRefId",
                table: "Movie",
                newName: "IX_Movie_MovieListRefId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieLists_ApplicationUserRefId",
                table: "MovieList",
                newName: "IX_MovieList_ApplicationUserRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "imdbID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieList",
                table: "MovieList",
                column: "MovieListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_MovieList_MovieListRefId",
                table: "Movie",
                column: "MovieListRefId",
                principalTable: "MovieList",
                principalColumn: "MovieListId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_AspNetUsers_ApplicationUserRefId",
                table: "MovieList",
                column: "ApplicationUserRefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
