using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class AddedBase64ImageToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoBase64",
                table: "JamUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoBase64",
                table: "JamUsers");
        }
    }
}
