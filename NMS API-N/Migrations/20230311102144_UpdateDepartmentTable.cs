using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMS_API_N.Migrations
{
    public partial class UpdateDepartmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Cities_CityId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CityId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Departments",
                newName: "DepartmentHead");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentAddress",
                table: "Departments",
                type: "int",
                maxLength: 128,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentCode",
                table: "Departments",
                type: "int",
                maxLength: 128,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentAddress",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "DepartmentHead",
                table: "Departments",
                newName: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CityId",
                table: "Departments",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Cities_CityId",
                table: "Departments",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
