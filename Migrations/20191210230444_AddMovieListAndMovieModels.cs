using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace movie_project.Migrations
{
    public partial class AddMovieListAndMovieModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieList",
                columns: table => new
                {
                    MovieListId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationUserRefId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieList", x => x.MovieListId);
                    table.ForeignKey(
                        name: "FK_MovieList_AspNetUsers_ApplicationUserRefId",
                        column: x => x.ApplicationUserRefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    imdbID = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    MovieListRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.imdbID);
                    table.ForeignKey(
                        name: "FK_Movie_MovieList_MovieListRefId",
                        column: x => x.MovieListRefId,
                        principalTable: "MovieList",
                        principalColumn: "MovieListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_MovieListRefId",
                table: "Movie",
                column: "MovieListRefId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieList_ApplicationUserRefId",
                table: "MovieList",
                column: "ApplicationUserRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "MovieList");
        }
    }
}
