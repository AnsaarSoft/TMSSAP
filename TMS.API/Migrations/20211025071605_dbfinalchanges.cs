using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.API.Migrations
{
    public partial class dbfinalchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreakTimes_TimeSheets_rTimeSheetID",
                table: "BreakTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_BreakTimes_Users_rUserID",
                table: "BreakTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveTimes_TimeSheets_rTimeSheetID",
                table: "LeaveTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveTimes_Users_rUserID",
                table: "LeaveTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheets_Users_rUserID",
                table: "TimeSheets");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheets_rUserID",
                table: "TimeSheets");

            migrationBuilder.DropIndex(
                name: "IX_LeaveTimes_rTimeSheetID",
                table: "LeaveTimes");

            migrationBuilder.DropIndex(
                name: "IX_LeaveTimes_rUserID",
                table: "LeaveTimes");

            migrationBuilder.DropIndex(
                name: "IX_BreakTimes_rTimeSheetID",
                table: "BreakTimes");

            migrationBuilder.DropIndex(
                name: "IX_BreakTimes_rUserID",
                table: "BreakTimes");

            migrationBuilder.DropColumn(
                name: "rUserID",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "rTimeSheetID",
                table: "LeaveTimes");

            migrationBuilder.DropColumn(
                name: "rUserID",
                table: "LeaveTimes");

            migrationBuilder.DropColumn(
                name: "rTimeSheetID",
                table: "BreakTimes");

            migrationBuilder.DropColumn(
                name: "rUserID",
                table: "BreakTimes");

            migrationBuilder.AddColumn<int>(
                name: "rUser",
                table: "TimeSheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rTimeSheet",
                table: "LeaveTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rUser",
                table: "LeaveTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rTimeSheet",
                table: "BreakTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rUser",
                table: "BreakTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rUser",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "rTimeSheet",
                table: "LeaveTimes");

            migrationBuilder.DropColumn(
                name: "rUser",
                table: "LeaveTimes");

            migrationBuilder.DropColumn(
                name: "rTimeSheet",
                table: "BreakTimes");

            migrationBuilder.DropColumn(
                name: "rUser",
                table: "BreakTimes");

            migrationBuilder.AddColumn<int>(
                name: "rUserID",
                table: "TimeSheets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rTimeSheetID",
                table: "LeaveTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rUserID",
                table: "LeaveTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rTimeSheetID",
                table: "BreakTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rUserID",
                table: "BreakTimes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_rUserID",
                table: "TimeSheets",
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
                name: "IX_BreakTimes_rTimeSheetID",
                table: "BreakTimes",
                column: "rTimeSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_BreakTimes_rUserID",
                table: "BreakTimes",
                column: "rUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BreakTimes_TimeSheets_rTimeSheetID",
                table: "BreakTimes",
                column: "rTimeSheetID",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BreakTimes_Users_rUserID",
                table: "BreakTimes",
                column: "rUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveTimes_TimeSheets_rTimeSheetID",
                table: "LeaveTimes",
                column: "rTimeSheetID",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveTimes_Users_rUserID",
                table: "LeaveTimes",
                column: "rUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheets_Users_rUserID",
                table: "TimeSheets",
                column: "rUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
