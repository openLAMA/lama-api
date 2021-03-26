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

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    public partial class AddSuborganizationsAndNewTestdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FourthTestTimestamp",
                schema: "ApiDb",
                table: "Organizations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThirdTestTimestamp",
                schema: "ApiDb",
                table: "Organizations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubOrganization",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubOrganization_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ApiDb",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubOrganization_OrganizationId",
                schema: "ApiDb",
                table: "SubOrganization",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubOrganization",
                schema: "ApiDb");

            migrationBuilder.DropColumn(
                name: "FourthTestTimestamp",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ThirdTestTimestamp",
                schema: "ApiDb",
                table: "Organizations");
        }
    }
}
