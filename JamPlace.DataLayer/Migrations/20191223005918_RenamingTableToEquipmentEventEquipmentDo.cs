using Microsoft.EntityFrameworkCore.Migrations;

namespace JamPlace.DataLayer.Migrations
{
    public partial class RenamingTableToEquipmentEventEquipmentDo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEquipment_JamEvents_JamEventId",
                table: "EventEquipment");

            migrationBuilder.DropTable(
                name: "EventEventEquipmentDo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JamEvents",
                table: "JamEvents");

            migrationBuilder.RenameTable(
                name: "JamEvents",
                newName: "amEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_amEvents",
                table: "amEvents",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EquipmentEventEquipment",
                columns: table => new
                {
                    EquipmentDoId = table.Column<int>(nullable: false),
                    EventEquipmentDoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentEventEquipment", x => new { x.EventEquipmentDoId, x.EquipmentDoId });
                    table.ForeignKey(
                        name: "FK_EquipmentEventEquipment_Equipment_EquipmentDoId",
                        column: x => x.EquipmentDoId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentEventEquipment_EventEquipment_EventEquipmentDoId",
                        column: x => x.EventEquipmentDoId,
                        principalTable: "EventEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentEventEquipment_EquipmentDoId",
                table: "EquipmentEventEquipment",
                column: "EquipmentDoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEquipment_amEvents_JamEventId",
                table: "EventEquipment",
                column: "JamEventId",
                principalTable: "amEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEquipment_amEvents_JamEventId",
                table: "EventEquipment");

            migrationBuilder.DropTable(
                name: "EquipmentEventEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_amEvents",
                table: "amEvents");

            migrationBuilder.RenameTable(
                name: "amEvents",
                newName: "JamEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JamEvents",
                table: "JamEvents",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EventEventEquipmentDo",
                columns: table => new
                {
                    EventEquipmentDoId = table.Column<int>(type: "integer", nullable: false),
                    EquipmentDoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEventEquipmentDo", x => new { x.EventEquipmentDoId, x.EquipmentDoId });
                    table.ForeignKey(
                        name: "FK_EventEventEquipmentDo_Equipment_EquipmentDoId",
                        column: x => x.EquipmentDoId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventEventEquipmentDo_EventEquipment_EventEquipmentDoId",
                        column: x => x.EventEquipmentDoId,
                        principalTable: "EventEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventEventEquipmentDo_EquipmentDoId",
                table: "EventEventEquipmentDo",
                column: "EquipmentDoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEquipment_JamEvents_JamEventId",
                table: "EventEquipment",
                column: "JamEventId",
                principalTable: "JamEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
