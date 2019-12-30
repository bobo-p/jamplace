using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class dodanoPoleDtaewJamEventDo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amEvents_Addresses_EventAdressId",
                table: "amEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_EventEquipment_amEvents_JamEventId",
                table: "EventEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_JamEventJamUserDo_amEvents_JamEventDoId",
                table: "JamEventJamUserDo");

            migrationBuilder.DropForeignKey(
                name: "FK_NeededEquipmentEventDo_amEvents_JamEventDoId",
                table: "NeededEquipmentEventDo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amEvents",
                table: "amEvents");

            migrationBuilder.RenameTable(
                name: "amEvents",
                newName: "JamEvents");

            migrationBuilder.RenameIndex(
                name: "IX_amEvents_EventAdressId",
                table: "JamEvents",
                newName: "IX_JamEvents_EventAdressId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "JamEvents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_JamEvents",
                table: "JamEvents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEquipment_JamEvents_JamEventId",
                table: "EventEquipment",
                column: "JamEventId",
                principalTable: "JamEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JamEventJamUserDo_JamEvents_JamEventDoId",
                table: "JamEventJamUserDo",
                column: "JamEventDoId",
                principalTable: "JamEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JamEvents_Addresses_EventAdressId",
                table: "JamEvents",
                column: "EventAdressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NeededEquipmentEventDo_JamEvents_JamEventDoId",
                table: "NeededEquipmentEventDo",
                column: "JamEventDoId",
                principalTable: "JamEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEquipment_JamEvents_JamEventId",
                table: "EventEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_JamEventJamUserDo_JamEvents_JamEventDoId",
                table: "JamEventJamUserDo");

            migrationBuilder.DropForeignKey(
                name: "FK_JamEvents_Addresses_EventAdressId",
                table: "JamEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_NeededEquipmentEventDo_JamEvents_JamEventDoId",
                table: "NeededEquipmentEventDo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JamEvents",
                table: "JamEvents");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "JamEvents");

            migrationBuilder.RenameTable(
                name: "JamEvents",
                newName: "amEvents");

            migrationBuilder.RenameIndex(
                name: "IX_JamEvents_EventAdressId",
                table: "amEvents",
                newName: "IX_amEvents_EventAdressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_amEvents",
                table: "amEvents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_amEvents_Addresses_EventAdressId",
                table: "amEvents",
                column: "EventAdressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventEquipment_amEvents_JamEventId",
                table: "EventEquipment",
                column: "JamEventId",
                principalTable: "amEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JamEventJamUserDo_amEvents_JamEventDoId",
                table: "JamEventJamUserDo",
                column: "JamEventDoId",
                principalTable: "amEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NeededEquipmentEventDo_amEvents_JamEventDoId",
                table: "NeededEquipmentEventDo",
                column: "JamEventDoId",
                principalTable: "amEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
