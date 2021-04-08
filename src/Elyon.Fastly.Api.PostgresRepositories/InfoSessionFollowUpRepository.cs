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
using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUps;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class InfoSessionFollowUpRepository : BaseRepository, IInfoSessionFollowUpRepository
    {
        public InfoSessionFollowUpRepository(Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper) 
            : base(contextFactory, mapper)
        {
        }

        public async Task<string> AddInfoSessionFollowUpAsync(Guid organizationId)
        {
            var entity = new InfoSessionFollowUp
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                Token = Guid.NewGuid().ToString(),
                Status = InfoSessionFollowUpStatus.Sent
            };

            var context = ContextFactory.CreateDataContext(null);
            await context.Set<InfoSessionFollowUp>().AddAsync(entity).ConfigureAwait(false);
            context.Entry(entity).State = EntityState.Detached;

            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity.Token;
        }

        public async Task UpdateStatusAsync(string token, InfoSessionFollowUpStatus newStatus)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));

            var context = ContextFactory.CreateDataContext(null);
            var items = context.Set<InfoSessionFollowUp>();
            var entityToUpdate = await items.AsNoTracking()
                .FirstOrDefaultAsync(item => item.Token == token)
                .ConfigureAwait(false);

            if (entityToUpdate != default)
            {
                entityToUpdate.Status = newStatus;
                context.Entry(entityToUpdate).Property(followUp => followUp.Status).IsModified = true;
                items.Update(entityToUpdate);

                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<string> GetTokenByOrganizationIdAsync(Guid organizationId)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Set<Organization>().AsNoTracking()
                .Include(item => item.InfoSessionFollowUp)
                .FirstOrDefaultAsync(item => item.Id == organizationId)
                .ConfigureAwait(false);

            return entity?.InfoSessionFollowUp.Token ?? null;
        }

        public async Task<InfoSessionFollowUpStatus> GetStatusByTokenAsync(string token)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Set<InfoSessionFollowUp>().AsNoTracking()
                .FirstOrDefaultAsync(item => item.Token == token)
                .ConfigureAwait(false);

            return entity.Status;
        } 
    }
}
