using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class addAuthorizeTypeGeneralSubTypeIdOnAppRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorizeTypeGeneralSubTypeId",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizeTypeGeneralSubTypeId",
                table: "AspNetRoles");
        }
    }
}
