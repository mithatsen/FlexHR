using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class TotalWorkingHourAndOvertimePayPerHourColumnAddedToSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OvertimePayPerHour",
                table: "StaffSalary",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalWorkingHour",
                table: "StaffSalary",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OvertimePayPerHour",
                table: "StaffSalary");

            migrationBuilder.DropColumn(
                name: "TotalWorkingHour",
                table: "StaffSalary");
        }
    }
}
