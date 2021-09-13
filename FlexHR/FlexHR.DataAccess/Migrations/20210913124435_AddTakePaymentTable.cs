using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class AddTakePaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WhoApprovedStaffId",
                table: "StaffShift",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WhoApprovedStaffId",
                table: "StaffPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WhoApprovedStaffId",
                table: "StaffLeave",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TakePayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPaymentId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TakePayment_StaffPayment_StaffPaymentId",
                        column: x => x.StaffPaymentId,
                        principalTable: "StaffPayment",
                        principalColumn: "StaffPaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TakePayment_StaffPaymentId",
                table: "TakePayment",
                column: "StaffPaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TakePayment");

            migrationBuilder.DropColumn(
                name: "WhoApprovedStaffId",
                table: "StaffShift");

            migrationBuilder.DropColumn(
                name: "WhoApprovedStaffId",
                table: "StaffPayment");

            migrationBuilder.DropColumn(
                name: "WhoApprovedStaffId",
                table: "StaffLeave");
        }
    }
}
