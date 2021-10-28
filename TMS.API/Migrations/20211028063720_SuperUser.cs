using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.API.Migrations
{
    public partial class SuperUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "flgSuper",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flgSuper",
                table: "Users");
        }
    }
}
