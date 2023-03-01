using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMS_API_N.Migrations
{
    public partial class RemoveIdTypeAndIdNoFromEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdType",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdNo",
                table: "Employees",
                type: "int",
                maxLength: 128,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdType",
                table: "Employees",
                type: "int",
                maxLength: 128,
                nullable: false,
                defaultValue: 0);
        }
    }
}
