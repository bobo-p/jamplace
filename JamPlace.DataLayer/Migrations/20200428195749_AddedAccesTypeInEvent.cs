using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class AddedAccesTypeInEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessType",
                table: "JamEvents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessType",
                table: "JamEvents");
        }
    }
}
