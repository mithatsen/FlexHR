using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class FileColumnTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileColumn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFileTypeGeneralSubTypeId = table.Column<int>(type: "int", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ColumnDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ColumnSequence = table.Column<int>(type: "int", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AllowNulls = table.Column<bool>(type: "bit", nullable: false),
                    IsExistInExcel = table.Column<bool>(type: "bit", nullable: false),
                    IsManuellAdded = table.Column<bool>(type: "bit", nullable: false),
                    IsExistControl = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileColumn", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileColumn");
        }
    }
}
