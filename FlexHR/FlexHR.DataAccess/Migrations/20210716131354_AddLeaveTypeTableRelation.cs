using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class AddLeaveTypeTableRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveTypeId",
                table: "StaffLeave",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StaffLeave_LeaveTypeId",
                table: "StaffLeave",
                column: "LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffLeave_LeaveType",
                table: "StaffLeave",
                column: "LeaveTypeId",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffLeave_LeaveType",
                table: "StaffLeave");

            migrationBuilder.DropIndex(
                name: "IX_StaffLeave_LeaveTypeId",
                table: "StaffLeave");

            migrationBuilder.DropColumn(
                name: "LeaveTypeId",
                table: "StaffLeave");
        }
    }
}
