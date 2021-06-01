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
using Elyon.Fastly.Api.Domain.Dtos.Cantons;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;

namespace Elyon.Fastly.Api.DomainServices
{
    public class CantonsService : BaseCrudService<CantonDto>, ICantonsService
    {
        private const string CountryName = "Schweiz";
        private const string BaselCantonName = "Basel";
        private const string BaselCantonShortName = "BL";
        private readonly ICountriesRepository _countriesRepository;

        public CantonsService(ICantonsRepository cantonsRepository, ICountriesRepository countriesRepository)
            : base(cantonsRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public async Task<CantonDto> CreateCantonWithSamplesCountAsync(CantonSpecDto cantonSpecDto)
        {
            if (cantonSpecDto == null)
            {
                throw new ArgumentNullException(nameof(cantonSpecDto));
            }

            Guid countryId = await _countriesRepository.GetCountryIdByNameAsync(CountryName)
                .ConfigureAwait(false);
            cantonSpecDto.CountryId = countryId;

            var doesCantonExist = await AnyAsync(c => c.Name == cantonSpecDto.Name || c.ShortName == cantonSpecDto.ShortName)
                .ConfigureAwait(false);

            if (doesCantonExist)
            {
                ValidationDictionary
                    .AddModelError("Canton with this name or abbreviation already exists", $"{cantonSpecDto.Name}, {cantonSpecDto.ShortName}");

                return null;
            }

            return await AddAsync(cantonSpecDto)
                .ConfigureAwait(false);
        }

        public async Task UpdateCantonWithSamplesCountAsync(CantonDto cantonDto)
        {
            if (cantonDto == null)
            {
                throw new ArgumentNullException(nameof(cantonDto));
            }

            var cantonToUpdate = await GetByIdAsync(cantonDto.Id)
                .ConfigureAwait(false);

            if (cantonToUpdate == null)
            {
                ValidationDictionary
                    .AddModelError("Canton with ID does not exist", $"{cantonDto.Id}");

                return;
            }

            if (cantonToUpdate.Name == BaselCantonName && cantonToUpdate.ShortName == BaselCantonShortName)
            {
                ValidationDictionary
                    .AddModelError("The default canton can not be updated", $"{cantonToUpdate.Name}, {cantonToUpdate.ShortName}");

                return;
            }

            var doesCantonExist = await AnyAsync(c => (c.Name == cantonDto.Name || c.ShortName == cantonDto.ShortName) && c.Id != cantonDto.Id)
                .ConfigureAwait(false);

            if (doesCantonExist)
            {
                ValidationDictionary
                    .AddModelError("Canton with this name or abbreviation already exists", $"{cantonDto.Name}, {cantonDto.ShortName}");

                return;
            }

            await UpdateAsync(cantonDto)
                .ConfigureAwait(false);
        }

        public async Task DeleteCantonAsync(Guid id)
        {
            var cantonToDelete = await GetByIdAsync(id)
                .ConfigureAwait(false);

            if (cantonToDelete.Name == BaselCantonName && cantonToDelete.ShortName == BaselCantonShortName)
            {
                ValidationDictionary
                    .AddModelError("The default canton can not be deleted", $"{cantonToDelete.Name}, {cantonToDelete.ShortName}");

                return;
            }

            await DeleteAsync(id)
                .ConfigureAwait(false);
        }
    }
}
