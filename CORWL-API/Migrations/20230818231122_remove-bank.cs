using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CORWL_API.Migrations
{
    public partial class removebank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankAccountNo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    BankAddress = table.Column<string>(type: "character varying(521)", maxLength: 521, nullable: true),
                    BankBranch = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    BankName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    SourceId = table.Column<int>(type: "integer", nullable: false),
                    SourceType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    SwiftCode = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });
        }
    }
}
