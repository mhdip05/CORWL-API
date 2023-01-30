using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NMS_API_N.Migrations
{
    public partial class AddCompanyColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Companies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2556)",
                oldMaxLength: 2556);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Companies",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CompanyCode",
                table: "Companies",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ConversionRate",
                table: "Companies",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Companies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterNationalCurrency",
                table: "Companies",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsParentCompany",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LocalCurrency",
                table: "Companies",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Companies",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNo",
                table: "Companies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "Companies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Web",
                table: "Companies",
                type: "nvarchar(126)",
                maxLength: 126,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Companies",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                table: "Companies",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Cities_CityId",
                table: "Companies",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Cities_CityId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CityId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyCode",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ConversionRate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "InterNationalCurrency",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsParentCompany",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LocalCurrency",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MobileNo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Web",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Companies",
                type: "nvarchar(2556)",
                maxLength: 2556,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
