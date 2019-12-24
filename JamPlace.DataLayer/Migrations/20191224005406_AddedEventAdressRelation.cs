using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class AddedEventAdressRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "amEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventAdressId",
                table: "amEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_amEvents_EventAdressId",
                table: "amEvents",
                column: "EventAdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_amEvents_Addresses_EventAdressId",
                table: "amEvents",
                column: "EventAdressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amEvents_Addresses_EventAdressId",
                table: "amEvents");

            migrationBuilder.DropIndex(
                name: "IX_amEvents_EventAdressId",
                table: "amEvents");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "amEvents");

            migrationBuilder.DropColumn(
                name: "EventAdressId",
                table: "amEvents");
        }
    }
}
