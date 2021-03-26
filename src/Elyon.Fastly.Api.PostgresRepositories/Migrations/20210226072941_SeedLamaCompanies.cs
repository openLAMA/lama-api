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
    public partial class SeedLamaCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "LamaCompanies",
                columns: new[] { "Id", "Name", "RoleType" },
                values: new object[,]
                {
                    { new Guid("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"), "University", 1 },
                    { new Guid("dd015958-aa5d-4568-850a-557a3c73f336"), "Laboratory", 2 },
                    { new Guid("13585770-17b3-4a9e-bbd7-4dff9cc9a5e2"), "Logistics", 3 },
                    { new Guid("bae8c907-4851-40ae-99ee-fbdf8dc43c83"), "State", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "LamaCompanies",
                keyColumn: "Id",
                keyValue: new Guid("13585770-17b3-4a9e-bbd7-4dff9cc9a5e2"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "LamaCompanies",
                keyColumn: "Id",
                keyValue: new Guid("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "LamaCompanies",
                keyColumn: "Id",
                keyValue: new Guid("bae8c907-4851-40ae-99ee-fbdf8dc43c83"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "LamaCompanies",
                keyColumn: "Id",
                keyValue: new Guid("dd015958-aa5d-4568-850a-557a3c73f336"));
        }
    }
}
