﻿#region Copyright
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
    public partial class AddCantonWeekdaysSamples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CantonWeekdaysSamples",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MondaySamples = table.Column<int>(type: "integer", nullable: false),
                    TuesdaySamples = table.Column<int>(type: "integer", nullable: false),
                    WednesdaySamples = table.Column<int>(type: "integer", nullable: false),
                    ThursdaySamples = table.Column<int>(type: "integer", nullable: false),
                    FridaySamples = table.Column<int>(type: "integer", nullable: false),
                    CantonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CantonWeekdaysSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CantonWeekdaysSamples_Cantons_CantonId",
                        column: x => x.CantonId,
                        principalSchema: "ApiDb",
                        principalTable: "Cantons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CantonWeekdaysSamples_CantonId",
                schema: "ApiDb",
                table: "CantonWeekdaysSamples",
                column: "CantonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CantonWeekdaysSamples",
                schema: "ApiDb");
        }
    }
}
