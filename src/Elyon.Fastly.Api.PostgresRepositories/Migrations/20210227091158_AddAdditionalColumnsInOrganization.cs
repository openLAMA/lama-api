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

using Microsoft.EntityFrameworkCore.Migrations;

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    public partial class AddAdditionalColumnsInOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalTestTubes",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                schema: "ApiDb",
                table: "Organizations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "County",
                schema: "ApiDb",
                table: "Organizations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeesCount",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manager",
                schema: "ApiDb",
                table: "Organizations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NaclLosing",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBags",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRakoBoxes",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrioLogistic",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegisteredEmployees",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolType",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentsCount",
                schema: "ApiDb",
                table: "Organizations",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalTestTubes",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Area",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "County",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "EmployeesCount",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Manager",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "NaclLosing",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "NumberOfBags",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "NumberOfRakoBoxes",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "PrioLogistic",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "RegisteredEmployees",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "SchoolType",
                schema: "ApiDb",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "StudentsCount",
                schema: "ApiDb",
                table: "Organizations");
        }
    }
}
