using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class RotationTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobRotationId",
                table: "Staff",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobRotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRotation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobRotationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobRotationId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    JobRotationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRotationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRotationHistory_JobRotation_JobRotationId",
                        column: x => x.JobRotationId,
                        principalTable: "JobRotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_JobRotationId",
                table: "Staff",
                column: "JobRotationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRotationHistory_JobRotationId",
                table: "JobRotationHistory",
                column: "JobRotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_JobRotation_JobRotationId",
                table: "Staff",
                column: "JobRotationId",
                principalTable: "JobRotation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_JobRotation_JobRotationId",
                table: "Staff");

            migrationBuilder.DropTable(
                name: "JobRotationHistory");

            migrationBuilder.DropTable(
                name: "JobRotation");

            migrationBuilder.DropIndex(
                name: "IX_Staff_JobRotationId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "JobRotationId",
                table: "Staff");
        }
    }
}
