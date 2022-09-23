using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    public partial class denemedenemedeneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_GenresId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "Books",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_GenresId",
                table: "Books",
                newName: "IX_Books_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Books",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                newName: "IX_Books_GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_GenresId",
                table: "Books",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
