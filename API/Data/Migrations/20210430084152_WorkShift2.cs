using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class WorkShift2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_WorkShifts_ShiftId",
                table: "Calendar");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "Calendar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_WorkShifts_ShiftId",
                table: "Calendar",
                column: "ShiftId",
                principalTable: "WorkShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_WorkShifts_ShiftId",
                table: "Calendar");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "Calendar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_WorkShifts_ShiftId",
                table: "Calendar",
                column: "ShiftId",
                principalTable: "WorkShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
