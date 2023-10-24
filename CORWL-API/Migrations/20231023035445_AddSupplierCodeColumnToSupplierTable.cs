using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORWL_API.Migrations
{
    public partial class AddSupplierCodeColumnToSupplierTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupplierCode",
                table: "Suppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierCode",
                table: "Suppliers");
        }
    }
}
