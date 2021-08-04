using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class RemoveStaffGeneralSubTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staff_GeneralSubType");

            migrationBuilder.AddColumn<int>(
                name: "ContractTypeGeneralSubTypeId",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractTypeGeneralSubTypeId",
                table: "Staff");

            migrationBuilder.CreateTable(
                name: "Staff_GeneralSubType",
                columns: table => new
                {
                    GeneralSubTypeStaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralSubTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff_GeneralSubType", x => x.GeneralSubTypeStaffId);
                    table.ForeignKey(
                        name: "FK_Staff_GeneralSubType_GeneralSubType",
                        column: x => x.GeneralSubTypeId,
                        principalTable: "GeneralSubType",
                        principalColumn: "GeneralSubTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_GeneralSubType_Staff",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_GeneralSubType_GeneralSubTypeId",
                table: "Staff_GeneralSubType",
                column: "GeneralSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_GeneralSubType_StaffId",
                table: "Staff_GeneralSubType",
                column: "StaffId");
        }
    }
}
