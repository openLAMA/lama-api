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
    public partial class AddInvitationConfirmationToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestingPersonnelInvitationConfirmationTokens",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TestingPersonnelId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestingPersonnelInvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingPersonnelInvitationConfirmationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingPersonnelInvitationConfirmationTokens_TestingPerson~1",
                        column: x => x.TestingPersonnelInvitationId,
                        principalSchema: "ApiDb",
                        principalTable: "TestingPersonnelInvitations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestingPersonnelInvitationConfirmationTokens_TestingPersonn~",
                        column: x => x.TestingPersonnelId,
                        principalSchema: "ApiDb",
                        principalTable: "TestingPersonnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestingPersonnelInvitationConfirmationTokens_TestingPerson~1",
                schema: "ApiDb",
                table: "TestingPersonnelInvitationConfirmationTokens",
                column: "TestingPersonnelInvitationId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingPersonnelInvitationConfirmationTokens_TestingPersonn~",
                schema: "ApiDb",
                table: "TestingPersonnelInvitationConfirmationTokens",
                column: "TestingPersonnelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestingPersonnelInvitationConfirmationTokens",
                schema: "ApiDb");
        }
    }
}
