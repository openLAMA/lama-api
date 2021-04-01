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

using AutoMapper;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Prime.Sdk.PostgreSql;
using System;
using System.Linq;
using System.Threading.Tasks;
using Prime.Sdk.Db.Common;
using Elyon.Fastly.Api.Domain;
using System.Linq.Expressions;
using Elyon.Fastly.Api.PostgresRepositories.Extensions;
using System.Collections.Generic;
using Elyon.Fastly.Api.Domain.Dtos.EpaadDtos;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class OrganizationsRepository : BaseCrudRepository<Organization, OrganizationDto>, IOrganizationsRepository
    {
        public OrganizationsRepository(
            Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper)
            : base(contextFactory, mapper)
        {

        }

        protected override void OnBeforeInsert(Organization entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Status = OrganizationStatus.PendingContact;
            var utcNow = DateTime.UtcNow;
            entity.CreatedOn = utcNow;
            entity.LastUpdatedOn = utcNow;
        }

        public async Task<bool> IsUserPartOfOrganizationAsync(Guid organizationId, Guid userId)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            return await context.Organizations
                .AnyAsync(org => org.Id == organizationId && org.Contacts.Select(x => x.Id).Contains(userId))
                .ConfigureAwait(false);
        }

        protected override async Task<OrganizationDto> GetByIdAsync(Guid id, TransactionContext transactionContext)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Organizations.AsNoTracking()
                .Include(x => x.Contacts)
                .Include(x => x.SupportPerson)
                .Include(x => x.OrganizationType)
                .Include(x => x.SubOrganizations)
                .FirstOrDefaultAsync(item => item.Id == id)
                .ConfigureAwait(false);

            return Mapper.Map<Organization, OrganizationDto>(entity);
        }

        public async Task<OrganizationDashboardDto> GetOrganizationDashboardInfoAsync(Guid id)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Organizations.AsNoTracking()
                .Include(x => x.SupportPerson)
                .FirstOrDefaultAsync(item => item.Id == id)
                .ConfigureAwait(false);

            return Mapper.Map<Organization, OrganizationDashboardDto>(entity);
        }

        public async Task<OrganizationProfileDto> GetOrganizationProfileAsync(Guid id)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Organizations.AsNoTracking()
                .Include(x => x.Contacts)
                .Include(x => x.SupportPerson)
                .Include(x => x.OrganizationType)
                .FirstOrDefaultAsync(item => item.Id == id)
                .ConfigureAwait(false);

            return Mapper.Map<Organization, OrganizationProfileDto>(entity);
        }

        public async Task ChangeOrganizationStatusAsync(Guid userId, OrganizationStatus status)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.Users;
            var entity = await items.AsNoTracking()
                .Include(x => x.Organization)
                .FirstOrDefaultAsync(t => t.Id == userId)
                .ConfigureAwait(false);

            var userOrganization = entity.Organization;

            if (userOrganization != null)
            {
                userOrganization.Status = status;
                context.Entry(userOrganization).State = EntityState.Modified;
                context.Organizations.Update(userOrganization);

                await context.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        protected override void OnBeforeUpdate(Organization entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var utcNow = DateTime.UtcNow;

            entity.LastUpdatedOn = utcNow;
        }

        public async Task UpdateOrganizationProfileAsync(OrganizationProfileSpecDto organizationDto)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var organizations = context.Organizations;

            var entity = Mapper.Map<Organization>(organizationDto);

            var dbEntity = await organizations
               .Include(x => x.Contacts)
               .FirstOrDefaultAsync(t => t.Id == entity.Id)
               .ConfigureAwait(false);

            foreach (var dbContact in dbEntity.Contacts)
            {
                if (!entity.Contacts.Any(i => i.Id == dbContact.Id))
                    context.Users.Remove(dbContact);
            }

            foreach (var newContact in entity.Contacts)
            {
                var dbContact = dbEntity.Contacts.FirstOrDefault(i => i.Id == newContact.Id);
                if (dbContact == null)
                {
                    dbEntity.Contacts.Add(newContact);
                }
                else
                {
                    newContact.OrganizationId = dbEntity.Id;
                    context.Entry(dbContact).CurrentValues.SetValues(newContact);
                    context.Entry(dbContact).State = EntityState.Modified;
                }
            }

            OnBeforeUpdate(dbEntity);

            Mapper.Map(organizationDto, dbEntity);

            context.Entry(dbEntity).State = EntityState.Modified;
            organizations.Update(dbEntity);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<PagedResults<OrganizationDto>> GetAllOrganizationsAsync(
            Paginator paginator,
            Expression<Func<OrganizationDto, bool>> dtoFilter,
            string orderBy,
            bool isAscending)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var query = context.Organizations.AsNoTracking().AsQueryable();

            if (dtoFilter != null)
            {
                var entityFilter = dtoFilter.ReplaceParameter<OrganizationDto, Organization>();
                query = query.Where(entityFilter);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                var sortOrder = QueryExtensions.GenerateSortOrder<Organization>(orderBy, isAscending);
                query = sortOrder(query);
            }
            else
            {
                query = query.OrderBy(x => x.Name);
            }

            return await Mapper.ProjectTo<OrganizationDto>(query)
                .PaginateAsync(paginator)
                .ConfigureAwait(false);
        }

        protected override async Task UpdateAsync(OrganizationDto item, TransactionContext transactionContext)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var organizations = context.Organizations;

            var entity = Mapper.Map<Organization>(item);

            var dbEntity = await organizations
               .Include(x => x.Contacts)
               .Include(x => x.SubOrganizations)
               .FirstOrDefaultAsync(t => t.Id == entity.Id)
               .ConfigureAwait(false);

            foreach (var dbContact in dbEntity.Contacts)
            {
                if (!entity.Contacts.Any(i => i.Id == dbContact.Id))
                    context.Users.Remove(dbContact);
            }

            var dbContacts = new List<User>();
            dbContacts.AddRange(dbEntity.Contacts);
            foreach (var newContact in entity.Contacts)
            {
                var dbContact = dbContacts.FirstOrDefault(i => i.Id == newContact.Id);
                if (dbContact == null)
                {
                    dbEntity.Contacts.Add(newContact);
                }
                else
                {
                    newContact.OrganizationId = dbEntity.Id;
                    context.Entry(dbContact).CurrentValues.SetValues(newContact);
                    context.Entry(dbContact).State = EntityState.Modified;
                }
            }

            foreach (var dbSubOrganization in dbEntity.SubOrganizations)
            {
                if (!entity.SubOrganizations.Any(i => i.Id == dbSubOrganization.Id))
                    context.SubOrganizations.Remove(dbSubOrganization);
            }

            foreach (var newSubOrganization in entity.SubOrganizations)
            {
                var dbSubOrganization = dbEntity.SubOrganizations.FirstOrDefault(i => i.Id == newSubOrganization.Id);
                if (dbSubOrganization == null)
                {
                    dbEntity.SubOrganizations.Add(newSubOrganization);
                }
                else
                {
                    newSubOrganization.OrganizationId = dbEntity.Id;
                    context.Entry(dbSubOrganization).CurrentValues.SetValues(newSubOrganization);
                    context.Entry(dbSubOrganization).State = EntityState.Modified;
                }
            }

            Mapper.Map(item, dbEntity);

            OnBeforeUpdate(dbEntity);

            context.Entry(dbEntity).State = EntityState.Modified;
            organizations.Update(dbEntity);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<DateTime> GetOrganizationCreationDateAsync(Guid id)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Organizations.AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id == id)
                .ConfigureAwait(false);

            return entity.CreatedOn;
        }

        public async Task UpdateEpaadIdAsync(int epaadId, Guid organizationId)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.Organizations;
            var entity = await items.AsNoTracking()
                .FirstAsync(t => t.Id == organizationId)
                .ConfigureAwait(false);

            entity.EpaadId = epaadId;
            context.Entry(entity).State = EntityState.Modified;
            context.Organizations.Update(entity);

            await context.SaveChangesAsync()
                .ConfigureAwait(false);        
        }

        public async Task ChangeOrganizationActiveStatusAsync(OrganizationActiveStatusDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            await using var context = ContextFactory.CreateDataContext(null);
            var organizations = context.Organizations;
            var entity = await organizations.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == dto.Id)
                .ConfigureAwait(false);

            if (entity != null)
            {
                entity.Status = dto.Status;
                entity.LastUpdatedOn = DateTime.UtcNow;
                context.Entry(entity).State = EntityState.Modified;
                context.Organizations.Update(entity);

                await context.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        public async Task<OrganizationStatus> GetOrganizationStatusByOrganizationIdAsync(Guid organizationId)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Organizations.AsNoTracking()
                .FirstAsync(item => item.Id == organizationId)
                .ConfigureAwait(false);

            return entity.Status;
        }

        public async Task<OrganizationStatusCalculationDto> GetOrganizationStatusCalculationDataAsync(Guid organizationId)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Organizations.AsNoTracking()
                .FirstAsync(item => item.Id == organizationId)
                .ConfigureAwait(false);

            return new OrganizationStatusCalculationDto
            {
                FirstTestTimestamp = entity.FirstTestTimestamp,
                SecondTestTimestamp = entity.SecondTestTimestamp,
                ThirdTestTimestamp = entity.ThirdTestTimestamp,
                FourthTestTimestamp = entity.FourthTestTimestamp,
                FifthTestTimestamp = entity.FifthTestTimestamp,
                OnboardingTimestamp = entity.OnboardingTimestamp,
                TrainingTimestamp = entity.TrainingTimestamp,
                Status = entity.Status
            };
        }

        public async Task<List<OrganizationEpaadInfoDto>> GetOrganizationsWithEpaadIdAsync()
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var organizationsWithEpaadId = await context.Organizations.AsNoTracking()
                .Where(item => item.EpaadId.HasValue)
                .Select(x => new OrganizationEpaadInfoDto
                {
                    Id = x.Id,
                    EpaadId = x.EpaadId.Value
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return organizationsWithEpaadId;
        }

        public async Task UpdateRegisteredEmployeesAndShortNamePerOrganizationAsync(EpaadOrganizationDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            await using var context = ContextFactory.CreateDataContext(null);
            var organizations = context.Organizations;
            var entity = await organizations.AsNoTracking()
                .FirstAsync(t => t.EpaadId.HasValue && t.EpaadId.Value == dto.EpaadId)
                .ConfigureAwait(false);

            entity.RegisteredEmployees = dto.RegisteredEmployees;
            entity.OrganizationShorcutName = dto.OrganizationShortcutName;
            entity.LastUpdatedOn = DateTime.UtcNow;
            context.Entry(entity).State = EntityState.Modified;
            context.Organizations.Update(entity);

            await context.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task UpdateOrganizationStatusAsync(Guid organizationId, OrganizationStatus status)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var organizations = context.Organizations;
            var entity = await organizations.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == organizationId)
                .ConfigureAwait(false);

            if (entity != null)
            {
                entity.Status = status;
                entity.LastUpdatedOn = DateTime.UtcNow;
                context.Entry(entity).State = EntityState.Modified;
                context.Organizations.Update(entity);

                await context.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        public async Task<int?> GetOrganizationEpaadIdAsync(Guid organizationId)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Organizations.AsNoTracking()
                .FirstAsync(item => item.Id == organizationId)
                .ConfigureAwait(false);

            return entity.EpaadId;
        }
    }
}