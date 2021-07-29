using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class UpdatePublicHolidayColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "PublicHoliday",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PublicHoliday",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "FinishDate",
                table: "PublicHoliday",
                newName: "End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "PublicHoliday",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "PublicHoliday",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "PublicHoliday",
                newName: "FinishDate");
        }
    }
}
