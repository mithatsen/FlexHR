using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class addGeneralStatusOnStaffShiftTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneralStatusGeneralSubTypeId",
                table: "StaffShift",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneralStatusGeneralSubTypeId",
                table: "StaffShift");
        }
    }
}
