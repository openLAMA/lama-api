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
    public partial class AddTestingPersonnelTablesAndStatusesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestingPersonnelInvitation",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SentByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InvitationForDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RequiredPersonnelCountShift1 = table.Column<int>(type: "integer", nullable: false),
                    RequiredPersonnelCountShift2 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingPersonnelInvitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingPersonnelInvitation_Users_SentByUserId",
                        column: x => x.SentByUserId,
                        principalSchema: "ApiDb",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestingPersonnelStatus",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingPersonnelStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestingPersonnel",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    Employeer = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingPersonnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingPersonnel_TestingPersonnelStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "ApiDb",
                        principalTable: "TestingPersonnelStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestingPersonnelWorkingArea",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TestingPersonnelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Area = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingPersonnelWorkingArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingPersonnelWorkingArea_TestingPersonnel_TestingPersonn~",
                        column: x => x.TestingPersonnelId,
                        principalSchema: "ApiDb",
                        principalTable: "TestingPersonnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "TestingPersonnelStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0c66bd94-5f3e-4aca-ab58-2b2b8c12d526"), "BSc" },
                    { new Guid("c67d1068-255a-4c89-b590-a4531154508a"), "MSc (Head)" },
                    { new Guid("d58d11d7-740a-4acf-b380-21d4b88d4aec"), "BSc (Head)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestingPersonnel_StatusId",
                schema: "ApiDb",
                table: "TestingPersonnel",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingPersonnelInvitation_InvitationForDate",
                schema: "ApiDb",
                table: "TestingPersonnelInvitation",
                column: "InvitationForDate");

            migrationBuilder.CreateIndex(
                name: "IX_TestingPersonnelInvitation_SentByUserId",
                schema: "ApiDb",
                table: "TestingPersonnelInvitation",
                column: "SentByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingPersonnelWorkingArea_TestingPersonnelId",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingArea",
                column: "TestingPersonnelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestingPersonnelInvitation",
                schema: "ApiDb");

            migrationBuilder.DropTable(
                name: "TestingPersonnelWorkingArea",
                schema: "ApiDb");

            migrationBuilder.DropTable(
                name: "TestingPersonnel",
                schema: "ApiDb");

            migrationBuilder.DropTable(
                name: "TestingPersonnelStatus",
                schema: "ApiDb");
        }
    }
}
