using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMS_API_N.Migrations
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
