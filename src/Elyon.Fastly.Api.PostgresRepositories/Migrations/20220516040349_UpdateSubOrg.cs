using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    public partial class UpdateSubOrg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                schema: "ApiDb",
                table: "SubOrganizations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingName",
                schema: "ApiDb",
                table: "SubOrganizations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                schema: "ApiDb",
                table: "SubOrganizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "County",
                schema: "ApiDb",
                table: "SubOrganizations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfParticipants",
                schema: "ApiDb",
                table: "SubOrganizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingContact",
                schema: "ApiDb",
                table: "SubOrganizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingEmail",
                schema: "ApiDb",
                table: "SubOrganizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                schema: "ApiDb",
                table: "SubOrganizations",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubOrganizations_CityId",
                schema: "ApiDb",
                table: "SubOrganizations",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubOrganizations_Cities_CityId",
                schema: "ApiDb",
                table: "SubOrganizations",
                column: "CityId",
                principalSchema: "ApiDb",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubOrganizations_Cities_CityId",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_SubOrganizations_CityId",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropColumn(
                name: "Area",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropColumn(
                name: "BuildingName",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropColumn(
                name: "County",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropColumn(
                name: "NumberOfParticipants",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropColumn(
                name: "ReportingContact",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropColumn(
                name: "ReportingEmail",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropColumn(
                name: "Zip",
                schema: "ApiDb",
                table: "SubOrganizations");
        }
    }
}
