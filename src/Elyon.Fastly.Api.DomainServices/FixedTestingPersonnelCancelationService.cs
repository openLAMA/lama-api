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
using System.Globalization;
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels;
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;

namespace Elyon.Fastly.Api.DomainServices
{
    public class FixedTestingPersonnelCancelationService : BaseService, IFixedTestingPersonnelCancelationService
    {
        private const string dateFormat = "d/M/yyyy";

        private readonly IFixedTestingPersonnelCancelationRepository _repository;
        private readonly ITestingPersonnelsRepository _testingPersonnelsRepository;

        public FixedTestingPersonnelCancelationService(IFixedTestingPersonnelCancelationRepository repository,
            ITestingPersonnelsRepository testingPersonnelsRepository)
        {
            _repository = repository;
            _testingPersonnelsRepository = testingPersonnelsRepository;
        }

        public async Task CancelFixedTestingPersonnelForDateAsync(CancelFixedTestingPersonnelForDateSpecDto specDto)
        {
            if (specDto == null)
                throw new ArgumentNullException(nameof(specDto));

            Guid testingPersonnelId = await _testingPersonnelsRepository.GetTestingPersonnelIdByEmailAndTypeAsync(specDto.Email, TestingPersonnelType.Fixed)
                .ConfigureAwait(false);

            if (testingPersonnelId == Guid.Empty)
            {
                ValidationDictionary
                    .AddModelError("Fixed testing personnel with email was not found", $"{specDto.Email}");
                return;
            }

            var cancelationForDateExists = await _repository.DoesCancelationForTestingPersonnelAndDateExistAsync(testingPersonnelId, specDto.Date)
                .ConfigureAwait(false);

            if (cancelationForDateExists)
            {
                ValidationDictionary
                    .AddModelError("Cancelation for testing personnel for date already exists", $"{specDto.Date.ToString(dateFormat, CultureInfo.InvariantCulture)}");
                return;
            }

            await _repository.CancelFixedTestingPersonnelForDateAsync(testingPersonnelId, specDto.CanceledByUserId, specDto.Date.Date)
                .ConfigureAwait(false);
        }
    }
}
