using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORWL_API.Migrations
{
    public partial class SpeelCorrectMaritalStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MartialStatus",
                table: "Employees",
                newName: "MaritalStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaritalStatus",
                table: "Employees",
                newName: "MartialStatus");
        }
    }
}
