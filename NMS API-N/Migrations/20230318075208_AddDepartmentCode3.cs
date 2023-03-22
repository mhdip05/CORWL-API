using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMS_API_N.Migrations
{
    public partial class AddDepartmentCode3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "Departments",
                type: "nvarchar(28)",
                maxLength: 28,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "Departments");
        }
    }
}
