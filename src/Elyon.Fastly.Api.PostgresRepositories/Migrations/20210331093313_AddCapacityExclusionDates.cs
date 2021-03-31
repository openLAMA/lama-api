using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    public partial class AddCapacityExclusionDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExclusionEndDate",
                schema: "ApiDb",
                table: "Organizations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExclusionStartDate",
                schema: "ApiDb",
                table: "Organizations",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExclusionEndDate",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ExclusionStartDate",
                schema: "ApiDb",
                table: "Organizations");
        }
    }
}
