using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG1442___Project_1.Data.FootballMigrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    TeamCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    Budget = table.Column<double>(type: "REAL", nullable: false),
                    LeagueID = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueID",
                        column: x => x.LeagueID,
                        principalTable: "Leagues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Jersey = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    DOB = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FeePaid = table.Column<double>(type: "REAL", nullable: false),
                    EMail = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    LeagueID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_EMail",
                table: "Players",
                column: "EMail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamID_LeagueID",
                table: "Players",
                columns: new[] { "TeamID", "LeagueID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueID",
                table: "Teams",
                column: "LeagueID");
            ExtraMigration.Steps(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
