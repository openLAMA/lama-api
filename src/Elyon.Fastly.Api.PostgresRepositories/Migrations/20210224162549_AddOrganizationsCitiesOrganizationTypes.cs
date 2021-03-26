#region Copyright
// openLAMA is an open source platform which has been developed by the
// Swiss Kanton Basel Landschaft, with the goal of automating and managing
// large scale Covid testing programs or any other pandemic/viral infections.

// Copyright(C) 2021 Kanton Basel Landschaft, Switzerland
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// See LICENSE.md in the project root for license information.
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see https://www.gnu.org/licenses/.
#endregion

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    public partial class AddOrganizationsCitiesOrganizationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "ApiDb",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationTypes",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cantons",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cantons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cantons_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "ApiDb",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CantonId = table.Column<Guid>(type: "uuid", nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Cantons_CantonId",
                        column: x => x.CantonId,
                        principalSchema: "ApiDb",
                        principalTable: "Cantons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "ApiDb",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EpaadId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OrganizationTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreationOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Zip = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OnboardingTimestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstTestTimestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SecondTestTimestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NumberOfSamples = table.Column<int>(type: "integer", nullable: false),
                    NumberOfPools = table.Column<int>(type: "integer", nullable: false),
                    SupportPersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "ApiDb",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organizations_OrganizationTypes_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalSchema: "ApiDb",
                        principalTable: "OrganizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_SupportPersonId",
                        column: x => x.SupportPersonId,
                        principalSchema: "ApiDb",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                schema: "ApiDb",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cantons_CountryId",
                schema: "ApiDb",
                table: "Cantons",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CantonId",
                schema: "ApiDb",
                table: "Cities",
                column: "CantonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                schema: "ApiDb",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CityId",
                schema: "ApiDb",
                table: "Organizations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationTypeId",
                schema: "ApiDb",
                table: "Organizations",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_SupportPersonId",
                schema: "ApiDb",
                table: "Organizations",
                column: "SupportPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                schema: "ApiDb",
                table: "Users",
                column: "OrganizationId",
                principalSchema: "ApiDb",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                schema: "ApiDb",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "ApiDb");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "ApiDb");

            migrationBuilder.DropTable(
                name: "OrganizationTypes",
                schema: "ApiDb");

            migrationBuilder.DropTable(
                name: "Cantons",
                schema: "ApiDb");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "ApiDb");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationId",
                schema: "ApiDb",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "ApiDb",
                table: "Users");
        }
    }
}
