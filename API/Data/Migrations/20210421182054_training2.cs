using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class training2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesTraining_TrainingId",
                table: "EmployeesTraining",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesTraining_Training_TrainingId",
                table: "EmployeesTraining",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesTraining_Users_EmployeeId",
                table: "EmployeesTraining",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesTraining_Training_TrainingId",
                table: "EmployeesTraining");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesTraining_Users_EmployeeId",
                table: "EmployeesTraining");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropIndex(
                name: "IX_EmployeesTraining_TrainingId",
                table: "EmployeesTraining");
        }
    }
}
