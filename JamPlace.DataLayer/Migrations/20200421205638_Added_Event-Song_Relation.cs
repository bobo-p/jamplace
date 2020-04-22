using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class Added_EventSong_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Songs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_EventId",
                table: "Songs",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_JamEvents_EventId",
                table: "Songs",
                column: "EventId",
                principalTable: "JamEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_JamEvents_EventId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_EventId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Songs");
        }
    }
}
