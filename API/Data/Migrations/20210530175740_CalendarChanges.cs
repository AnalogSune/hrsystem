using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class CalendarChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_WorkShifts_ShiftId",
                table: "Calendar");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_WorkShifts_ShiftId",
                table: "Calendar",
                column: "ShiftId",
                principalTable: "WorkShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_WorkShifts_ShiftId",
                table: "Calendar");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_WorkShifts_ShiftId",
                table: "Calendar",
                column: "ShiftId",
                principalTable: "WorkShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
