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
    public partial class RenameTestingPersonnelsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestingPersonnel_TestingPersonnelStatus_StatusId",
                schema: "ApiDb",
                table: "TestingPersonnel");

            migrationBuilder.DropForeignKey(
                name: "FK_TestingPersonnelInvitation_Users_SentByUserId",
                schema: "ApiDb",
                table: "TestingPersonnelInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_TestingPersonnelWorkingArea_TestingPersonnel_TestingPersonn~",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestingPersonnelWorkingArea",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestingPersonnelStatus",
                schema: "ApiDb",
                table: "TestingPersonnelStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestingPersonnelInvitation",
                schema: "ApiDb",
                table: "TestingPersonnelInvitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestingPersonnel",
                schema: "ApiDb",
                table: "TestingPersonnel");

            migrationBuilder.RenameTable(
                name: "TestingPersonnelWorkingArea",
                schema: "ApiDb",
                newName: "TestingPersonnelWorkingAreas",
                newSchema: "ApiDb");

            migrationBuilder.RenameTable(
                name: "TestingPersonnelStatus",
                schema: "ApiDb",
                newName: "TestingPersonnelStatuses",
                newSchema: "ApiDb");

            migrationBuilder.RenameTable(
                name: "TestingPersonnelInvitation",
                schema: "ApiDb",
                newName: "TestingPersonnelInvitations",
                newSchema: "ApiDb");

            migrationBuilder.RenameTable(
                name: "TestingPersonnel",
                schema: "ApiDb",
                newName: "TestingPersonnels",
                newSchema: "ApiDb");

            migrationBuilder.RenameIndex(
                name: "IX_TestingPersonnelWorkingArea_TestingPersonnelId",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingAreas",
                newName: "IX_TestingPersonnelWorkingAreas_TestingPersonnelId");

            migrationBuilder.RenameIndex(
                name: "IX_TestingPersonnelInvitation_SentByUserId",
                schema: "ApiDb",
                table: "TestingPersonnelInvitations",
                newName: "IX_TestingPersonnelInvitations_SentByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestingPersonnelInvitation_InvitationForDate",
                schema: "ApiDb",
                table: "TestingPersonnelInvitations",
                newName: "IX_TestingPersonnelInvitations_InvitationForDate");

            migrationBuilder.RenameIndex(
                name: "IX_TestingPersonnel_StatusId",
                schema: "ApiDb",
                table: "TestingPersonnels",
                newName: "IX_TestingPersonnels_StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestingPersonnelWorkingAreas",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingAreas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestingPersonnelStatuses",
                schema: "ApiDb",
                table: "TestingPersonnelStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestingPersonnelInvitations",
                schema: "ApiDb",
                table: "TestingPersonnelInvitations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestingPersonnels",
                schema: "ApiDb",
                table: "TestingPersonnels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TestingPersonnelConfirmations",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestingPersonnelInvitationId = table.Column<Guid>(type: "uuid", nullable: true),
                    AcceptedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftNumber = table.Column<int>(type: "integer", nullable: false),
                    PersonnelId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestingPersonnelId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingPersonnelConfirmations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingPersonnelConfirmations_TestingPersonnelInvitations_T~",
                        column: x => x.TestingPersonnelInvitationId,
                        principalSchema: "ApiDb",
                        principalTable: "TestingPersonnelInvitations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestingPersonnelConfirmations_TestingPersonnels_TestingPers~",
                        column: x => x.TestingPersonnelId,
                        principalSchema: "ApiDb",
                        principalTable: "TestingPersonnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestingPersonnelConfirmations_TestingPersonnelId",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations",
                column: "TestingPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingPersonnelConfirmations_TestingPersonnelInvitationId",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations",
                column: "TestingPersonnelInvitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestingPersonnelInvitations_Users_SentByUserId",
                schema: "ApiDb",
                table: "TestingPersonnelInvitations",
                column: "SentByUserId",
                principalSchema: "ApiDb",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestingPersonnels_TestingPersonnelStatuses_StatusId",
                schema: "ApiDb",
                table: "TestingPersonnels",
                column: "StatusId",
                principalSchema: "ApiDb",
                principalTable: "TestingPersonnelStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestingPersonnelWorkingAreas_TestingPersonnels_TestingPerso~",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingAreas",
                column: "TestingPersonnelId",
                principalSchema: "ApiDb",
                principalTable: "TestingPersonnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestingPersonnelInvitations_Users_SentByUserId",
                schema: "ApiDb",
                table: "TestingPersonnelInvitations");

            migrationBuilder.DropForeignKey(
                name: "FK_TestingPersonnels_TestingPersonnelStatuses_StatusId",
                schema: "ApiDb",
                table: "TestingPersonnels");

            migrationBuilder.DropForeignKey(
                name: "FK_TestingPersonnelWorkingAreas_TestingPersonnels_TestingPerso~",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingAreas");

            migrationBuilder.DropTable(
                name: "TestingPersonnelConfirmations",
                schema: "ApiDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestingPersonnelWorkingAreas",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestingPersonnelStatuses",
                schema: "ApiDb",
                table: "TestingPersonnelStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestingPersonnels",
                schema: "ApiDb",
                table: "TestingPersonnels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestingPersonnelInvitations",
                schema: "ApiDb",
                table: "TestingPersonnelInvitations");

            migrationBuilder.RenameTable(
                name: "TestingPersonnelWorkingAreas",
                schema: "ApiDb",
                newName: "TestingPersonnelWorkingArea",
                newSchema: "ApiDb");

            migrationBuilder.RenameTable(
                name: "TestingPersonnelStatuses",
                schema: "ApiDb",
                newName: "TestingPersonnelStatus",
                newSchema: "ApiDb");

            migrationBuilder.RenameTable(
                name: "TestingPersonnels",
                schema: "ApiDb",
                newName: "TestingPersonnel",
                newSchema: "ApiDb");

            migrationBuilder.RenameTable(
                name: "TestingPersonnelInvitations",
                schema: "ApiDb",
                newName: "TestingPersonnelInvitation",
                newSchema: "ApiDb");

            migrationBuilder.RenameIndex(
                name: "IX_TestingPersonnelWorkingAreas_TestingPersonnelId",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingArea",
                newName: "IX_TestingPersonnelWorkingArea_TestingPersonnelId");

            migrationBuilder.RenameIndex(
                name: "IX_TestingPersonnels_StatusId",
                schema: "ApiDb",
                table: "TestingPersonnel",
                newName: "IX_TestingPersonnel_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_TestingPersonnelInvitations_SentByUserId",
                schema: "ApiDb",
                table: "TestingPersonnelInvitation",
                newName: "IX_TestingPersonnelInvitation_SentByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestingPersonnelInvitations_InvitationForDate",
                schema: "ApiDb",
                table: "TestingPersonnelInvitation",
                newName: "IX_TestingPersonnelInvitation_InvitationForDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestingPersonnelWorkingArea",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingArea",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestingPersonnelStatus",
                schema: "ApiDb",
                table: "TestingPersonnelStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestingPersonnel",
                schema: "ApiDb",
                table: "TestingPersonnel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestingPersonnelInvitation",
                schema: "ApiDb",
                table: "TestingPersonnelInvitation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestingPersonnel_TestingPersonnelStatus_StatusId",
                schema: "ApiDb",
                table: "TestingPersonnel",
                column: "StatusId",
                principalSchema: "ApiDb",
                principalTable: "TestingPersonnelStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestingPersonnelInvitation_Users_SentByUserId",
                schema: "ApiDb",
                table: "TestingPersonnelInvitation",
                column: "SentByUserId",
                principalSchema: "ApiDb",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestingPersonnelWorkingArea_TestingPersonnel_TestingPersonn~",
                schema: "ApiDb",
                table: "TestingPersonnelWorkingArea",
                column: "TestingPersonnelId",
                principalSchema: "ApiDb",
                principalTable: "TestingPersonnel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
