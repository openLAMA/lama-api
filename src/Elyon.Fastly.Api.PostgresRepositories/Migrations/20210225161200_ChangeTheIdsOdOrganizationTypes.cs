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
    public partial class ChangeTheIdsOdOrganizationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Cantons_CantonId",
                schema: "ApiDb",
                table: "Cities");

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 82000, "Company" },
                    { 82001, "Pharmacy" },
                    { 82002, "School" },
                    { 82003, "Nursing Home" },
                    { 82004, "Hospital" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Cantons_CantonId",
                schema: "ApiDb",
                table: "Cities",
                column: "CantonId",
                principalSchema: "ApiDb",
                principalTable: "Cantons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Cantons_CantonId",
                schema: "ApiDb",
                table: "Cities");

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 82000);

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 82001);

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 82002);

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 82003);

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                keyColumn: "Id",
                keyValue: 82004);            

            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "OrganizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Nursing Home" },
                    { 1, "Company" },
                    { 2, "Pharmacy" },
                    { 3, "School" },
                    { 5, "Hospital" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Cantons_CantonId",
                schema: "ApiDb",
                table: "Cities",
                column: "CantonId",
                principalSchema: "ApiDb",
                principalTable: "Cantons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
