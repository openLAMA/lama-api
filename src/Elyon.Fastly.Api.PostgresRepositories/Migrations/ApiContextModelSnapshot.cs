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
using Elyon.Fastly.Api.PostgresRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ApiDb")
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Canton", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cantons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            CountryId = new Guid("506fcee9-0ae2-49fa-a0ff-f9d3ac6d68bb"),
                            Name = "Basel",
                            ShortName = "BL"
                        });
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CantonId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.HasIndex("CantonId");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6a16f6ec-1cfb-465e-be5d-9e96b477e43b"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Aesch (BL)",
                            ZipCode = "4147"
                        },
                        new
                        {
                            Id = new Guid("c8028390-a5b1-4441-8ead-79420b84c5f7"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Allschwil",
                            ZipCode = "4123"
                        },
                        new
                        {
                            Id = new Guid("bcbefa58-8ad7-4d6a-bdbd-7d38850c8a9f"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Anwil",
                            ZipCode = "4469"
                        },
                        new
                        {
                            Id = new Guid("f30883a5-2ba4-4c20-b9f4-7dac2bfd6eb7"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Arboldswil",
                            ZipCode = "4424"
                        },
                        new
                        {
                            Id = new Guid("58df162a-12b5-4afb-b612-e3b8e572b4c7"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Arisdorf",
                            ZipCode = "4422"
                        },
                        new
                        {
                            Id = new Guid("8449ce80-e3ea-44ee-96fc-abf9af0309ff"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Arlesheim",
                            ZipCode = "4144"
                        },
                        new
                        {
                            Id = new Guid("b0486622-3b7e-4c78-bc3b-5307830867f2"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Augst",
                            ZipCode = "4302"
                        },
                        new
                        {
                            Id = new Guid("ba1ee371-aea8-494e-938f-a7d1456d9a0f"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Bennwil",
                            ZipCode = "4431"
                        },
                        new
                        {
                            Id = new Guid("db2a8854-cf75-46f4-b590-703de1df0c4b"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Biel-Benken",
                            ZipCode = "4105"
                        },
                        new
                        {
                            Id = new Guid("16ea13eb-f74f-4785-a860-e28add9a2ee9"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Binningen",
                            ZipCode = "4102"
                        },
                        new
                        {
                            Id = new Guid("95ba6878-d6f9-41ff-9b1a-cda4d09a23d9"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Birsfelden",
                            ZipCode = "4127"
                        },
                        new
                        {
                            Id = new Guid("3dfe5d4c-c02e-4c9a-b5e4-2a452ad63677"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Blauen",
                            ZipCode = "4223"
                        },
                        new
                        {
                            Id = new Guid("667f4f8a-3d7a-43d8-94fb-2c114284a897"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Böckten",
                            ZipCode = "4461"
                        },
                        new
                        {
                            Id = new Guid("f2c24d84-2d9b-4b07-8fe6-d34aafe5363a"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Bottmingen",
                            ZipCode = "4103"
                        },
                        new
                        {
                            Id = new Guid("bdb71127-3a86-445e-991e-7ecf54acdf66"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Bretzwil",
                            ZipCode = "4207"
                        },
                        new
                        {
                            Id = new Guid("3fdfc425-c0b4-44f1-948d-f474c354d1c0"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Brislach",
                            ZipCode = "4225"
                        },
                        new
                        {
                            Id = new Guid("34863dcf-c956-4b92-bd92-decc00f3f166"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Bubendorf",
                            ZipCode = "4416"
                        },
                        new
                        {
                            Id = new Guid("7a7b9061-8378-4f6a-bb2a-b7f701187fe8"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Buckten",
                            ZipCode = "4446"
                        },
                        new
                        {
                            Id = new Guid("e596762b-9a0b-45d7-8b06-26daedd1aa13"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Burg im Leimental",
                            ZipCode = "4117"
                        },
                        new
                        {
                            Id = new Guid("3341c73c-de40-4363-94fb-1f3c82f2eb18"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Buus",
                            ZipCode = "4463"
                        },
                        new
                        {
                            Id = new Guid("ab0c8028-0992-4fb2-8ee9-6e961036d224"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Diegten",
                            ZipCode = "4457"
                        },
                        new
                        {
                            Id = new Guid("ee6109db-6206-4dff-aade-b1980986c8f1"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Diepflingen",
                            ZipCode = "4442"
                        },
                        new
                        {
                            Id = new Guid("0af9771f-8f3c-4ac9-8f9b-5529f128e57a"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Dittingen",
                            ZipCode = "4243"
                        },
                        new
                        {
                            Id = new Guid("b6958641-a535-4b0c-8c0b-3ac786751e46"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Duggingen",
                            ZipCode = "4202"
                        },
                        new
                        {
                            Id = new Guid("bf558c66-a77f-496c-9ffa-1528b2bafd3a"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Eptingen",
                            ZipCode = "4458"
                        },
                        new
                        {
                            Id = new Guid("7c2d1d35-c0f6-4010-be73-a8eea7614b24"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Ettingen",
                            ZipCode = "4107"
                        },
                        new
                        {
                            Id = new Guid("fd49eb6c-8eb2-4b54-a69e-26d68b3c94c2"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Frenkendorf",
                            ZipCode = "4402"
                        },
                        new
                        {
                            Id = new Guid("49de07a8-1a7a-40f6-bd2f-7b710adcb840"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Füllinsdorf",
                            ZipCode = "4414"
                        },
                        new
                        {
                            Id = new Guid("0a19530c-a378-4220-b6f4-e1113d3ac8f7"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Gelterkinden",
                            ZipCode = "4460"
                        },
                        new
                        {
                            Id = new Guid("f75ec56e-dfc6-431b-b0f3-afd114990fec"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Giebenach",
                            ZipCode = "4304"
                        },
                        new
                        {
                            Id = new Guid("cc2f6cec-a11a-4bd3-9a5d-85b655d1cccb"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Grellingen",
                            ZipCode = "4203"
                        },
                        new
                        {
                            Id = new Guid("f0cd07ae-6076-464b-b9b5-5f450db117ae"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Häfelfingen",
                            ZipCode = "4445"
                        },
                        new
                        {
                            Id = new Guid("6ec67058-b6bc-4db0-af0e-cadc56c30a61"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Hemmiken",
                            ZipCode = "4465"
                        },
                        new
                        {
                            Id = new Guid("5c255305-e61b-42db-b29b-67a75a316d30"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Hersberg",
                            ZipCode = "4423"
                        },
                        new
                        {
                            Id = new Guid("7f0f8a9a-b420-4e24-b2e2-ead08400d6fa"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Hölstein",
                            ZipCode = "4434"
                        },
                        new
                        {
                            Id = new Guid("8c398866-d726-4cf6-b2e1-94241c38e26f"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Itingen",
                            ZipCode = "4452"
                        },
                        new
                        {
                            Id = new Guid("f69372a3-0c4f-486d-b512-87aca63b71af"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Känerkinden",
                            ZipCode = "4447"
                        },
                        new
                        {
                            Id = new Guid("440f3ca5-159b-4b2c-88b6-531ccd8daf23"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Kilchberg (BL)",
                            ZipCode = "4496"
                        },
                        new
                        {
                            Id = new Guid("1094661b-acfa-4cef-bf49-b3b96da7bc05"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Lampenberg",
                            ZipCode = "4432"
                        },
                        new
                        {
                            Id = new Guid("bf5ba1d0-740c-4689-af9f-a20e39427d1e"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Langenbruck",
                            ZipCode = "4438"
                        },
                        new
                        {
                            Id = new Guid("ca610793-6bf2-4f8d-ab82-82eb7b714444"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Läufelfingen",
                            ZipCode = "4448"
                        },
                        new
                        {
                            Id = new Guid("5aa9adbd-642b-443b-9e6c-b83af4246465"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Laufen",
                            ZipCode = "4242"
                        },
                        new
                        {
                            Id = new Guid("7214e98e-9774-4aab-aa5f-b8a6672aa41b"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Lausen",
                            ZipCode = "4415"
                        },
                        new
                        {
                            Id = new Guid("379f78b2-1f47-4b37-b22c-c0c2c291a5ed"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Lauwil",
                            ZipCode = "4426"
                        },
                        new
                        {
                            Id = new Guid("14cd1881-af07-4a77-b798-5dda33cc0275"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Liedertswil",
                            ZipCode = "4436"
                        },
                        new
                        {
                            Id = new Guid("f4330e65-8900-4b6a-8331-6928eb5fab8b"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Liesberg",
                            ZipCode = "4253"
                        },
                        new
                        {
                            Id = new Guid("3e68fda1-cbf2-4f1a-8111-60af6514ae31"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Liestal",
                            ZipCode = "4410"
                        },
                        new
                        {
                            Id = new Guid("714f5425-21f2-472f-a93d-9bfe1dbbdbd7"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Lupsingen",
                            ZipCode = "4419"
                        },
                        new
                        {
                            Id = new Guid("a8e33de7-a1e6-40e6-9a08-6394a504f791"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Maisprach",
                            ZipCode = "4464"
                        },
                        new
                        {
                            Id = new Guid("43d7590e-4847-4eec-b2cf-7b29d2fbfff8"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Münchenstein",
                            ZipCode = "4142"
                        },
                        new
                        {
                            Id = new Guid("2aebb383-6aee-422f-adff-8aefae14dcf4"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Muttenz",
                            ZipCode = "4132"
                        },
                        new
                        {
                            Id = new Guid("2d693d94-caf5-44d8-b76c-ab1bec9be043"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Nenzlingen",
                            ZipCode = "4224"
                        },
                        new
                        {
                            Id = new Guid("a5b934b7-6406-4b6f-a598-290fb56f5eb8"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Niederdorf",
                            ZipCode = "4435"
                        },
                        new
                        {
                            Id = new Guid("13a82b53-84e1-4de4-b77b-912a268a2057"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Nusshof",
                            ZipCode = "4453"
                        },
                        new
                        {
                            Id = new Guid("648a2908-c3e7-4c3c-a784-a8647f866683"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Oberdorf (BL)",
                            ZipCode = "4436"
                        },
                        new
                        {
                            Id = new Guid("478bc21f-ceef-4141-81fd-f280dc3df1d3"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Oberwil (BL)",
                            ZipCode = "4104"
                        },
                        new
                        {
                            Id = new Guid("1ca2c9b6-fa55-47a2-8e25-ed2f5fd412bb"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Oltingen",
                            ZipCode = "4494"
                        },
                        new
                        {
                            Id = new Guid("48ff9916-36b7-43e3-ac44-6093f0c9c27a"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Ormalingen",
                            ZipCode = "4466"
                        },
                        new
                        {
                            Id = new Guid("6608b089-e8ea-4eef-b635-5f665ed372e9"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Pfeffingen",
                            ZipCode = "4148"
                        },
                        new
                        {
                            Id = new Guid("e43ff4d4-8e3b-4fc0-b365-235796920336"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Pratteln",
                            ZipCode = "4133"
                        },
                        new
                        {
                            Id = new Guid("ddd7e685-83eb-4cba-8d18-ee9dbff9f5b4"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Ramlinsburg",
                            ZipCode = "4433"
                        },
                        new
                        {
                            Id = new Guid("c7dc4df1-ab52-4cce-9f07-550380e3ce98"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Reigoldswil",
                            ZipCode = "4418"
                        },
                        new
                        {
                            Id = new Guid("c6179b94-fb3e-49a9-9938-c3e56e24d528"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Reinach (BL)",
                            ZipCode = "4153"
                        },
                        new
                        {
                            Id = new Guid("46d9a4e0-6423-41ad-a86f-80124fa102f2"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Rickenbach (BL)",
                            ZipCode = "4462"
                        },
                        new
                        {
                            Id = new Guid("07951cf9-f975-428d-bf2e-63109d47f17b"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Roggenburg",
                            ZipCode = "2814"
                        },
                        new
                        {
                            Id = new Guid("847a9ce8-57b3-4e51-b95b-2fd18121775f"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Röschenz",
                            ZipCode = "4244"
                        },
                        new
                        {
                            Id = new Guid("54076e0c-7017-40fe-97b2-1a917feac78b"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Rothenfluh",
                            ZipCode = "4467"
                        },
                        new
                        {
                            Id = new Guid("c281d224-d18a-408f-8544-00dc54c7feed"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Rümlingen",
                            ZipCode = "4444"
                        },
                        new
                        {
                            Id = new Guid("a5b6119c-203b-4392-97c4-6bd4dd264d1c"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Rünenberg",
                            ZipCode = "4497"
                        },
                        new
                        {
                            Id = new Guid("386a3ff2-76f3-497d-ae63-b854c9d5d0e3"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Schönenbuch",
                            ZipCode = "4124"
                        },
                        new
                        {
                            Id = new Guid("8a480bba-19f8-49cd-81eb-b86525a4de44"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Seltisberg",
                            ZipCode = "4411"
                        },
                        new
                        {
                            Id = new Guid("ab0f65e3-8f16-4882-9fd0-8e01876da1ac"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Sissach",
                            ZipCode = "4450"
                        },
                        new
                        {
                            Id = new Guid("209ebc04-3a0e-43f4-af3a-bddffae8b0c5"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Tecknau",
                            ZipCode = "4492"
                        },
                        new
                        {
                            Id = new Guid("cf55f917-f6fa-4aba-aa71-842c786bad90"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Tenniken",
                            ZipCode = "4456"
                        },
                        new
                        {
                            Id = new Guid("578f65d9-b071-4b30-a595-eb4acaef5114"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Therwil",
                            ZipCode = "4106"
                        },
                        new
                        {
                            Id = new Guid("13145ec6-27ba-4245-afba-f0ceb6f838e1"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Thürnen",
                            ZipCode = "4441"
                        },
                        new
                        {
                            Id = new Guid("dfd08754-980a-492b-a49f-575491b3e8cb"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Titterten",
                            ZipCode = "4425"
                        },
                        new
                        {
                            Id = new Guid("7a20fe1c-71bd-4dfd-a19f-815283246234"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Wahlen",
                            ZipCode = "4246"
                        },
                        new
                        {
                            Id = new Guid("da4610e7-b35c-4ab7-8167-86620532ad63"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Waldenburg",
                            ZipCode = "4437"
                        },
                        new
                        {
                            Id = new Guid("30e4d57e-d903-4d28-bae0-f9080933083b"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Wenslingen",
                            ZipCode = "4493"
                        },
                        new
                        {
                            Id = new Guid("a1943e15-034f-4c77-a6e1-71e0afbe5d80"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Wintersingen",
                            ZipCode = "4451"
                        },
                        new
                        {
                            Id = new Guid("25160415-b80d-49f5-b631-c56d277a6154"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Wittinsburg",
                            ZipCode = "4443"
                        },
                        new
                        {
                            Id = new Guid("7e0843c2-69fe-43d9-bed6-0572810ec542"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Zeglingen",
                            ZipCode = "4495"
                        },
                        new
                        {
                            Id = new Guid("c3652c23-fca4-4eff-93f8-a36cdfe291cd"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Ziefen",
                            ZipCode = "4417"
                        },
                        new
                        {
                            Id = new Guid("c41f5626-9caa-4f5b-a8fa-b485b59704c8"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Zunzgen",
                            ZipCode = "4455"
                        },
                        new
                        {
                            Id = new Guid("f1403454-f1df-49a1-98ed-48edff944d49"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Zwingen",
                            ZipCode = "4222"
                        },
                        new
                        {
                            Id = new Guid("b4bfb54d-aef6-4a9f-a720-4bd36404e06a"),
                            CantonId = new Guid("13f87683-8736-49e3-9a96-bceafb2d6846"),
                            Name = "Basel",
                            ZipCode = "4001"
                        });
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("506fcee9-0ae2-49fa-a0ff-f9d3ac6d68bb"),
                            Name = "Schweiz",
                            ShortName = "CH"
                        });
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.LamaCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("RoleType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("LamaCompanies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"),
                            Name = "University",
                            RoleType = 1
                        },
                        new
                        {
                            Id = new Guid("dd015958-aa5d-4568-850a-557a3c73f336"),
                            Name = "Laboratory",
                            RoleType = 2
                        },
                        new
                        {
                            Id = new Guid("13585770-17b3-4a9e-bbd7-4dff9cc9a5e2"),
                            Name = "Logistics",
                            RoleType = 3
                        },
                        new
                        {
                            Id = new Guid("bae8c907-4851-40ae-99ee-fbdf8dc43c83"),
                            Name = "State",
                            RoleType = 4
                        });
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("AdditionalTestTubes")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Area")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uuid");

                    b.Property<string>("County")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("EmployeesCount")
                        .HasColumnType("integer");

                    b.Property<int?>("EpaadId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ExclusionEndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ExclusionStartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FifthTestTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FirstTestTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FourthTestTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Manager")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("NaclLosing")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("NumberOfBags")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfPools")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfRakoBoxes")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfSamples")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("OnboardingTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OrganizationShortcutName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("OrganizationTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("PickupLocation")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int?>("PrioLogistic")
                        .HasColumnType("integer");

                    b.Property<int?>("RegisteredEmployees")
                        .HasColumnType("integer");

                    b.Property<int?>("SchoolType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("SecondTestTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int?>("StudentsCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("SupportPersonId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ThirdTestTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("TrainingTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Zip")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("OrganizationTypeId");

                    b.HasIndex("SupportPersonId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.OrganizationNote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatorName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("OrganizationNotes");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.OrganizationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrganizationTypes");

                    b.HasData(
                        new
                        {
                            Id = 82000,
                            Name = "Company"
                        },
                        new
                        {
                            Id = 82001,
                            Name = "Pharmacy"
                        },
                        new
                        {
                            Id = 82002,
                            Name = "School"
                        },
                        new
                        {
                            Id = 82003,
                            Name = "Nursing Home"
                        },
                        new
                        {
                            Id = 82004,
                            Name = "Hospital"
                        });
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.SubOrganization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("SubOrganizations");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.SupportPersonOrgTypeDefaultMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("OrganizationTypeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("SupportPersonOrgTypeDefaultMappings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fc3498d0-af10-4a7d-882b-4cfcce073a4d"),
                            OrganizationTypeId = 82000,
                            UserId = new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f")
                        },
                        new
                        {
                            Id = new Guid("d54768ec-27e4-4f2d-b081-7df9b9825aab"),
                            OrganizationTypeId = 82001,
                            UserId = new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f")
                        },
                        new
                        {
                            Id = new Guid("b9b33e39-f133-43f0-b05c-59771164a092"),
                            OrganizationTypeId = 82002,
                            UserId = new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f")
                        },
                        new
                        {
                            Id = new Guid("2604552a-fe6c-4570-93d0-8c9013a83a6a"),
                            OrganizationTypeId = 82003,
                            UserId = new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f")
                        },
                        new
                        {
                            Id = new Guid("c3fa9ce0-03b5-4f90-be92-c222d6161ec7"),
                            OrganizationTypeId = 82004,
                            UserId = new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f")
                        });
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Employeer")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("TestingPersonnels");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelConfirmation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AcceptedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ShiftNumber")
                        .HasColumnType("integer");

                    b.Property<Guid>("TestingPersonnelId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TestingPersonnelInvitationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestingPersonnelId");

                    b.HasIndex("TestingPersonnelInvitationId");

                    b.ToTable("TestingPersonnelConfirmations");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelInvitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("InvitationForDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("RequiredPersonnelCountShift1")
                        .HasColumnType("integer");

                    b.Property<int>("RequiredPersonnelCountShift2")
                        .HasColumnType("integer");

                    b.Property<Guid>("SentByUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InvitationForDate");

                    b.HasIndex("SentByUserId");

                    b.ToTable("TestingPersonnelInvitations");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelInvitationConfirmationToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TestingPersonnelId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TestingPersonnelInvitationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TestingPersonnelId");

                    b.HasIndex("TestingPersonnelInvitationId")
                        .HasDatabaseName("IX_TestingPersonnelInvitationConfirmationTokens_TestingPerson~1");

                    b.ToTable("TestingPersonnelInvitationConfirmationTokens");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("TestingPersonnelStatuses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c66bd94-5f3e-4aca-ab58-2b2b8c12d526"),
                            Name = "BSc"
                        },
                        new
                        {
                            Id = new Guid("d95e09c1-4b57-423c-a180-6c1c3011d959"),
                            Name = "MSc"
                        },
                        new
                        {
                            Id = new Guid("c67d1068-255a-4c89-b590-a4531154508a"),
                            Name = "MSc (Head)"
                        },
                        new
                        {
                            Id = new Guid("d58d11d7-740a-4acf-b380-21d4b88d4aec"),
                            Name = "BSc (Head)"
                        });
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelWorkingArea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Area")
                        .HasColumnType("integer");

                    b.Property<Guid>("TestingPersonnelId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestingPersonnelId");

                    b.ToTable("TestingPersonnelWorkingAreas");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("LamaCompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("LandLineNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.HasIndex("LamaCompanyId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7ef459ed-035d-48f8-adc0-ab225a316d7f"),
                            Email = "dUmJTbaQV6uSRgNo6qSxdMMo7Vt2kqQ/GD7C8q6Spmo=",
                            LamaCompanyId = new Guid("68e237f6-cdc9-4d91-99ae-c8b1842ed2ea"),
                            LandLineNumber = "SSEfX6+eGdQho09y+G6bag==",
                            Name = "dHJp3o7Hkp/HS/nmMIa4kA==",
                            PhoneNumber = "vSRpqIgaDb+VbmBPGRPIWg=="
                        },
                        new
                        {
                            Id = new Guid("917aea9f-89d1-43e9-95ad-43969a1f276c"),
                            Email = "NOApzKyRTRHuc8nvOH2APp8ij4KTJGxTQNQPGEpbu1k=",
                            LamaCompanyId = new Guid("dd015958-aa5d-4568-850a-557a3c73f336"),
                            LandLineNumber = "SSEfX6+eGdQho09y+G6bag==",
                            Name = "xmSc5gwiON+AwwxsabEpig==",
                            PhoneNumber = "vSRpqIgaDb+VbmBPGRPIWg==="
                        },
                        new
                        {
                            Id = new Guid("4491038d-39a8-410a-97b0-60f7f604d5ac"),
                            Email = "FSR76pjB9FuzJcAyT0xu0Wcoe2pISxF64mK+mhI6ZiI=",
                            LamaCompanyId = new Guid("13585770-17b3-4a9e-bbd7-4dff9cc9a5e2"),
                            LandLineNumber = "SSEfX6+eGdQho09y+G6bag==",
                            Name = "VKoWZeB+QezSZdJzg/G6ig==",
                            PhoneNumber = "vSRpqIgaDb+VbmBPGRPIWg=="
                        },
                        new
                        {
                            Id = new Guid("076466c2-1540-4a4d-98eb-12bea4a8c71b"),
                            Email = "XTBEsQ6hodiv2AxLjves1GHtrafYSmYGHwZYbCSMKYo=",
                            LamaCompanyId = new Guid("bae8c907-4851-40ae-99ee-fbdf8dc43c83"),
                            LandLineNumber = "SSEfX6+eGdQho09y+G6bag==",
                            Name = "e5rRqXltNck37nfPTN/RAw==",
                            PhoneNumber = "vSRpqIgaDb+VbmBPGRPIWg=="
                        });
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.UserConfirmationToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationTimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserConfirmationTokens");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Canton", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.Country", "Country")
                        .WithMany("Cantons")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.City", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.Canton", "Canton")
                        .WithMany("Cities")
                        .HasForeignKey("CantonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Canton");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Organization", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.OrganizationType", "OrganizationType")
                        .WithMany("Organizations")
                        .HasForeignKey("OrganizationTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.User", "SupportPerson")
                        .WithMany("SupportOrganizations")
                        .HasForeignKey("SupportPersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("OrganizationType");

                    b.Navigation("SupportPerson");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.OrganizationNote", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.Organization", "Organization")
                        .WithMany("Notes")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.User", "User")
                        .WithMany("OrganizationNotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.SubOrganization", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.Organization", "Organization")
                        .WithMany("SubOrganizations")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.SupportPersonOrgTypeDefaultMapping", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.OrganizationType", "OrganizationType")
                        .WithMany()
                        .HasForeignKey("OrganizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.User", "User")
                        .WithMany("SupportPersonOrgTypeDefaults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnel", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelStatus", "Status")
                        .WithMany("TestingPersonnels")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelConfirmation", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnel", "TestingPersonnel")
                        .WithMany("TestingPersonnelConfirmations")
                        .HasForeignKey("TestingPersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelInvitation", "TestingPersonnelInvitation")
                        .WithMany("TestingPersonnelConfirmations")
                        .HasForeignKey("TestingPersonnelInvitationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestingPersonnel");

                    b.Navigation("TestingPersonnelInvitation");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelInvitation", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.User", "SentByUser")
                        .WithMany()
                        .HasForeignKey("SentByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SentByUser");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelInvitationConfirmationToken", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnel", "TestingPersonnel")
                        .WithMany()
                        .HasForeignKey("TestingPersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelInvitation", "TestingPersonnelInvitation")
                        .WithMany()
                        .HasForeignKey("TestingPersonnelInvitationId")
                        .HasConstraintName("FK_TestingPersonnelInvitationConfirmationTokens_TestingPerson~1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestingPersonnel");

                    b.Navigation("TestingPersonnelInvitation");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelWorkingArea", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnel", "TestingPersonnel")
                        .WithMany("TestingPersonnelWorkingAreas")
                        .HasForeignKey("TestingPersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestingPersonnel");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.User", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.LamaCompany", "LamaCompany")
                        .WithMany("Users")
                        .HasForeignKey("LamaCompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.Organization", "Organization")
                        .WithMany("Contacts")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("LamaCompany");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.UserConfirmationToken", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Canton", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Country", b =>
                {
                    b.Navigation("Cantons");

                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.LamaCompany", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Organization", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Notes");

                    b.Navigation("SubOrganizations");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.OrganizationType", b =>
                {
                    b.Navigation("Organizations");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnel", b =>
                {
                    b.Navigation("TestingPersonnelConfirmations");

                    b.Navigation("TestingPersonnelWorkingAreas");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelInvitation", b =>
                {
                    b.Navigation("TestingPersonnelConfirmations");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.TestingPersonnelStatus", b =>
                {
                    b.Navigation("TestingPersonnels");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.User", b =>
                {
                    b.Navigation("OrganizationNotes");

                    b.Navigation("SupportOrganizations");

                    b.Navigation("SupportPersonOrgTypeDefaults");
                });
#pragma warning restore 612, 618
        }
    }
}
