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
    public partial class AddLamaCompaniesAndChangesToConfToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                schema: "ApiDb",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConfirmationType",
                schema: "ApiDb",
                table: "UserConfirmationTokens");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                schema: "ApiDb",
                table: "UserConfirmationTokens",
                newName: "ExpirationTimeStamp");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "ApiDb",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "LamaCompanyId",
                schema: "ApiDb",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ApiDb",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "ApiDb",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "ApiDb",
                table: "UserConfirmationTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "LamaCompanies",
                schema: "ApiDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    RoleType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LamaCompanies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LamaCompanyId",
                schema: "ApiDb",
                table: "Users",
                column: "LamaCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LamaCompanies_LamaCompanyId",
                schema: "ApiDb",
                table: "Users",
                column: "LamaCompanyId",
                principalSchema: "ApiDb",
                principalTable: "LamaCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_LamaCompanies_LamaCompanyId",
                schema: "ApiDb",
                table: "Users");

            migrationBuilder.DropTable(
                name: "LamaCompanies",
                schema: "ApiDb");

            migrationBuilder.DropIndex(
                name: "IX_Users_LamaCompanyId",
                schema: "ApiDb",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LamaCompanyId",
                schema: "ApiDb",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ApiDb",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "ApiDb",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "ApiDb",
                table: "UserConfirmationTokens");

            migrationBuilder.RenameColumn(
                name: "ExpirationTimeStamp",
                schema: "ApiDb",
                table: "UserConfirmationTokens",
                newName: "ExpirationDate");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "ApiDb",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                schema: "ApiDb",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConfirmationType",
                schema: "ApiDb",
                table: "UserConfirmationTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
