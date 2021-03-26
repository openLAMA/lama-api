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
    public partial class SeedLamaCompanyUsersAndDefaultSupportPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "Users",
                columns: new[] { "Id", "Email", "LamaCompanyId", "Name", "OrganizationId", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f"), "dUmJTbaQV6uSRgNo6qSxdMMo7Vt2kqQ/GD7C8q6Spmo=", new Guid("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"), "dHJp3o7Hkp/HS/nmMIa4kA==", null, "vSRpqIgaDb+VbmBPGRPIWg==" },
                    { new Guid("917aea9f-89d1-43e9-95ad-43969a1f276c"), "NOApzKyRTRHuc8nvOH2APp8ij4KTJGxTQNQPGEpbu1k=", new Guid("dd015958-aa5d-4568-850a-557a3c73f336"), "xmSc5gwiON+AwwxsabEpig==", null, "vSRpqIgaDb+VbmBPGRPIWg==" },
                    { new Guid("4491038d-39a8-410a-97b0-60f7f604d5ac"), "FSR76pjB9FuzJcAyT0xu0Wcoe2pISxF64mK+mhI6ZiI=", new Guid("13585770-17b3-4a9e-bbd7-4dff9cc9a5e2"), "VKoWZeB+QezSZdJzg/G6ig==", null, "vSRpqIgaDb+VbmBPGRPIWg==" },
                    { new Guid("076466c2-1540-4a4d-98eb-12bea4a8c71b"), "XTBEsQ6hodiv2AxLjves1GHtrafYSmYGHwZYbCSMKYo=", new Guid("bae8c907-4851-40ae-99ee-fbdf8dc43c83"), "e5rRqXltNck37nfPTN/RAw==", null, "vSRpqIgaDb+VbmBPGRPIWg==" }
                });

            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "SupportPersonOrgTypeDefaultMappings",
                columns: new[] { "Id", "OrganizationTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("fc3498d0-af10-4a7d-882b-4cfcce073a4d"), 82000, new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f") },
                    { new Guid("d54768ec-27e4-4f2d-b081-7df9b9825aab"), 82001, new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f") },
                    { new Guid("b9b33e39-f133-43f0-b05c-59771164a092"), 82002, new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f") },
                    { new Guid("2604552a-fe6c-4570-93d0-8c9013a83a6a"), 82003, new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f") },
                    { new Guid("c3fa9ce0-03b5-4f90-be92-c222d6161ec7"), 82004, new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "SupportPersonOrgTypeDefaultMappings",
                keyColumn: "Id",
                keyValue: new Guid("2604552a-fe6c-4570-93d0-8c9013a83a6a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "SupportPersonOrgTypeDefaultMappings",
                keyColumn: "Id",
                keyValue: new Guid("b9b33e39-f133-43f0-b05c-59771164a092"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "SupportPersonOrgTypeDefaultMappings",
                keyColumn: "Id",
                keyValue: new Guid("c3fa9ce0-03b5-4f90-be92-c222d6161ec7"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "SupportPersonOrgTypeDefaultMappings",
                keyColumn: "Id",
                keyValue: new Guid("d54768ec-27e4-4f2d-b081-7df9b9825aab"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "SupportPersonOrgTypeDefaultMappings",
                keyColumn: "Id",
                keyValue: new Guid("fc3498d0-af10-4a7d-882b-4cfcce073a4d"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("076466c2-1540-4a4d-98eb-12bea4a8c71b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4491038d-39a8-410a-97b0-60f7f604d5ac"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("917aea9f-89d1-43e9-95ad-43969a1f276c"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f"));
        }
    }
}
