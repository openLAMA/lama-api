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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Dtos.Cities;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;

namespace Elyon.Fastly.Api.DomainServices
{
    public class CitiesService : BaseService, ICitiesService
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesService(ICitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            return await _citiesRepository.GetAllCitiesAsync()
                .ConfigureAwait(false);
        }

        public async Task<CityDto> GetCityByIdAsync(Guid id)
        {
            return await _citiesRepository.GetCityByIdAsync(id)
                .ConfigureAwait(false);
        }
    }
}