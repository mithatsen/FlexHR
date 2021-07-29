using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class addhalfdayonpublicholiday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolidayDuration",
                table: "PublicHoliday");

            migrationBuilder.RenameColumn(
                name: "IsFullDay",
                table: "PublicHoliday",
                newName: "IsHalfDay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsHalfDay",
                table: "PublicHoliday",
                newName: "IsFullDay");

            migrationBuilder.AddColumn<decimal>(
                name: "HolidayDuration",
                table: "PublicHoliday",
                type: "numeric(3,1)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
