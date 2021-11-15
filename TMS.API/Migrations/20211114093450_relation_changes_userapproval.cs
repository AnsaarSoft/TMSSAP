using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.API.Migrations
{
    public partial class relation_changes_userapproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApprovals_TimeSheets_oDocumentID",
                table: "UserApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApprovals_Users_oUserID",
                table: "UserApprovals");

            migrationBuilder.DropIndex(
                name: "IX_UserApprovals_oDocumentID",
                table: "UserApprovals");

            migrationBuilder.DropIndex(
                name: "IX_UserApprovals_oUserID",
                table: "UserApprovals");

            migrationBuilder.DropColumn(
                name: "oDocumentID",
                table: "UserApprovals");

            migrationBuilder.DropColumn(
                name: "oUserID",
                table: "UserApprovals");

            migrationBuilder.AddColumn<int>(
                name: "rDocument",
                table: "UserApprovals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rUser",
                table: "UserApprovals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rDocument",
                table: "UserApprovals");

            migrationBuilder.DropColumn(
                name: "rUser",
                table: "UserApprovals");

            migrationBuilder.AddColumn<int>(
                name: "oDocumentID",
                table: "UserApprovals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "oUserID",
                table: "UserApprovals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserApprovals_oDocumentID",
                table: "UserApprovals",
                column: "oDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_UserApprovals_oUserID",
                table: "UserApprovals",
                column: "oUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApprovals_TimeSheets_oDocumentID",
                table: "UserApprovals",
                column: "oDocumentID",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserApprovals_Users_oUserID",
                table: "UserApprovals",
                column: "oUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
