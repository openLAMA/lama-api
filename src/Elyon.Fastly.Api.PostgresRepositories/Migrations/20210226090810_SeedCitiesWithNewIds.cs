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
    public partial class SeedCitiesWithNewIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("02f9ca33-252f-49bd-8f9d-74b8e64a60dd"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("055fc7ed-0866-4b62-8f36-495f36aee376"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("05dbc28f-b078-4b8b-95ac-a68a89ae081c"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("093b7fb1-6be2-4930-8322-657696f38f4a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0a6fcf8a-764d-458f-b984-93880a5b1d24"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0cbf3423-e49f-4d90-804a-ee9dac849b58"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("17bb259a-f67d-41c4-91a3-12438a0ec165"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1e2235fa-2eb5-4ef5-b5d7-40e39d5b33a3"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1f929496-6261-4f7b-bb69-b7632e6d6112"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2260f643-e9d1-4183-871e-3feded0e9baf"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("22d6dca6-68cf-45d7-9419-2b569aa669c1"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2f230a58-d1dc-4028-840a-754830b00230"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2f4f7532-95cf-4e76-a071-72395cb4ed54"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("30654687-75c8-46df-9927-2b3d8f05451e"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("307d47a0-c1d6-42da-b567-71e9928ecf7a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3a3c87df-a6ed-4f86-a878-6f8d1f9e6e2a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3de7302e-05ae-46e2-b2bc-2bc1186dbf9d"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3e541c57-d978-4699-8e9d-0d2dde1612fe"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4440a17b-46f0-4996-94da-c79745bab540"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4d271b36-075e-41e0-9be2-ec537defc982"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4e9a9038-5723-4aa5-85bf-3010441452df"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4ef4d901-1a2e-4d9a-bf99-bfb8933d822f"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4ff276d2-467b-448a-9940-206cf99481f6"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("52a9cfe2-badc-437b-b314-f37429bf93c4"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("55a2f047-2f11-4196-b831-e07893c79242"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("57d8dbdf-0c9e-4ef2-86a3-a2e44b042a4d"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("5af9c867-e61f-432b-b13a-f999327eac71"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("5afb5820-867d-4f37-be12-87457c0d3989"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("5dd03835-d798-458a-bf79-4be8c8e6e916"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6113f133-7f1d-4044-8871-23c1dabf6fef"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6408c97c-dc04-491b-af96-0c05927989e0"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("66b0488e-d5fd-438c-8317-aaa30c2b39ce"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("677d2118-6959-4839-bd79-4e7554c7f35c"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("696c6c43-355a-4a14-8c77-8d582af7665e"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6a4a21bd-4048-4963-b523-68cc576f3fd2"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6a96993f-8820-4b88-9ed8-da65ed35ecb9"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6b1fa92a-0c87-46ab-bbb5-c0ff959cba64"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6fe584e8-c04a-44e5-8880-10a31ca792c4"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("71d14d25-4828-4722-b7e3-23dcecbc4c55"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7684c2d6-522b-4ddc-8e48-b80c7c6aefab"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("80eefe05-8b09-4813-b958-b43515164e97"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("81676a54-cdba-4e94-b48b-4c3d11b986a3"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("862cd4f3-8777-41ab-84e0-d508397f81cd"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8744e152-e041-45c6-9445-b60a766509e8"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8ca22c69-4d8f-4a02-b9d1-5b8738d96544"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8df4e983-242c-42c3-acdb-f55d188f0182"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9038c099-f860-4015-8f8b-5262325ac239"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("93af29c4-f57a-46d8-95b8-51c047e8a9cc"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("979a54e7-f589-44df-8e76-630d464c0122"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("98639c2e-d449-4033-bd8b-8b797e6690c3"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("99161de4-2642-4cbc-bedd-68e196173f11"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9d22d728-a81b-4bb7-8308-0f8e4fd86882"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9d2d46f2-4141-4103-b18b-0089890e7066"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9d4b70cb-5053-4241-a429-817b883b7698"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9f048cfb-9181-4a71-be06-421b1b0217a1"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9f3d84f6-d034-462b-a876-07342b8eef6a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a6bc0eee-5bad-4903-b305-250500097454"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("aba7b68f-9cc5-4b7a-bf21-10ea575aa385"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b0bb4473-09d8-4778-9a18-7e8a5266bb89"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b2e0a3a4-150a-4c06-98e5-53b7a1e90250"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b791e3a5-8ecb-4717-8f26-5a96c5499c33"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bda65285-0cbd-4a50-9ea3-e5845ddfced4"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bfce7a8b-5a63-4390-bfd3-40851aa70432"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c53959f5-3965-4107-8397-61fa30f95078"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c63b1be1-e947-48da-b260-d81490b4f3df"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c76ee3a5-c352-41e1-a3b4-95e20a4dffe4"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("cc37cd77-5999-459a-9377-3db6ec4a7dd1"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0625eaf-4be1-4136-8672-44c4f3d5f213"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d2409d2f-f0c8-4dc5-b109-75ae812f3d0b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d2f9dbe7-6ec3-4d53-9c59-c86ed4179307"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d3ece47c-8897-4fa7-b01d-bd4e971a2fe7"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d6e6b8f6-784d-4ec0-b04b-469aff6916e1"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d9d8286b-568b-43c7-9fab-a85897496847"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e0da3c86-90fa-498c-9245-3d4aa727bf70"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e22995eb-d23b-486e-8ba5-09962d0fef86"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e7d67b51-edbc-4c06-9200-8c2c4aa6c2b5"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e8bcac88-70dd-4797-8b1f-ce9b5b920b30"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("eb851c7a-555e-4e51-b3b6-235e04fef60f"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ed784616-f93b-4e1e-8058-c0dab3c817ac"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("edea27d6-4a91-4349-bc57-109be40c7153"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ee3e36e0-f818-4cc8-9d9c-46a5aace9453"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f3dba01b-9b4c-4072-a550-33205980075a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f8c9aca1-b327-4cef-b824-09c2e3864a4a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f96b2284-aa83-4ea0-ade8-b5e4e43aa177"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fcfe0b86-96e4-4160-94e5-8a710c22f64a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fd792e5d-925c-407f-aaaf-57cd5e438216"));

            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "Cities",
                columns: new[] { "Id", "CantonId", "CountryId", "Name", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("6a16f6ec-1cfb-465e-be5d-9e96b477e43b"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Aesch (BL)", "4147" },
                    { new Guid("c7dc4df1-ab52-4cce-9f07-550380e3ce98"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Reigoldswil", "4418" },
                    { new Guid("ddd7e685-83eb-4cba-8d18-ee9dbff9f5b4"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Ramlinsburg", "4433" },
                    { new Guid("e43ff4d4-8e3b-4fc0-b365-235796920336"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Pratteln", "4133" },
                    { new Guid("6608b089-e8ea-4eef-b635-5f665ed372e9"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Pfeffingen", "4148" },
                    { new Guid("48ff9916-36b7-43e3-ac44-6093f0c9c27a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Ormalingen", "4466" },
                    { new Guid("1ca2c9b6-fa55-47a2-8e25-ed2f5fd412bb"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Oltingen", "4494" },
                    { new Guid("478bc21f-ceef-4141-81fd-f280dc3df1d3"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Oberwil (BL)", "4104" },
                    { new Guid("648a2908-c3e7-4c3c-a784-a8647f866683"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Oberdorf (BL)", "4436" },
                    { new Guid("13a82b53-84e1-4de4-b77b-912a268a2057"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Nusshof", "4453" },
                    { new Guid("a5b934b7-6406-4b6f-a598-290fb56f5eb8"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Niederdorf", "4435" },
                    { new Guid("2d693d94-caf5-44d8-b76c-ab1bec9be043"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Nenzlingen", "4224" },
                    { new Guid("2aebb383-6aee-422f-adff-8aefae14dcf4"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Muttenz", "4132" },
                    { new Guid("43d7590e-4847-4eec-b2cf-7b29d2fbfff8"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Münchenstein", "4142" },
                    { new Guid("a8e33de7-a1e6-40e6-9a08-6394a504f791"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Maisprach", "4464" },
                    { new Guid("714f5425-21f2-472f-a93d-9bfe1dbbdbd7"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Lupsingen", "4419" },
                    { new Guid("3e68fda1-cbf2-4f1a-8111-60af6514ae31"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Liestal", "4410" },
                    { new Guid("f4330e65-8900-4b6a-8331-6928eb5fab8b"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Liesberg", "4253" },
                    { new Guid("c6179b94-fb3e-49a9-9938-c3e56e24d528"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Reinach (BL)", "4153" },
                    { new Guid("46d9a4e0-6423-41ad-a86f-80124fa102f2"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Rickenbach (BL)", "4462" },
                    { new Guid("07951cf9-f975-428d-bf2e-63109d47f17b"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Roggenburg", "2814" },
                    { new Guid("847a9ce8-57b3-4e51-b95b-2fd18121775f"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Röschenz", "4244" },
                    { new Guid("c3652c23-fca4-4eff-93f8-a36cdfe291cd"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Ziefen", "4417" },
                    { new Guid("7e0843c2-69fe-43d9-bed6-0572810ec542"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Zeglingen", "4495" },
                    { new Guid("25160415-b80d-49f5-b631-c56d277a6154"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Wittinsburg", "4443" },
                    { new Guid("a1943e15-034f-4c77-a6e1-71e0afbe5d80"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Wintersingen", "4451" },
                    { new Guid("30e4d57e-d903-4d28-bae0-f9080933083b"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Wenslingen", "4493" },
                    { new Guid("da4610e7-b35c-4ab7-8167-86620532ad63"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Waldenburg", "4437" },
                    { new Guid("7a20fe1c-71bd-4dfd-a19f-815283246234"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Wahlen", "4246" },
                    { new Guid("dfd08754-980a-492b-a49f-575491b3e8cb"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Titterten", "4425" },
                    { new Guid("14cd1881-af07-4a77-b798-5dda33cc0275"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Liedertswil", "4436" },
                    { new Guid("13145ec6-27ba-4245-afba-f0ceb6f838e1"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Thürnen", "4441" },
                    { new Guid("cf55f917-f6fa-4aba-aa71-842c786bad90"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Tenniken", "4456" },
                    { new Guid("209ebc04-3a0e-43f4-af3a-bddffae8b0c5"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Tecknau", "4492" },
                    { new Guid("ab0f65e3-8f16-4882-9fd0-8e01876da1ac"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Sissach", "4450" },
                    { new Guid("8a480bba-19f8-49cd-81eb-b86525a4de44"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Seltisberg", "4411" },
                    { new Guid("386a3ff2-76f3-497d-ae63-b854c9d5d0e3"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Schönenbuch", "4124" },
                    { new Guid("a5b6119c-203b-4392-97c4-6bd4dd264d1c"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Rünenberg", "4497" },
                    { new Guid("c281d224-d18a-408f-8544-00dc54c7feed"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Rümlingen", "4444" },
                    { new Guid("54076e0c-7017-40fe-97b2-1a917feac78b"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Rothenfluh", "4467" },
                    { new Guid("578f65d9-b071-4b30-a595-eb4acaef5114"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Therwil", "4106" },
                    { new Guid("379f78b2-1f47-4b37-b22c-c0c2c291a5ed"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Lauwil", "4426" },
                    { new Guid("7214e98e-9774-4aab-aa5f-b8a6672aa41b"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Lausen", "4415" },
                    { new Guid("5aa9adbd-642b-443b-9e6c-b83af4246465"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Laufen", "4242" },
                    { new Guid("e596762b-9a0b-45d7-8b06-26daedd1aa13"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Burg im Leimental", "4117" },
                    { new Guid("7a7b9061-8378-4f6a-bb2a-b7f701187fe8"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Buckten", "4446" },
                    { new Guid("34863dcf-c956-4b92-bd92-decc00f3f166"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Bubendorf", "4416" },
                    { new Guid("3fdfc425-c0b4-44f1-948d-f474c354d1c0"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Brislach", "4225" },
                    { new Guid("bdb71127-3a86-445e-991e-7ecf54acdf66"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Bretzwil", "4207" },
                    { new Guid("f2c24d84-2d9b-4b07-8fe6-d34aafe5363a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Bottmingen", "4103" },
                    { new Guid("667f4f8a-3d7a-43d8-94fb-2c114284a897"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Böckten", "4461" },
                    { new Guid("3dfe5d4c-c02e-4c9a-b5e4-2a452ad63677"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Blauen", "4223" },
                    { new Guid("3341c73c-de40-4363-94fb-1f3c82f2eb18"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Buus", "4463" },
                    { new Guid("95ba6878-d6f9-41ff-9b1a-cda4d09a23d9"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Birsfelden", "4127" },
                    { new Guid("db2a8854-cf75-46f4-b590-703de1df0c4b"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Biel-Benken", "4105" },
                    { new Guid("ba1ee371-aea8-494e-938f-a7d1456d9a0f"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Bennwil", "4431" },
                    { new Guid("b0486622-3b7e-4c78-bc3b-5307830867f2"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Augst", "4302" },
                    { new Guid("8449ce80-e3ea-44ee-96fc-abf9af0309ff"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Arlesheim", "4144" },
                    { new Guid("58df162a-12b5-4afb-b612-e3b8e572b4c7"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Arisdorf", "4422" },
                    { new Guid("f30883a5-2ba4-4c20-b9f4-7dac2bfd6eb7"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Arboldswil", "4424" },
                    { new Guid("bcbefa58-8ad7-4d6a-bdbd-7d38850c8a9f"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Anwil", "4469" },
                    { new Guid("c8028390-a5b1-4441-8ead-79420b84c5f7"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Allschwil", "4123" },
                    { new Guid("16ea13eb-f74f-4785-a860-e28add9a2ee9"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Binningen", "4101" },
                    { new Guid("c41f5626-9caa-4f5b-a8fa-b485b59704c8"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Zunzgen", "4455" },
                    { new Guid("ab0c8028-0992-4fb2-8ee9-6e961036d224"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Diegten", "4457" },
                    { new Guid("0af9771f-8f3c-4ac9-8f9b-5529f128e57a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Dittingen", "4243" },
                    { new Guid("ca610793-6bf2-4f8d-ab82-82eb7b714444"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Läufelfingen", "4448" },
                    { new Guid("bf5ba1d0-740c-4689-af9f-a20e39427d1e"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Langenbruck", "4438" },
                    { new Guid("1094661b-acfa-4cef-bf49-b3b96da7bc05"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Lampenberg", "4432" },
                    { new Guid("440f3ca5-159b-4b2c-88b6-531ccd8daf23"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Kilchberg (BL)", "4496" },
                    { new Guid("f69372a3-0c4f-486d-b512-87aca63b71af"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Känerkinden", "4447" },
                    { new Guid("8c398866-d726-4cf6-b2e1-94241c38e26f"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Itingen", "4452" },
                    { new Guid("7f0f8a9a-b420-4e24-b2e2-ead08400d6fa"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Hölstein", "4434" },
                    { new Guid("5c255305-e61b-42db-b29b-67a75a316d30"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Hersberg", "4423" },
                    { new Guid("ee6109db-6206-4dff-aade-b1980986c8f1"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Diepflingen", "4442" },
                    { new Guid("6ec67058-b6bc-4db0-af0e-cadc56c30a61"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Hemmiken", "4465" },
                    { new Guid("cc2f6cec-a11a-4bd3-9a5d-85b655d1cccb"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Grellingen", "4203" },
                    { new Guid("f75ec56e-dfc6-431b-b0f3-afd114990fec"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Giebenach", "4304" },
                    { new Guid("0a19530c-a378-4220-b6f4-e1113d3ac8f7"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Gelterkinden", "4460" },
                    { new Guid("49de07a8-1a7a-40f6-bd2f-7b710adcb840"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Füllinsdorf", "4414" },
                    { new Guid("fd49eb6c-8eb2-4b54-a69e-26d68b3c94c2"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Frenkendorf", "4402" },
                    { new Guid("7c2d1d35-c0f6-4010-be73-a8eea7614b24"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Ettingen", "4107" },
                    { new Guid("bf558c66-a77f-496c-9ffa-1528b2bafd3a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Eptingen", "4458" },
                    { new Guid("b6958641-a535-4b0c-8c0b-3ac786751e46"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Duggingen", "4202" },
                    { new Guid("f0cd07ae-6076-464b-b9b5-5f450db117ae"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Häfelfingen", "4445" },
                    { new Guid("f1403454-f1df-49a1-98ed-48edff944d49"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Zwingen", "4222" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("07951cf9-f975-428d-bf2e-63109d47f17b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0a19530c-a378-4220-b6f4-e1113d3ac8f7"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0af9771f-8f3c-4ac9-8f9b-5529f128e57a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1094661b-acfa-4cef-bf49-b3b96da7bc05"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("13145ec6-27ba-4245-afba-f0ceb6f838e1"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("13a82b53-84e1-4de4-b77b-912a268a2057"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("14cd1881-af07-4a77-b798-5dda33cc0275"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("16ea13eb-f74f-4785-a860-e28add9a2ee9"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1ca2c9b6-fa55-47a2-8e25-ed2f5fd412bb"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("209ebc04-3a0e-43f4-af3a-bddffae8b0c5"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("25160415-b80d-49f5-b631-c56d277a6154"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2aebb383-6aee-422f-adff-8aefae14dcf4"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2d693d94-caf5-44d8-b76c-ab1bec9be043"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("30e4d57e-d903-4d28-bae0-f9080933083b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3341c73c-de40-4363-94fb-1f3c82f2eb18"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("34863dcf-c956-4b92-bd92-decc00f3f166"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("379f78b2-1f47-4b37-b22c-c0c2c291a5ed"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("386a3ff2-76f3-497d-ae63-b854c9d5d0e3"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3dfe5d4c-c02e-4c9a-b5e4-2a452ad63677"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3e68fda1-cbf2-4f1a-8111-60af6514ae31"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3fdfc425-c0b4-44f1-948d-f474c354d1c0"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("43d7590e-4847-4eec-b2cf-7b29d2fbfff8"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("440f3ca5-159b-4b2c-88b6-531ccd8daf23"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("46d9a4e0-6423-41ad-a86f-80124fa102f2"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("478bc21f-ceef-4141-81fd-f280dc3df1d3"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("48ff9916-36b7-43e3-ac44-6093f0c9c27a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("49de07a8-1a7a-40f6-bd2f-7b710adcb840"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("54076e0c-7017-40fe-97b2-1a917feac78b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("578f65d9-b071-4b30-a595-eb4acaef5114"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("58df162a-12b5-4afb-b612-e3b8e572b4c7"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("5aa9adbd-642b-443b-9e6c-b83af4246465"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("5c255305-e61b-42db-b29b-67a75a316d30"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("648a2908-c3e7-4c3c-a784-a8647f866683"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6608b089-e8ea-4eef-b635-5f665ed372e9"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("667f4f8a-3d7a-43d8-94fb-2c114284a897"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6a16f6ec-1cfb-465e-be5d-9e96b477e43b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6ec67058-b6bc-4db0-af0e-cadc56c30a61"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("714f5425-21f2-472f-a93d-9bfe1dbbdbd7"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7214e98e-9774-4aab-aa5f-b8a6672aa41b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7a20fe1c-71bd-4dfd-a19f-815283246234"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7a7b9061-8378-4f6a-bb2a-b7f701187fe8"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7c2d1d35-c0f6-4010-be73-a8eea7614b24"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7e0843c2-69fe-43d9-bed6-0572810ec542"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7f0f8a9a-b420-4e24-b2e2-ead08400d6fa"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8449ce80-e3ea-44ee-96fc-abf9af0309ff"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("847a9ce8-57b3-4e51-b95b-2fd18121775f"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8a480bba-19f8-49cd-81eb-b86525a4de44"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8c398866-d726-4cf6-b2e1-94241c38e26f"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("95ba6878-d6f9-41ff-9b1a-cda4d09a23d9"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a1943e15-034f-4c77-a6e1-71e0afbe5d80"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a5b6119c-203b-4392-97c4-6bd4dd264d1c"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a5b934b7-6406-4b6f-a598-290fb56f5eb8"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a8e33de7-a1e6-40e6-9a08-6394a504f791"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ab0c8028-0992-4fb2-8ee9-6e961036d224"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ab0f65e3-8f16-4882-9fd0-8e01876da1ac"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b0486622-3b7e-4c78-bc3b-5307830867f2"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b6958641-a535-4b0c-8c0b-3ac786751e46"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ba1ee371-aea8-494e-938f-a7d1456d9a0f"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bcbefa58-8ad7-4d6a-bdbd-7d38850c8a9f"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bdb71127-3a86-445e-991e-7ecf54acdf66"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bf558c66-a77f-496c-9ffa-1528b2bafd3a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bf5ba1d0-740c-4689-af9f-a20e39427d1e"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c281d224-d18a-408f-8544-00dc54c7feed"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c3652c23-fca4-4eff-93f8-a36cdfe291cd"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c41f5626-9caa-4f5b-a8fa-b485b59704c8"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c6179b94-fb3e-49a9-9938-c3e56e24d528"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c7dc4df1-ab52-4cce-9f07-550380e3ce98"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c8028390-a5b1-4441-8ead-79420b84c5f7"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ca610793-6bf2-4f8d-ab82-82eb7b714444"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("cc2f6cec-a11a-4bd3-9a5d-85b655d1cccb"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("cf55f917-f6fa-4aba-aa71-842c786bad90"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("da4610e7-b35c-4ab7-8167-86620532ad63"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("db2a8854-cf75-46f4-b590-703de1df0c4b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ddd7e685-83eb-4cba-8d18-ee9dbff9f5b4"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("dfd08754-980a-492b-a49f-575491b3e8cb"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e43ff4d4-8e3b-4fc0-b365-235796920336"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e596762b-9a0b-45d7-8b06-26daedd1aa13"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ee6109db-6206-4dff-aade-b1980986c8f1"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f0cd07ae-6076-464b-b9b5-5f450db117ae"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f1403454-f1df-49a1-98ed-48edff944d49"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f2c24d84-2d9b-4b07-8fe6-d34aafe5363a"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f30883a5-2ba4-4c20-b9f4-7dac2bfd6eb7"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f4330e65-8900-4b6a-8331-6928eb5fab8b"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f69372a3-0c4f-486d-b512-87aca63b71af"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f75ec56e-dfc6-431b-b0f3-afd114990fec"));

            migrationBuilder.DeleteData(
                schema: "ApiDb",
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fd49eb6c-8eb2-4b54-a69e-26d68b3c94c2"));

            migrationBuilder.InsertData(
                schema: "ApiDb",
                table: "Cities",
                columns: new[] { "Id", "CantonId", "CountryId", "Name", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("5afb5820-867d-4f37-be12-87457c0d3989"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Aesch (BL)", "4147" },
                    { new Guid("a6bc0eee-5bad-4903-b305-250500097454"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Reigoldswil", "4418" },
                    { new Guid("2f4f7532-95cf-4e76-a071-72395cb4ed54"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Ramlinsburg", "4433" },
                    { new Guid("6a4a21bd-4048-4963-b523-68cc576f3fd2"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Pratteln", "4133" },
                    { new Guid("c63b1be1-e947-48da-b260-d81490b4f3df"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Pfeffingen", "4148" },
                    { new Guid("8ca22c69-4d8f-4a02-b9d1-5b8738d96544"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Ormalingen", "4466" },
                    { new Guid("30654687-75c8-46df-9927-2b3d8f05451e"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Oltingen", "4494" },
                    { new Guid("b791e3a5-8ecb-4717-8f26-5a96c5499c33"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Oberwil (BL)", "4104" },
                    { new Guid("e22995eb-d23b-486e-8ba5-09962d0fef86"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Oberdorf (BL)", "4436" },
                    { new Guid("3e541c57-d978-4699-8e9d-0d2dde1612fe"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Nusshof", "4453" },
                    { new Guid("ed784616-f93b-4e1e-8058-c0dab3c817ac"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Niederdorf", "4435" },
                    { new Guid("99161de4-2642-4cbc-bedd-68e196173f11"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Nenzlingen", "4224" },
                    { new Guid("1e2235fa-2eb5-4ef5-b5d7-40e39d5b33a3"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Muttenz", "4132" },
                    { new Guid("80eefe05-8b09-4813-b958-b43515164e97"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Münchenstein", "4142" },
                    { new Guid("fcfe0b86-96e4-4160-94e5-8a710c22f64a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Maisprach", "4464" },
                    { new Guid("696c6c43-355a-4a14-8c77-8d582af7665e"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Lupsingen", "4419" },
                    { new Guid("b0bb4473-09d8-4778-9a18-7e8a5266bb89"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Liestal", "4410" },
                    { new Guid("4ef4d901-1a2e-4d9a-bf99-bfb8933d822f"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Liesberg", "4253" },
                    { new Guid("c53959f5-3965-4107-8397-61fa30f95078"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Reinach (BL)", "4153" },
                    { new Guid("17bb259a-f67d-41c4-91a3-12438a0ec165"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Rickenbach (BL)", "4462" },
                    { new Guid("d3ece47c-8897-4fa7-b01d-bd4e971a2fe7"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Roggenburg", "2814" },
                    { new Guid("52a9cfe2-badc-437b-b314-f37429bf93c4"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Röschenz", "4244" },
                    { new Guid("5af9c867-e61f-432b-b13a-f999327eac71"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Ziefen", "4417" },
                    { new Guid("6fe584e8-c04a-44e5-8880-10a31ca792c4"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Zeglingen", "4495" },
                    { new Guid("f96b2284-aa83-4ea0-ade8-b5e4e43aa177"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Wittinsburg", "4443" },
                    { new Guid("d6e6b8f6-784d-4ec0-b04b-469aff6916e1"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Wintersingen", "4451" },
                    { new Guid("ee3e36e0-f818-4cc8-9d9c-46a5aace9453"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Wenslingen", "4493" },
                    { new Guid("677d2118-6959-4839-bd79-4e7554c7f35c"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Waldenburg", "4437" },
                    { new Guid("81676a54-cdba-4e94-b48b-4c3d11b986a3"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Wahlen", "4246" },
                    { new Guid("9d2d46f2-4141-4103-b18b-0089890e7066"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Titterten", "4425" },
                    { new Guid("98639c2e-d449-4033-bd8b-8b797e6690c3"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Liedertswil", "4436" },
                    { new Guid("aba7b68f-9cc5-4b7a-bf21-10ea575aa385"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Thürnen", "4441" },
                    { new Guid("9f3d84f6-d034-462b-a876-07342b8eef6a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Tenniken", "4456" },
                    { new Guid("4440a17b-46f0-4996-94da-c79745bab540"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Tecknau", "4492" },
                    { new Guid("2260f643-e9d1-4183-871e-3feded0e9baf"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Sissach", "4450" },
                    { new Guid("6b1fa92a-0c87-46ab-bbb5-c0ff959cba64"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Seltisberg", "4411" },
                    { new Guid("bfce7a8b-5a63-4390-bfd3-40851aa70432"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Schönenbuch", "4124" },
                    { new Guid("71d14d25-4828-4722-b7e3-23dcecbc4c55"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Rünenberg", "4497" },
                    { new Guid("055fc7ed-0866-4b62-8f36-495f36aee376"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Rümlingen", "4444" },
                    { new Guid("7684c2d6-522b-4ddc-8e48-b80c7c6aefab"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Rothenfluh", "4467" },
                    { new Guid("6a96993f-8820-4b88-9ed8-da65ed35ecb9"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Therwil", "4106" },
                    { new Guid("02f9ca33-252f-49bd-8f9d-74b8e64a60dd"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Lauwil", "4426" },
                    { new Guid("e7d67b51-edbc-4c06-9200-8c2c4aa6c2b5"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Lausen", "4415" },
                    { new Guid("05dbc28f-b078-4b8b-95ac-a68a89ae081c"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Laufen", "4242" },
                    { new Guid("4e9a9038-5723-4aa5-85bf-3010441452df"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Burg im Leimental", "4117" },
                    { new Guid("bda65285-0cbd-4a50-9ea3-e5845ddfced4"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Buckten", "4446" },
                    { new Guid("f8c9aca1-b327-4cef-b824-09c2e3864a4a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Bubendorf", "4416" },
                    { new Guid("55a2f047-2f11-4196-b831-e07893c79242"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Brislach", "4225" },
                    { new Guid("eb851c7a-555e-4e51-b3b6-235e04fef60f"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Bretzwil", "4207" },
                    { new Guid("307d47a0-c1d6-42da-b567-71e9928ecf7a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Bottmingen", "4103" },
                    { new Guid("2f230a58-d1dc-4028-840a-754830b00230"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Böckten", "4461" },
                    { new Guid("0a6fcf8a-764d-458f-b984-93880a5b1d24"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Blauen", "4223" },
                    { new Guid("4ff276d2-467b-448a-9940-206cf99481f6"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Buus", "4463" },
                    { new Guid("66b0488e-d5fd-438c-8317-aaa30c2b39ce"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Birsfelden", "4127" },
                    { new Guid("fd792e5d-925c-407f-aaaf-57cd5e438216"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Biel-Benken", "4105" },
                    { new Guid("edea27d6-4a91-4349-bc57-109be40c7153"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Bennwil", "4431" },
                    { new Guid("9d22d728-a81b-4bb7-8308-0f8e4fd86882"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Augst", "4302" },
                    { new Guid("d2409d2f-f0c8-4dc5-b109-75ae812f3d0b"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Arlesheim", "4144" },
                    { new Guid("c76ee3a5-c352-41e1-a3b4-95e20a4dffe4"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Arisdorf", "4422" },
                    { new Guid("6113f133-7f1d-4044-8871-23c1dabf6fef"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Arboldswil", "4424" },
                    { new Guid("9f048cfb-9181-4a71-be06-421b1b0217a1"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Anwil", "4469" },
                    { new Guid("e8bcac88-70dd-4797-8b1f-ce9b5b920b30"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Allschwil", "4123" },
                    { new Guid("8744e152-e041-45c6-9445-b60a766509e8"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Binningen", "4101" },
                    { new Guid("979a54e7-f589-44df-8e76-630d464c0122"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Zunzgen", "4455" },
                    { new Guid("093b7fb1-6be2-4930-8322-657696f38f4a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Diegten", "4457" },
                    { new Guid("57d8dbdf-0c9e-4ef2-86a3-a2e44b042a4d"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Dittingen", "4243" },
                    { new Guid("3de7302e-05ae-46e2-b2bc-2bc1186dbf9d"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Läufelfingen", "4448" },
                    { new Guid("8df4e983-242c-42c3-acdb-f55d188f0182"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Langenbruck", "4438" },
                    { new Guid("22d6dca6-68cf-45d7-9419-2b569aa669c1"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Lampenberg", "4432" },
                    { new Guid("b2e0a3a4-150a-4c06-98e5-53b7a1e90250"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Kilchberg (BL)", "4496" },
                    { new Guid("f3dba01b-9b4c-4072-a550-33205980075a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Känerkinden", "4447" },
                    { new Guid("3a3c87df-a6ed-4f86-a878-6f8d1f9e6e2a"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Itingen", "4452" },
                    { new Guid("cc37cd77-5999-459a-9377-3db6ec4a7dd1"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Hölstein", "4434" },
                    { new Guid("93af29c4-f57a-46d8-95b8-51c047e8a9cc"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Hersberg", "4423" },
                    { new Guid("d2f9dbe7-6ec3-4d53-9c59-c86ed4179307"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Diepflingen", "4442" },
                    { new Guid("1f929496-6261-4f7b-bb69-b7632e6d6112"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Hemmiken", "4465" },
                    { new Guid("4d271b36-075e-41e0-9be2-ec537defc982"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Grellingen", "4203" },
                    { new Guid("e0da3c86-90fa-498c-9245-3d4aa727bf70"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Giebenach", "4304" },
                    { new Guid("9038c099-f860-4015-8f8b-5262325ac239"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Gelterkinden", "4460" },
                    { new Guid("d9d8286b-568b-43c7-9fab-a85897496847"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Füllinsdorf", "4414" },
                    { new Guid("0cbf3423-e49f-4d90-804a-ee9dac849b58"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Frenkendorf", "4402" },
                    { new Guid("d0625eaf-4be1-4136-8672-44c4f3d5f213"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Ettingen", "4107" },
                    { new Guid("5dd03835-d798-458a-bf79-4be8c8e6e916"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Eptingen", "4458" },
                    { new Guid("9d4b70cb-5053-4241-a429-817b883b7698"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Duggingen", "4202" },
                    { new Guid("862cd4f3-8777-41ab-84e0-d508397f81cd"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Häfelfingen", "4445" },
                    { new Guid("6408c97c-dc04-491b-af96-0c05927989e0"), new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"), null, "Zwingen", "4222" }
                });
        }
    }
}
