using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JamPlace.DataLayer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(nullable: true),
                    LocalNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JamEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JamEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JamUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(nullable: true),
                    UserIdentityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JamUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NeededEquipmentEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JamEventId = table.Column<int>(nullable: false),
                    EquipmentId = table.Column<int>(nullable: false),
                    Quanity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeededEquipmentEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    Artist = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JamUserId = table.Column<int>(nullable: false),
                    JamEventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventEquipment_JamEvents_JamEventId",
                        column: x => x.JamEventId,
                        principalTable: "JamEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventEquipment_JamUsers_JamUserId",
                        column: x => x.JamUserId,
                        principalTable: "JamUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalEquipmentUser",
                columns: table => new
                {
                    JamUserDoId = table.Column<int>(nullable: false),
                    EquipmentDoId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "EventEventEquipmentDo",
                columns: table => new
                {
                    EquipmentDoId = table.Column<int>(nullable: false),
                    EventEquipmentDoId = table.Column<int>(nullable: false)
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
                name: "IX_EventEquipment_JamEventId",
                table: "EventEquipment",
                column: "JamEventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventEquipment_JamUserId",
                table: "EventEquipment",
                column: "JamUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventEventEquipmentDo_EquipmentDoId",
                table: "EventEventEquipmentDo",
                column: "EquipmentDoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalEquipmentUser_EquipmentDoId",
                table: "PersonalEquipmentUser",
                column: "EquipmentDoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "EventEventEquipmentDo");

            migrationBuilder.DropTable(
                name: "NeededEquipmentEvent");

            migrationBuilder.DropTable(
                name: "PersonalEquipmentUser");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "EventEquipment");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "JamEvents");

            migrationBuilder.DropTable(
                name: "JamUsers");
        }
    }
}
