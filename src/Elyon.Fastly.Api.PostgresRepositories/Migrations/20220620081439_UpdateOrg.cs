using Microsoft.EntityFrameworkCore.Migrations;

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    public partial class UpdateOrg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubTypeCode",
                schema: "ApiDb",
                table: "Organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTypeName",
                schema: "ApiDb",
                table: "Organizations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTypeCode",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "SubTypeName",
                schema: "ApiDb",
                table: "Organizations");
        }
    }
}
