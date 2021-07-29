using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class AddPublicHolidayTableFulDayColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFullDay",
                table: "Event");

            migrationBuilder.AddColumn<bool>(
                name: "IsFullDay",
                table: "PublicHoliday",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFullDay",
                table: "PublicHoliday");

            migrationBuilder.AddColumn<bool>(
                name: "IsFullDay",
                table: "Event",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
