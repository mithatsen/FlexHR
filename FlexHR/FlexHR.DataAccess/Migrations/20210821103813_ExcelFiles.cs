using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class ExcelFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileTypeId",
                table: "FileColumn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FileColumnProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileColumnProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FileUploadTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileColumn_FileColumnProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileColumnId = table.Column<int>(type: "int", nullable: false),
                    FileColumnPropertiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileColumn_FileColumnProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileColumnFileColumnProperties_FileColumn",
                        column: x => x.FileColumnId,
                        principalTable: "FileColumn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileColumnFileColumnProperties_FileColumnProperties",
                        column: x => x.FileColumnPropertiesId,
                        principalTable: "FileColumnProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileColumn_FileTypeId",
                table: "FileColumn",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FileColumn_FileColumnProperties_FileColumnId",
                table: "FileColumn_FileColumnProperties",
                column: "FileColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_FileColumn_FileColumnProperties_FileColumnPropertiesId",
                table: "FileColumn_FileColumnProperties",
                column: "FileColumnPropertiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileColumn_FileType",
                table: "FileColumn",
                column: "FileTypeId",
                principalTable: "FileType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileColumn_FileType",
                table: "FileColumn");

            migrationBuilder.DropTable(
                name: "FileColumn_FileColumnProperties");

            migrationBuilder.DropTable(
                name: "FileType");

            migrationBuilder.DropTable(
                name: "FileColumnProperties");

            migrationBuilder.DropIndex(
                name: "IX_FileColumn_FileTypeId",
                table: "FileColumn");

            migrationBuilder.DropColumn(
                name: "FileTypeId",
                table: "FileColumn");
        }
    }
}
