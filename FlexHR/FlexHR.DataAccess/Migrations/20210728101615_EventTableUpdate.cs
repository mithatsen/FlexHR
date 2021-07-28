using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexHR.DataAccess.Migrations
{
    public partial class EventTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Event",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Event",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Event",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Event",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Event",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Event",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Event",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Event",
                newName: "EventId");
        }
    }
}
