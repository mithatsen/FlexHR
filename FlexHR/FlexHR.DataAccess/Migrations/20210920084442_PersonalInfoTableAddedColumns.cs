using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class PersonalInfoTableAddedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChildrenCount",
                table: "StaffPersonelInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRetired",
                table: "StaffPersonelInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PartnerWorkingStatus",
                table: "StaffPersonelInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildrenCount",
                table: "StaffPersonelInfo");

            migrationBuilder.DropColumn(
                name: "IsRetired",
                table: "StaffPersonelInfo");

            migrationBuilder.DropColumn(
                name: "PartnerWorkingStatus",
                table: "StaffPersonelInfo");
        }
    }
}
