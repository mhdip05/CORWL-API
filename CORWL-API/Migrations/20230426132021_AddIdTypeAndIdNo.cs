using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORWL_API.Migrations
{
    public partial class AddIdTypeAndIdNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdNo",
                table: "Employees",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdType",
                table: "Employees",
                type: "character varying(56)",
                maxLength: 56,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdType",
                table: "Employees");
        }
    }
}
