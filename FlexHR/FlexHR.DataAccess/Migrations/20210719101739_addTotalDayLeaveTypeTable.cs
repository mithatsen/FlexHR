using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class addTotalDayLeaveTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalDay",
                table: "LeaveType",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDay",
                table: "LeaveType");
        }
    }
}
