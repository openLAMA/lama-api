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
using System.Threading.Tasks;
using AutoMapper;
using Elyon.Fastly.Api.Domain.Dtos.Cantons;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Prime.Sdk.Db.Common;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class CantonsRepository : BaseCrudRepository<Canton, CantonDto>, ICantonsRepository
    {
        public CantonsRepository(Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper)
            : base(contextFactory, mapper)
        {
        }

        protected override async Task<CantonDto> GetByIdAsync(Guid id, TransactionContext transactionContext)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Cantons.AsNoTracking()
                .Include(x => x.CantonWeekdaysSamples)
                .FirstOrDefaultAsync(item => item.Id == id)
                .ConfigureAwait(false);

            return Mapper.Map<Canton, CantonDto>(entity);
        }

        protected override async Task UpdateAsync(CantonDto item, TransactionContext transactionContext)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var cantons = context.Cantons;

            var entity = Mapper.Map<Canton>(item);

            var dbEntity = await cantons
               .Include(x => x.CantonWeekdaysSamples)
               .FirstOrDefaultAsync(c => c.Id == item.Id)
               .ConfigureAwait(false);

            context.Entry(dbEntity.CantonWeekdaysSamples).CurrentValues.SetValues(entity.CantonWeekdaysSamples);
            context.Entry(dbEntity.CantonWeekdaysSamples).State = EntityState.Modified;

            Mapper.Map(item, dbEntity);

            OnBeforeUpdate(dbEntity);

            context.Entry(dbEntity).State = EntityState.Modified;
            cantons.Update(dbEntity);

            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
