using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class changeStaffSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAGI",
                table: "StaffSalary");

            migrationBuilder.DropColumn(
                name: "TotalWorkingHour",
                table: "StaffSalary");

            migrationBuilder.RenameColumn(
                name: "OvertimePayPerHour",
                table: "StaffSalary",
                newName: "PayPerHour");

            migrationBuilder.AddColumn<decimal>(
                name: "AgiPayment",
                table: "StaffSalary",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgiPayment",
                table: "StaffSalary");

            migrationBuilder.RenameColumn(
                name: "PayPerHour",
                table: "StaffSalary",
                newName: "OvertimePayPerHour");

            migrationBuilder.AddColumn<bool>(
                name: "IsAGI",
                table: "StaffSalary",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalWorkingHour",
                table: "StaffSalary",
                type: "int",
                nullable: true);
        }
    }
}
