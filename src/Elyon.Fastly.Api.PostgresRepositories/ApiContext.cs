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

using Elyon.Fastly.Api.PostgresRepositories.DataSeed;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Prime.Sdk.PostgreSql;
using System;
using System.Data.Common;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class ApiContext : PostgreSqlContext
    {
        private const string Schema = "ApiDb";

        public DbSet<User> Users { get; set; }

        public DbSet<UserConfirmationToken> UserConfirmationTokens { get; set; }

        public DbSet<LamaCompany> LamaCompanies { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Canton> Cantons { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<OrganizationType> OrganizationTypes { get; set; }

        public DbSet<SupportPersonOrgTypeDefaultMapping> SupportPersonOrgTypeDefaultMappings { get; set; }

        public DbSet<TestingPersonnel> TestingPersonnels { get; set; }

        public DbSet<TestingPersonnelConfirmation> TestingPersonnelConfirmations { get; set; }

        public DbSet<TestingPersonnelInvitation> TestingPersonnelInvitations { get; set; }

        public DbSet<TestingPersonnelWorkingArea> TestingPersonnelWorkingAreas { get; set; }

        public DbSet<TestingPersonnelStatus> TestingPersonnelStatuses { get; set; }

        public DbSet<TestingPersonnelInvitationConfirmationToken> TestingPersonnelInvitationConfirmationTokens { get; set; }

        public DbSet<SubOrganization> SubOrganizations { get; set; }

        public DbSet<OrganizationNote> OrganizationNotes { get; set; }

        public DbSet<InfoSessionFollowUp> InfoSessionFollowUps { get; set; }

        public ApiContext() : base(Schema)
        {            
        }

        public ApiContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {            
        }

        public ApiContext(DbContextOptions options)
            : base(Schema, options)
        {            
        }

        public ApiContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {            
        }

        protected override void OnPrimeModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Entity<User>()
                .HasIndex(b => b.Id);

            modelBuilder.Entity<LamaCompany>()
                .HasMany(lc => lc.Users)
                .WithOne(u => u.LamaCompany)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Cities)
                .WithOne(ci => ci.Country)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Canton>()
                .HasMany(ca => ca.Cities)
                .WithOne(ci => ci.Canton)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Contacts)
                .WithOne(u => u.Organization)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Organization>()
                .HasOne(o => o.SupportPerson)
                .WithMany(u => u.SupportOrganizations)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.SupportOrganizations)
                .WithOne(o => o.SupportPerson)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrganizationType>()
                .HasMany(ot => ot.Organizations)
                .WithOne(o => o.OrganizationType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrganizationNote>()
                .HasOne(n => n.User)
                .WithMany(u => u.OrganizationNotes)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrganizationNote>()
                .HasOne(n => n.Organization)
                .WithMany(o => o.Notes)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrganizationNote>()
                .HasIndex(n => n.OrganizationId);

            modelBuilder.Entity<TestingPersonnelInvitation>()
               .HasIndex(b => b.InvitationForDate);

            modelBuilder.Entity<InfoSessionFollowUp>()
                .HasOne(i => i.Organization)
                .WithOne(o => o.InfoSessionFollowUp)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrganizationType>()
               .HasData(DataSeeder.SeedOrganizationTypes());

            modelBuilder.Entity<Country>()
              .HasData(DataSeeder.SeedCountry());

            modelBuilder.Entity<Canton>()
              .HasData(DataSeeder.SeedCanton());

            modelBuilder.Entity<City>()
              .HasData(DataSeeder.SeedCities());

            modelBuilder.Entity<LamaCompany>()
              .HasData(DataSeeder.SeedLamaCompanies());

            modelBuilder.Entity<User>()
              .HasData(DataSeeder.SeedDefaulSupportUsersForLamaCompanies());

            modelBuilder.Entity<SupportPersonOrgTypeDefaultMapping>()
              .HasData(DataSeeder.SeedDefaultSupportPersonOrgTypeRelation());

            modelBuilder.Entity<TestingPersonnelStatus>()
             .HasData(DataSeeder.SeedDefaultTestingPersonnelStatuses());
        }
    }
}
