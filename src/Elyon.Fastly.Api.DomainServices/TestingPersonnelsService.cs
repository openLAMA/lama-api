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

using Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.DomainServices
{
    public class TestingPersonnelsService : BaseCrudService<TestingPersonnelDto>, ITestingPersonnelsService
    {
        private readonly DateTime _testsHistoryEarliestDate = new DateTime(2021, 3, 1);
        private readonly ITestingPersonnelStatusesRepository _testingPersonnelStatusesRepository;
        private readonly ITestingPersonnelsRepository _testingPersonnelsRepository;

        public TestingPersonnelsService(ITestingPersonnelsRepository testingPersonnelsRepository, 
            ITestingPersonnelStatusesRepository testingPersonnelStatusesRepository)
            : base(testingPersonnelsRepository)
        {
            _testingPersonnelStatusesRepository = testingPersonnelStatusesRepository;
            _testingPersonnelsRepository = testingPersonnelsRepository;
        }

        public async Task<List<TestingPersonnelStatusDto>> GetAllTestingPersonnelStatusesAsync()
        {
            return await _testingPersonnelStatusesRepository
                .GetAllTestingPersonnelStatusesAsync()
                .ConfigureAwait(false);
        }

        public async Task<TestsDataWithIsEarliestDateDto> GetTestsDataDtoAsync(DateTime startDate)
        {
            return await _testingPersonnelsRepository
               .GetTestsDataDtoAsync(startDate != DateTime.MinValue ? 
               (startDate < _testsHistoryEarliestDate ? _testsHistoryEarliestDate : startDate) : DateTime.UtcNow, false)
               .ConfigureAwait(false);
        }

        public async Task<TestsDataDto> GetTestsDataDtoForDateAsync(DateTime testDate)
        {
            var result = await _testingPersonnelsRepository
               .GetTestsDataDtoAsync(testDate, true)
               .ConfigureAwait(false);

            return result.TestsData.FirstOrDefault();
        }

        public async Task<bool> CheckTestingPersonnelEmailExistAsync(string testingPersonnelEmail, Guid testingPersonnelId)
        {
            return await _testingPersonnelsRepository
                .CheckTestingPersonnelEmailExistAsync(testingPersonnelEmail, testingPersonnelId)
                .ConfigureAwait(false);
        }

        public async Task<TestingPersonnelDto> CreateTestingPersonnelAsync(TestingPersonnelSpecDto specDto)
        {
            if (specDto == null)
            {
                throw new ArgumentNullException(nameof(specDto));
            }

            var existingTestingPersonnelId = await _testingPersonnelsRepository.GetTestingPersonnelIdByEmailAsync(specDto.Email)
                .ConfigureAwait(false);
            if (existingTestingPersonnelId != Guid.Empty)
            {
                ValidationDictionary
                    .AddModelError("Testing personnel with this email already exists", $"{specDto.Email}");
                return null;
            }

            var testingPersonnel = await AddAsync(specDto)
                .ConfigureAwait(false);

            return testingPersonnel;
        }

        public async Task<List<AvailableTemporaryPersonnelDto>> GetAvailableTemporaryPersonnelAsync(AvailableTemporaryPersonnelSpecDto specDto)
        {
            if (specDto == null)
            {
                throw new ArgumentNullException(nameof(specDto));
            }

            return await _testingPersonnelsRepository
                .GetAvailableTemporaryPersonnelForDateAndShiftAsync(specDto.Date, specDto.ShiftNumber)
                .ConfigureAwait(false);
        }
    }
}
