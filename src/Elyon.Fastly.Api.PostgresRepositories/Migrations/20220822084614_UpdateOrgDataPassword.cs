using Microsoft.EntityFrameworkCore.Migrations;

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    public partial class UpdateOrgDataPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataPassword",
                schema: "ApiDb",
                table: "Organizations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPassword",
                schema: "ApiDb",
                table: "Organizations");
        }
    }
}
