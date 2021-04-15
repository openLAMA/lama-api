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
    public partial class SeedSupportPersonForNewOrgType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "Users",
                columns: new[] { "Id", "Email", "LamaCompanyId", "LandLineNumber", "Name", "OrganizationId", "PhoneNumber" },
                values: new object[] { new Guid("f54d712f-94d1-447f-becb-62df0d72b216"), "gnHWLPNRW2ZdXA2qHX2XAD4vx1slAqetnp1KV1yV6ac=", new Guid("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"), "dwPhC2kEaekmMahSecP+Zw==", "5z5VQaw55Tw7a0KbuzkQmvDLXgKyF0hTsbC7/k6RI4w=", null, "dwPhC2kEaekmMahSecP+Zw==" });

            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "SupportPersonOrgTypeDefaultMappings",
                columns: new[] { "Id", "OrganizationTypeId", "UserId" },
                values: new object[] { new Guid("b902522e-c816-4a79-bcf4-9eb0896ab78a"), 99990, new Guid("f54d712f-94d1-447f-becb-62df0d72b216") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "SupportPersonOrgTypeDefaultMappings",
                keyColumn: "Id",
                keyValue: new Guid("b902522e-c816-4a79-bcf4-9eb0896ab78a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f54d712f-94d1-447f-becb-62df0d72b216"));
        }
    }
}
