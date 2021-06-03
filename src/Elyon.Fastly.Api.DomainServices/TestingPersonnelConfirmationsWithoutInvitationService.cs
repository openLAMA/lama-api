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
    public class TestingPersonnelConfirmationsWithoutInvitationService : BaseService, ITestingPersonnelConfirmationsWithoutInvitationService
    {
        private const string dateFormat = "d/M/yyyy";
        private readonly ITestingPersonnelConfirmationsWithoutInvitationRepository _repository;
        private readonly ITestingPersonnelsRepository _testingPersonnelsRepository;
        private readonly ITestingPersonnelInvitationsRepository _testingPersonnelInvitationsRepository;

        public TestingPersonnelConfirmationsWithoutInvitationService(ITestingPersonnelConfirmationsWithoutInvitationRepository repository,
            ITestingPersonnelsRepository testingPersonnelsRepository, ITestingPersonnelInvitationsRepository testingPersonnelInvitationsRepository)
        {
            _repository = repository;
            _testingPersonnelsRepository = testingPersonnelsRepository;
            _testingPersonnelInvitationsRepository = testingPersonnelInvitationsRepository;
        }

        public async Task CreateConfirmationWithoutInvitationAsync(TestingPersonnelConfirmationsWithoutInvitationSpecDto specDto)
        {
            if (specDto == null)
                throw new ArgumentNullException(nameof(specDto));

            var invitationExists = await _testingPersonnelInvitationsRepository.AnyAsync(i => i.InvitationForDate.Date == specDto.Date.Date)
                .ConfigureAwait(false);

            if (invitationExists)
            {
                ValidationDictionary
                    .AddModelError("Invitation for selected date already exists", $"{specDto.Date.ToString(dateFormat, CultureInfo.InvariantCulture)}");
                return;
            }

            var testingPersonnelExists = await _testingPersonnelsRepository
                .AnyAsync(tp => tp.Id == specDto.TestingPersonnelId && tp.Type == TestingPersonnelType.Temporary)
                .ConfigureAwait(false);

            if (!testingPersonnelExists)
            {
                ValidationDictionary
                    .AddModelError("Testing personnel does not exist or is not of Temporary type", 
                        $"Testing personnel does not exist or is not of Temporary type");
                return;
            }

            bool confirmationForDateExists = await _repository.DoesConfirmationExistAsync(specDto.TestingPersonnelId, specDto.Date, specDto.ShiftNumber)
                .ConfigureAwait(false);

            if (confirmationForDateExists)
            {
                ValidationDictionary
                    .AddModelError("Confirmation for testing personnel for date and shift already exists", 
                    $"Confirmation for testing personnel for date and shift already exists") ;
                return;
            }

            await _repository.AddConfirmationAsync(specDto.TestingPersonnelId, specDto.Date, specDto.ShiftNumber)
                .ConfigureAwait(false);
        }

        public async Task DeleteConfirmationWithoutInvitationAsync(Guid id)
        {
            await _repository.DeleteConfirmationAsync(id)
                .ConfigureAwait(false);
        }

        public async Task<bool> DoesConfirmationExistAsync(Guid id)
        {
            return await _repository.DoesConfirmationExistAsync(id)
                .ConfigureAwait(false);

        }
    }
}
