using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class AddedRelationJamUserEventEquipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalEquipmentUserDo_Equipment_EquipmentDoId",
                table: "PersonalEquipmentUserDo");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalEquipmentUserDo_JamUsers_JamUserDoId",
                table: "PersonalEquipmentUserDo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalEquipmentUserDo",
                table: "PersonalEquipmentUserDo");

            migrationBuilder.RenameTable(
                name: "PersonalEquipmentUserDo",
                newName: "PersonalEquipmentUser");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalEquipmentUserDo_EquipmentDoId",
                table: "PersonalEquipmentUser",
                newName: "IX_PersonalEquipmentUser_EquipmentDoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalEquipmentUser",
                table: "PersonalEquipmentUser",
                columns: new[] { "JamUserDoId", "EquipmentDoId" });

            migrationBuilder.CreateTable(
                name: "JamUserEventEquipment",
                columns: table => new
                {
                    JamUserDoId = table.Column<int>(nullable: false),
                    EventEquipmentDoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JamUserEventEquipment", x => new { x.JamUserDoId, x.EventEquipmentDoId });
                    table.ForeignKey(
                        name: "FK_JamUserEventEquipment_EquipmentUser_EventEquipmentDoId",
                        column: x => x.EventEquipmentDoId,
                        principalTable: "EquipmentUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JamUserEventEquipment_JamUsers_JamUserDoId",
                        column: x => x.JamUserDoId,
                        principalTable: "JamUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JamUserEventEquipment_EventEquipmentDoId",
                table: "JamUserEventEquipment",
                column: "EventEquipmentDoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalEquipmentUser_Equipment_EquipmentDoId",
                table: "PersonalEquipmentUser",
                column: "EquipmentDoId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalEquipmentUser_JamUsers_JamUserDoId",
                table: "PersonalEquipmentUser",
                column: "JamUserDoId",
                principalTable: "JamUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalEquipmentUser_Equipment_EquipmentDoId",
                table: "PersonalEquipmentUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalEquipmentUser_JamUsers_JamUserDoId",
                table: "PersonalEquipmentUser");

            migrationBuilder.DropTable(
                name: "JamUserEventEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalEquipmentUser",
                table: "PersonalEquipmentUser");

            migrationBuilder.RenameTable(
                name: "PersonalEquipmentUser",
                newName: "PersonalEquipmentUserDo");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalEquipmentUser_EquipmentDoId",
                table: "PersonalEquipmentUserDo",
                newName: "IX_PersonalEquipmentUserDo_EquipmentDoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalEquipmentUserDo",
                table: "PersonalEquipmentUserDo",
                columns: new[] { "JamUserDoId", "EquipmentDoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalEquipmentUserDo_Equipment_EquipmentDoId",
                table: "PersonalEquipmentUserDo",
                column: "EquipmentDoId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalEquipmentUserDo_JamUsers_JamUserDoId",
                table: "PersonalEquipmentUserDo",
                column: "JamUserDoId",
                principalTable: "JamUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
