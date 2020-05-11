using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JamPlace.DataLayer.Migrations
{
    public partial class ChangedEquipmentRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEquipment_JamEvents_JamEventId",
                table: "EventEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EventEquipment_JamUsers_JamUserId",
                table: "EventEquipment");

            migrationBuilder.DropTable(
                name: "EquipmentEventEquipment");

            migrationBuilder.DropTable(
                name: "NeededEquipmentEventDo");

            migrationBuilder.DropTable(
                name: "PersonalEquipmentUser");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_EventEquipment_JamEventId",
                table: "EventEquipment");

            migrationBuilder.DropIndex(
                name: "IX_EventEquipment_JamUserId",
                table: "EventEquipment");

            migrationBuilder.DropColumn(
                name: "JamEventId",
                table: "EventEquipment");

            migrationBuilder.DropColumn(
                name: "JamUserId",
                table: "EventEquipment");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EventEquipment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "EventEquipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EventEquipment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EventEquipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NeededEventEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeededEventEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NeededEventEquipment_JamEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "JamEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeededEventEquipment_JamUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "JamUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventEquipment_EventId",
                table: "EventEquipment",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventEquipment_UserId",
                table: "EventEquipment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NeededEventEquipment_EventId",
                table: "NeededEventEquipment",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_NeededEventEquipment_UserId",
                table: "NeededEventEquipment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEquipment_JamEvents_EventId",
                table: "EventEquipment",
                column: "EventId",
                principalTable: "JamEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventEquipment_JamUsers_UserId",
                table: "EventEquipment",
                column: "UserId",
                principalTable: "JamUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEquipment_JamEvents_EventId",
                table: "EventEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EventEquipment_JamUsers_UserId",
                table: "EventEquipment");

            migrationBuilder.DropTable(
                name: "NeededEventEquipment");

            migrationBuilder.DropIndex(
                name: "IX_EventEquipment_EventId",
                table: "EventEquipment");

            migrationBuilder.DropIndex(
                name: "IX_EventEquipment_UserId",
                table: "EventEquipment");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "EventEquipment");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "EventEquipment");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EventEquipment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EventEquipment");

            migrationBuilder.AddColumn<int>(
                name: "JamEventId",
                table: "EventEquipment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JamUserId",
                table: "EventEquipment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentEventEquipment",
                columns: table => new
                {
                    EventEquipmentDoId = table.Column<int>(type: "integer", nullable: false),
                    EquipmentDoId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "NeededEquipmentEventDo",
                columns: table => new
                {
                    JamEventDoId = table.Column<int>(type: "integer", nullable: false),
                    EquipmentDoId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeededEquipmentEventDo", x => new { x.JamEventDoId, x.EquipmentDoId });
                    table.ForeignKey(
                        name: "FK_NeededEquipmentEventDo_Equipment_EquipmentDoId",
                        column: x => x.EquipmentDoId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeededEquipmentEventDo_JamEvents_JamEventDoId",
                        column: x => x.JamEventDoId,
                        principalTable: "JamEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalEquipmentUser",
                columns: table => new
                {
                    JamUserDoId = table.Column<int>(type: "integer", nullable: false),
                    EquipmentDoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalEquipmentUser", x => new { x.JamUserDoId, x.EquipmentDoId });
                    table.ForeignKey(
                        name: "FK_PersonalEquipmentUser_Equipment_EquipmentDoId",
                        column: x => x.EquipmentDoId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalEquipmentUser_JamUsers_JamUserDoId",
                        column: x => x.JamUserDoId,
                        principalTable: "JamUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventEquipment_JamEventId",
                table: "EventEquipment",
                column: "JamEventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventEquipment_JamUserId",
                table: "EventEquipment",
                column: "JamUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentEventEquipment_EquipmentDoId",
                table: "EquipmentEventEquipment",
                column: "EquipmentDoId");

            migrationBuilder.CreateIndex(
                name: "IX_NeededEquipmentEventDo_EquipmentDoId",
                table: "NeededEquipmentEventDo",
                column: "EquipmentDoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalEquipmentUser_EquipmentDoId",
                table: "PersonalEquipmentUser",
                column: "EquipmentDoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEquipment_JamEvents_JamEventId",
                table: "EventEquipment",
                column: "JamEventId",
                principalTable: "JamEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventEquipment_JamUsers_JamUserId",
                table: "EventEquipment",
                column: "JamUserId",
                principalTable: "JamUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
