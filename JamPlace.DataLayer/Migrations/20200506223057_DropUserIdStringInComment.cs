using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class DropUserIdStringInComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Comments_JamUsers_UserId",
            //    table: "Comments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Comments_UserId",
            //    table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Comments",
                type: "integer",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_UserId",
            //    table: "Comments",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Comments_JamUsers_UserId",
            //    table: "Comments",
            //    column: "UserId",
            //    principalTable: "JamUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
