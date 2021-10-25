using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.API.Migrations
{
    public partial class BreakLeaveTimeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flgFullDay",
                table: "LeaveTimes");

            migrationBuilder.DropColumn(
                name: "flgFullDay",
                table: "BreakTimes");

            migrationBuilder.RenameColumn(
                name: "StarTime",
                table: "LeaveTimes",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "StarTime",
                table: "BreakTimes",
                newName: "StartTime");

            migrationBuilder.AddColumn<bool>(
                name: "flgBreak",
                table: "TimeSheets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "flgLeave",
                table: "TimeSheets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flgBreak",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "flgLeave",
                table: "TimeSheets");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "LeaveTimes",
                newName: "StarTime");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "BreakTimes",
                newName: "StarTime");

            migrationBuilder.AddColumn<bool>(
                name: "flgFullDay",
                table: "LeaveTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "flgFullDay",
                table: "BreakTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
