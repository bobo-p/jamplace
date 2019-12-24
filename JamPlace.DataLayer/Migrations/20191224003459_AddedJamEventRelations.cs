using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JamPlace.DataLayer.Migrations
{
    public partial class AddedJamEventRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NeededEquipmentEvent",
                table: "NeededEquipmentEvent");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "NeededEquipmentEvent");

            migrationBuilder.DropColumn(
                name: "JamEventId",
                table: "NeededEquipmentEvent");

            migrationBuilder.DropColumn(
                name: "Quanity",
                table: "NeededEquipmentEvent");

            migrationBuilder.RenameTable(
                name: "NeededEquipmentEvent",
                newName: "NeededEquipmentEventDo");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NeededEquipmentEventDo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "JamEventDoId",
                table: "NeededEquipmentEventDo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentDoId",
                table: "NeededEquipmentEventDo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NeededEquipmentEventDo",
                table: "NeededEquipmentEventDo",
                columns: new[] { "JamEventDoId", "EquipmentDoId" });

            migrationBuilder.CreateTable(
                name: "JamEventJamUserDo",
                columns: table => new
                {
                    JamUserDoId = table.Column<int>(nullable: false),
                    JamEventDoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JamEventJamUserDo", x => new { x.JamEventDoId, x.JamUserDoId });
                    table.ForeignKey(
                        name: "FK_JamEventJamUserDo_amEvents_JamEventDoId",
                        column: x => x.JamEventDoId,
                        principalTable: "amEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JamEventJamUserDo_JamUsers_JamUserDoId",
                        column: x => x.JamUserDoId,
                        principalTable: "JamUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NeededEquipmentEventDo_EquipmentDoId",
                table: "NeededEquipmentEventDo",
                column: "EquipmentDoId");

            migrationBuilder.CreateIndex(
                name: "IX_JamEventJamUserDo_JamUserDoId",
                table: "JamEventJamUserDo",
                column: "JamUserDoId");

            migrationBuilder.AddForeignKey(
                name: "FK_NeededEquipmentEventDo_Equipment_EquipmentDoId",
                table: "NeededEquipmentEventDo",
                column: "EquipmentDoId",
                principalTable: "Equipment",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NeededEquipmentEventDo_Equipment_EquipmentDoId",
                table: "NeededEquipmentEventDo");

            migrationBuilder.DropForeignKey(
                name: "FK_NeededEquipmentEventDo_amEvents_JamEventDoId",
                table: "NeededEquipmentEventDo");

            migrationBuilder.DropTable(
                name: "JamEventJamUserDo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NeededEquipmentEventDo",
                table: "NeededEquipmentEventDo");

            migrationBuilder.DropIndex(
                name: "IX_NeededEquipmentEventDo_EquipmentDoId",
                table: "NeededEquipmentEventDo");

            migrationBuilder.DropColumn(
                name: "JamEventDoId",
                table: "NeededEquipmentEventDo");

            migrationBuilder.DropColumn(
                name: "EquipmentDoId",
                table: "NeededEquipmentEventDo");

            migrationBuilder.RenameTable(
                name: "NeededEquipmentEventDo",
                newName: "NeededEquipmentEvent");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NeededEquipmentEvent",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "NeededEquipmentEvent",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JamEventId",
                table: "NeededEquipmentEvent",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quanity",
                table: "NeededEquipmentEvent",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NeededEquipmentEvent",
                table: "NeededEquipmentEvent",
                column: "Id");
        }
    }
}
