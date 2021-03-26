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
using Elyon.Fastly.Api.Domain.Dtos.LamaCompanies;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class LamaCompaniesRepository : BaseCrudRepository<LamaCompany, LamaCompanyDto>, ILamaCompaniesRepository
    {
        public LamaCompaniesRepository(Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper)
            : base(contextFactory, mapper)
        {
        }

        public async Task<bool> IsUserPartOfLamaCompanyAsync(Guid lamaCompanyId, Guid userId)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            return await context.LamaCompanies
                .AnyAsync(org => org.Id == lamaCompanyId && org.Users.Select(x => x.Id).Contains(userId))
                .ConfigureAwait(false);
        }

        public async Task<LamaCompanyProfileDto> GetLamaCompanyProfileAsync(Guid lamaCompanyId)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.LamaCompanies.AsNoTracking()
                .Include(x => x.Users)
                .ThenInclude(x => x.SupportPersonOrgTypeDefaults)
                .ThenInclude(x => x.OrganizationType)
                .FirstOrDefaultAsync(item => item.Id == lamaCompanyId)
                .ConfigureAwait(false);

            return Mapper.Map<LamaCompany, LamaCompanyProfileDto>(entity);
        }

        public async Task UpdateLamaCompanyProfileAsync(LamaCompanyProfileSpecDto lamaCompanyProfileDto)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var organizations = context.LamaCompanies;

            var entity = Mapper.Map<LamaCompany>(lamaCompanyProfileDto);
            var dbEntity = await organizations
                .Include(x => x.Users)
                .ThenInclude(x => x.SupportPersonOrgTypeDefaults)
                .FirstOrDefaultAsync(t => t.Id == entity.Id)
                .ConfigureAwait(false);

            foreach (var user in dbEntity.Users)
            {
                if (!entity.Users.Any(i => i.Id == user.Id))
                    context.Users.Remove(user);                    
            }

            foreach (var newContact in entity.Users)
            {
                var dbContact = dbEntity.Users.FirstOrDefault(i => i.Id == newContact.Id);
                if (dbContact == null)
                {
                    dbEntity.Users.Add(newContact);
                }
                else
                {
                    newContact.LamaCompanyId = dbEntity.Id;
                    context.Entry(dbContact).CurrentValues.SetValues(newContact);
                    context.Entry(dbContact).State = EntityState.Modified;
                }
            }

            OnBeforeUpdate(dbEntity);

            Mapper.Map(lamaCompanyProfileDto, dbEntity);

            context.Entry(dbEntity).State = EntityState.Modified;
            organizations.Update(dbEntity);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
