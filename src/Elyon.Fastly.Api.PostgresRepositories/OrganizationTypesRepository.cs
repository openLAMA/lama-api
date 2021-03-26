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
using Elyon.Fastly.Api.PostgresRepositories.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class OrganizationTypesRepository : BaseRepository, IOrganizationTypesRepository
    {
        public OrganizationTypesRepository(
            Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper)
            : base(contextFactory, mapper)
        {

        }

        public async Task<List<OrganizationTypeDto>> GetAllOrganizationTypesAsync()
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = await context.OrganizationTypes
                .Select(x => new OrganizationTypeDto { Id = x.Id, Name = x.Name })
                .ToListAsync()
                .ConfigureAwait(false);

            return items;
        }

        public async Task<bool> AnyOrganizationTypeAsync(Expression<Func<OrganizationTypeDto, bool>> predicate)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.OrganizationTypes;

            var entityPredicate = predicate.ReplaceParameter<OrganizationTypeDto, OrganizationType>();
            return await items.AnyAsync(entityPredicate).ConfigureAwait(false);
        }
    }
}