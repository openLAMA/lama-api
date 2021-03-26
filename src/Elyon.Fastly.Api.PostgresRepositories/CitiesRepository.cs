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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Elyon.Fastly.Api.Domain.Dtos.Cities;
using Elyon.Fastly.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class CitiesRepository : BaseRepository, ICitiesRepository
    {
        public CitiesRepository(Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper)
           : base(contextFactory, mapper)
        {
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var cities = await context.Cities.OrderBy(c => c.Name)
                .ToListAsync()
                .ConfigureAwait(false);

            return Mapper.Map<List<CityDto>>(cities);
        }

        public async Task<CityDto> GetCityByIdAsync(Guid id)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var entity = await context.Cities.AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id == id)
                .ConfigureAwait(false);

            return Mapper.Map<CityDto>(entity);
        }

        public async Task<CityEpaadDto> GetCityEpaadDtoAsync(Guid id)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var entity = await context.Cities.AsNoTracking()
                .Include(x => x.Canton)
                .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(item => item.Id == id)
                .ConfigureAwait(false);

            return new CityEpaadDto
            {
                Name = entity.Name,
                ZipCode = entity.ZipCode,
                CountryShortName = entity.Canton?.Country?.ShortName
            };
        }
    }
}