using Microsoft.EntityFrameworkCore.Migrations;

namespace movie_project.Migrations
{
    public partial class AddRequiredToMovieListAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_AspNetUsers_ApplicationUserRefId",
                table: "MovieList");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserRefId",
                table: "MovieList",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_AspNetUsers_ApplicationUserRefId",
                table: "MovieList",
                column: "ApplicationUserRefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_AspNetUsers_ApplicationUserRefId",
                table: "MovieList");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserRefId",
                table: "MovieList",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_AspNetUsers_ApplicationUserRefId",
                table: "MovieList",
                column: "ApplicationUserRefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
