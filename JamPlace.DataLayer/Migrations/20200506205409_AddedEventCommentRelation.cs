using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class AddedEventCommentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EventId",
                table: "Comments",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_JamEvents_EventId",
                table: "Comments",
                column: "EventId",
                principalTable: "JamEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_JamEvents_EventId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_EventId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Comments");
        }
    }
}
