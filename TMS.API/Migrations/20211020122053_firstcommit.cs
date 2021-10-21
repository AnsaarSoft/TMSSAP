using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.API.Migrations
{
    public partial class firstcommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SBOId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaveHours = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Users_rUserID",
                        column: x => x.rUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BreakTimes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rUserID = table.Column<int>(type: "int", nullable: true),
                    rTimeSheetID = table.Column<int>(type: "int", nullable: true),
                    StarTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    flgFullDay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakTimes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BreakTimes_TimeSheets_rTimeSheetID",
                        column: x => x.rTimeSheetID,
                        principalTable: "TimeSheets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BreakTimes_Users_rUserID",
                        column: x => x.rUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTimes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rUserID = table.Column<int>(type: "int", nullable: true),
                    rTimeSheetID = table.Column<int>(type: "int", nullable: true),
                    StarTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    flgFullDay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTimes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeaveTimes_TimeSheets_rTimeSheetID",
                        column: x => x.rTimeSheetID,
                        principalTable: "TimeSheets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveTimes_Users_rUserID",
                        column: x => x.rUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakTimes_rTimeSheetID",
                table: "BreakTimes",
                column: "rTimeSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_BreakTimes_rUserID",
                table: "BreakTimes",
                column: "rUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTimes_rTimeSheetID",
                table: "LeaveTimes",
                column: "rTimeSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTimes_rUserID",
                table: "LeaveTimes",
                column: "rUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_rUserID",
                table: "TimeSheets",
                column: "rUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakTimes");

            migrationBuilder.DropTable(
                name: "LeaveTimes");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
