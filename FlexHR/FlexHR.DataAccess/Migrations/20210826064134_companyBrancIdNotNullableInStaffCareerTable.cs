using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class companyBrancIdNotNullableInStaffCareerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffCareer_CompanyBranch",
                table: "StaffCareer");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyBranchId",
                table: "StaffCareer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffCareer_CompanyBranch",
                table: "StaffCareer",
                column: "CompanyBranchId",
                principalTable: "CompanyBranch",
                principalColumn: "CompanyBranchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffCareer_CompanyBranch",
                table: "StaffCareer");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyBranchId",
                table: "StaffCareer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffCareer_CompanyBranch",
                table: "StaffCareer",
                column: "CompanyBranchId",
                principalTable: "CompanyBranch",
                principalColumn: "CompanyBranchId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
