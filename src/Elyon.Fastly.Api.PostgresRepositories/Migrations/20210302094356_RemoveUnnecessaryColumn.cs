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
    public partial class RemoveUnnecessaryColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestingPersonnelConfirmations_TestingPersonnelInvitations_T~",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations");

            migrationBuilder.DropColumn(
                name: "InvitationId",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestingPersonnelInvitationId",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TestingPersonnelConfirmations_TestingPersonnelInvitations_T~",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations",
                column: "TestingPersonnelInvitationId",
                principalSchema: "ApiDb",
                principalTable: "TestingPersonnelInvitations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestingPersonnelConfirmations_TestingPersonnelInvitations_T~",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestingPersonnelInvitationId",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "InvitationId",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_TestingPersonnelConfirmations_TestingPersonnelInvitations_T~",
                schema: "ApiDb",
                table: "TestingPersonnelConfirmations",
                column: "TestingPersonnelInvitationId",
                principalSchema: "ApiDb",
                principalTable: "TestingPersonnelInvitations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
