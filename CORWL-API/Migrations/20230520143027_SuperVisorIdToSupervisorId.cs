using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORWL_API.Migrations
{
    public partial class SuperVisorIdToSupervisorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuperVisorId",
                table: "EmployeeJobDetails",
                newName: "SupervisorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "EmployeeJobDetails",
                newName: "SuperVisorId");
        }
    }
}
