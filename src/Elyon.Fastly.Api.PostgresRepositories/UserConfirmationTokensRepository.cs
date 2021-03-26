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
using Elyon.Fastly.Api.Domain.Dtos;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class UserConfirmationTokensRepository : BaseRepository, IUserConfirmationTokensRepository
    {
        public UserConfirmationTokensRepository(
            Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper)
            : base(contextFactory, mapper)
        {

        }

        public async Task<string> AddUserConfirmationTokenAsync(Guid userId, DateTime expirationDate)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.UserConfirmationTokens;

            var entity = new UserConfirmationToken()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ExpirationTimeStamp = expirationDate,
                Token = Guid.NewGuid().ToString(),
                IsUsed = false
            };           

            await items.AddAsync(entity).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity.Token;
        }

        public async Task<UserOrganizationDto> GetUserOrgByConfirmationTokenAsync(string token, DateTime expirationDate)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.UserConfirmationTokens;
            var userConfToken = await items
                .Include(x => x.User)
                .FirstOrDefaultAsync(t => t.Token == token && t.ExpirationTimeStamp >= expirationDate && !t.IsUsed)
                .ConfigureAwait(false);

            if(userConfToken == null)
            {
                return null;
            }

            return new UserOrganizationDto { UserId = userConfToken.UserId, OrganizationId = userConfToken.User?.OrganizationId};
        }

        public async Task DisposeConfirmationToken(string token)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.UserConfirmationTokens;
            var entity = await items.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Token == token)
                .ConfigureAwait(false);

            if(entity != null)
            {
                entity.IsUsed = true;
                context.Entry(entity).State = EntityState.Modified;
                items.Update(entity);

                await context.SaveChangesAsync()
                    .ConfigureAwait(false);
            }            
        }
    }
}
