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
    public partial class AddSupportPersonalWeekdayShifts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FridayShift",
                schema: "ApiDb",
                table: "TestingPersonnels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasFixedWorkingDays",
                schema: "ApiDb",
                table: "TestingPersonnels",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MondayShift",
                schema: "ApiDb",
                table: "TestingPersonnels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThursdayShift",
                schema: "ApiDb",
                table: "TestingPersonnels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TuesdayShift",
                schema: "ApiDb",
                table: "TestingPersonnels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WednesdayShift",
                schema: "ApiDb",
                table: "TestingPersonnels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FridayShift",
                schema: "ApiDb",
                table: "TestingPersonnels");

            migrationBuilder.DropColumn(
                name: "HasFixedWorkingDays",
                schema: "ApiDb",
                table: "TestingPersonnels");

            migrationBuilder.DropColumn(
                name: "MondayShift",
                schema: "ApiDb",
                table: "TestingPersonnels");

            migrationBuilder.DropColumn(
                name: "ThursdayShift",
                schema: "ApiDb",
                table: "TestingPersonnels");

            migrationBuilder.DropColumn(
                name: "TuesdayShift",
                schema: "ApiDb",
                table: "TestingPersonnels");

            migrationBuilder.DropColumn(
                name: "WednesdayShift",
                schema: "ApiDb",
                table: "TestingPersonnels");
        }
    }
}
