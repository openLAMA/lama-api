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
    public partial class MakeLandLineNumberRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update \"ApiDb\".\"Users\" set \"LandLineNumber\" = \'SSEfX6+eGdQho09y+G6bag==\' where \"LandLineNumber\" is null");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "ApiDb",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "LandLineNumber",
                schema: "ApiDb",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("076466c2-1540-4a4d-98eb-12bea4a8c71b"),
                column: "LandLineNumber",
                value: "SSEfX6+eGdQho09y+G6bag==");

            migrationBuilder.UpdateData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4491038d-39a8-410a-97b0-60f7f604d5ac"),
                column: "LandLineNumber",
                value: "SSEfX6+eGdQho09y+G6bag==");

            migrationBuilder.UpdateData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                column: "LandLineNumber",
                value: "SSEfX6+eGdQho09y+G6bag==");

            migrationBuilder.UpdateData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("917aea9f-89d1-43e9-95ad-43969a1f276c"),
                column: "LandLineNumber",
                value: "SSEfX6+eGdQho09y+G6bag==");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "ApiDb",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LandLineNumber",
                schema: "ApiDb",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("076466c2-1540-4a4d-98eb-12bea4a8c71b"),
                column: "LandLineNumber",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4491038d-39a8-410a-97b0-60f7f604d5ac"),
                column: "LandLineNumber",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                column: "LandLineNumber",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("917aea9f-89d1-43e9-95ad-43969a1f276c"),
                column: "LandLineNumber",
                value: null);
        }
    }
}
