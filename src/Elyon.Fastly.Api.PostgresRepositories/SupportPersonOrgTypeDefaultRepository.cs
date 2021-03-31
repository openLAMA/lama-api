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
using Elyon.Fastly.Api.Domain;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Prime.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class SupportPersonOrgTypeDefaultRepository : BaseRepository, ISupportPersonOrgTypeDefaultRepository
    {
        private readonly IAESCryptography _aesCryptography;

        public SupportPersonOrgTypeDefaultRepository(
            Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, 
            IMapper mapper, IAESCryptography aesCryptography)
            : base(contextFactory, mapper)
        {
            _aesCryptography = aesCryptography;
        }

        public async Task<ShortContactInfoDto> GetSupportPersonByOrganizationTypeAsync(int organizationTypeId)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.SupportPersonOrgTypeDefaultMappings;

            var defaulSupportPerson = await items.Where(
                    sup => sup.OrganizationTypeId == organizationTypeId)
                .Include(sup => sup.User)
                .Select(x => new ShortContactInfoDto
                {
                    SupportPersonId = x.UserId,
                    Name = _aesCryptography.Decrypt(x.User.Name)
                }).FirstOrDefaultAsync()
                .ConfigureAwait(false);
            
            if(defaulSupportPerson == null)
            {
                return await items.Select(x =>
                    new ShortContactInfoDto
                    {
                        SupportPersonId = x.UserId,
                        Name = _aesCryptography.Decrypt(x.User.Name)
                    })
                    .FirstAsync()
                    .ConfigureAwait(false);
            }

            return defaulSupportPerson;
        }

        public async Task<List<ShortContactInfoDto>> GetSupportPeopleByOrganizationTypeAsync(int organizationTypeId)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.SupportPersonOrgTypeDefaultMappings;

            var defaulSupportPeople = await items.Where(
                    sup => sup.OrganizationTypeId == organizationTypeId)
                .Select(x => new ShortContactInfoDto
                {
                    SupportPersonId = x.UserId,
                    Name = _aesCryptography.Decrypt(x.User.Name)
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return defaulSupportPeople;
        }
    }
}
