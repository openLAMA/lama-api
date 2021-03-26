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

// <auto-generated />
using System;
using Elyon.Fastly.Api.PostgresRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Elyon.Fastly.Api.PostgresRepositories.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20210225133358_RenameOrganizationCreatedOn")]
    partial class RenameOrganizationCreatedOn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EpaadId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("FirstTestTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("NumberOfPools")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfSamples")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("OnboardingTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OrganizationTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("SecondTestTimestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("SupportPersonId")
                        .HasColumnType("uuid");

                    b.Property<string>("Zip")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("OrganizationTypeId");

                    b.HasIndex("SupportPersonId");

                    b.ToTable("Organizations");
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
                            Id = 1,
                            Name = "Company"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pharmacy"
                        },
                        new
                        {
                            Id = 3,
                            Name = "School"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Nursing Home"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Hospital"
                        });
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("LamaCompanyId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
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
                        .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.SupportPersonOrgTypeDefaultMapping", b =>
                {
                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.OrganizationType", "OrganizationType")
                        .WithMany()
                        .HasForeignKey("OrganizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Elyon.Fastly.Api.PostgresRepositories.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationType");

                    b.Navigation("User");
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
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.OrganizationType", b =>
                {
                    b.Navigation("Organizations");
                });

            modelBuilder.Entity("Elyon.Fastly.Api.PostgresRepositories.Entities.User", b =>
                {
                    b.Navigation("SupportOrganizations");
                });
#pragma warning restore 612, 618
        }
    }
}
