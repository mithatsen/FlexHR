using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class change_salary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossSalary",
                table: "StaffSalary");

            migrationBuilder.DropColumn(
                name: "NetSalary",
                table: "StaffSalary");

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "StaffSalary",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "StaffSalary");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossSalary",
                table: "StaffSalary",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NetSalary",
                table: "StaffSalary",
                type: "numeric(18,2)",
                nullable: true);
        }
    }
}
