using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class AddedRelationPersonalEquipmentUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalEquipmentUserDo",
                columns: table => new
                {
                    JamUserDoId = table.Column<int>(nullable: false),
                    EquipmentDoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalEquipmentUserDo", x => new { x.JamUserDoId, x.EquipmentDoId });
                    table.ForeignKey(
                        name: "FK_PersonalEquipmentUserDo_Equipment_EquipmentDoId",
                        column: x => x.EquipmentDoId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalEquipmentUserDo_JamUsers_JamUserDoId",
                        column: x => x.JamUserDoId,
                        principalTable: "JamUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalEquipmentUserDo_EquipmentDoId",
                table: "PersonalEquipmentUserDo",
                column: "EquipmentDoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalEquipmentUserDo");
        }
    }
}
