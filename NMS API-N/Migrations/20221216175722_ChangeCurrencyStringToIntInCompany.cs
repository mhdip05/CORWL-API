using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMS_API_N.Migrations
{
    public partial class ChangeCurrencyStringToIntInCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterNationalCurrency",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LocalCurrency",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "InterNationalCurrencyId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalCurrencyId",
                table: "Companies",
                type: "int",
                maxLength: 16,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterNationalCurrencyId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LocalCurrencyId",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "InterNationalCurrency",
                table: "Companies",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalCurrency",
                table: "Companies",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);
        }
    }
}
