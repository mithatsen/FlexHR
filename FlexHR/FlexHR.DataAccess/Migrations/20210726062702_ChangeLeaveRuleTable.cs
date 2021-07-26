using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class ChangeLeaveRuleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualLeaveCount",
                table: "LeaveRules");

            migrationBuilder.RenameColumn(
                name: "MinYear",
                table: "LeaveRules",
                newName: "SeniorityYear");

            migrationBuilder.RenameColumn(
                name: "MaxYear",
                table: "LeaveRules",
                newName: "AditionalLeaveAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeniorityYear",
                table: "LeaveRules",
                newName: "MinYear");

            migrationBuilder.RenameColumn(
                name: "AditionalLeaveAmount",
                table: "LeaveRules",
                newName: "MaxYear");

            migrationBuilder.AddColumn<int>(
                name: "AnnualLeaveCount",
                table: "LeaveRules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
