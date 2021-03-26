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
    public partial class AddSubOrganizationsNavProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubOrganization_Organizations_OrganizationId",
                schema: "ApiDb",
                table: "SubOrganization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubOrganization",
                schema: "ApiDb",
                table: "SubOrganization");

            migrationBuilder.RenameTable(
                name: "SubOrganization",
                schema: "ApiDb",
                newName: "SubOrganizations",
                newSchema: "ApiDb");

            migrationBuilder.RenameIndex(
                name: "IX_SubOrganization_OrganizationId",
                schema: "ApiDb",
                table: "SubOrganizations",
                newName: "IX_SubOrganizations_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubOrganizations",
                schema: "ApiDb",
                table: "SubOrganizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubOrganizations_Organizations_OrganizationId",
                schema: "ApiDb",
                table: "SubOrganizations",
                column: "OrganizationId",
                principalSchema: "ApiDb",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubOrganizations_Organizations_OrganizationId",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubOrganizations",
                schema: "ApiDb",
                table: "SubOrganizations");

            migrationBuilder.RenameTable(
                name: "SubOrganizations",
                schema: "ApiDb",
                newName: "SubOrganization",
                newSchema: "ApiDb");

            migrationBuilder.RenameIndex(
                name: "IX_SubOrganizations_OrganizationId",
                schema: "ApiDb",
                table: "SubOrganization",
                newName: "IX_SubOrganization_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubOrganization",
                schema: "ApiDb",
                table: "SubOrganization",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubOrganization_Organizations_OrganizationId",
                schema: "ApiDb",
                table: "SubOrganization",
                column: "OrganizationId",
                principalSchema: "ApiDb",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
