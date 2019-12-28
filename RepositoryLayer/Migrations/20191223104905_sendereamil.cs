using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class sendereamil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Collabrator",
                newName: "SenderEmail");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverEmail",
                table: "Collabrator",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverEmail",
                table: "Collabrator");

            migrationBuilder.RenameColumn(
                name: "SenderEmail",
                table: "Collabrator",
                newName: "Email");
        }
    }
}
