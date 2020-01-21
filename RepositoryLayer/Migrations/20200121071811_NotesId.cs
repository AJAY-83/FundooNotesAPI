using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class NotesId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "NoteLabel");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Label");

            migrationBuilder.RenameColumn(
                name: "NotesId",
                table: "NoteLabel",
                newName: "NoteId");

            migrationBuilder.RenameColumn(
                name: "LabelData",
                table: "NoteLabel",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "NoteLabel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "NoteLabel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "NoteLabel");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "NoteLabel");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "NoteLabel",
                newName: "LabelData");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "NoteLabel",
                newName: "NotesId");

            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "NoteLabel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Label",
                nullable: false,
                defaultValue: 0);
        }
    }
}
