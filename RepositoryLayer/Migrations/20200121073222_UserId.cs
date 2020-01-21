using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "NoteLabel");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "NoteLabel");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "NoteLabel");

            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "NoteLabel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "NoteLabel");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "NoteLabel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "NoteLabel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NoteLabel",
                nullable: true);
        }
    }
}
