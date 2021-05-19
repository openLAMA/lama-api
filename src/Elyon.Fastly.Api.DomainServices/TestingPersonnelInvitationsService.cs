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
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.DomainServices
{
    public class TestingPersonnelInvitationsService : BaseCrudService<TestingPersonnelInvitationDto>, ITestingPersonnelInvitationsService
    {
        private const string dateFormat = "d/M/yyyy";
        private const int capacityToAdd = 1;

        private readonly ITestingPersonnelInvitationsRepository _testingPersonnelInvitationsRepository;
        private readonly ITestingPersonnelsRepository _testingPersonnelsRepository;
        private readonly ITestingPersonnelInvitationConfirmationTokensRepository _invitationConfirmationTokensRepository;
        private readonly ITestingPersonnelConfirmationsRepository _testingPersonnelConfirmationsRepository;
        private readonly IEmailSenderService _mailSender;

        public TestingPersonnelInvitationsService(ITestingPersonnelInvitationsRepository testingPersonnelInvitationsRepository,
            ITestingPersonnelsRepository testingPersonnelsRepository,
            ITestingPersonnelInvitationConfirmationTokensRepository invitationConfirmationTokensRepository,
            ITestingPersonnelConfirmationsRepository testingPersonnelConfirmationsRepository, IEmailSenderService mailSender)
            : base(testingPersonnelInvitationsRepository)
        {
            _testingPersonnelInvitationsRepository = testingPersonnelInvitationsRepository;
            _testingPersonnelsRepository = testingPersonnelsRepository;
            _invitationConfirmationTokensRepository = invitationConfirmationTokensRepository;
            _testingPersonnelConfirmationsRepository = testingPersonnelConfirmationsRepository;
            _mailSender = mailSender;
        }

        public async Task CreateInvitationAsync(TestingPersonnelInvitationSpecDto specDto)
        {
            if(specDto == null)
            {
                throw new ArgumentNullException(nameof(specDto));
            }

            var alreadySentInvitationForDate = await AnyAsync(x => x.InvitationForDate.Date == specDto.Date.Date)
                .ConfigureAwait(false);
            if (alreadySentInvitationForDate)
            {
                ValidationDictionary
                    .AddModelError("Invitation for the date has already been sent", $"{specDto.Date.ToString(dateFormat, CultureInfo.InvariantCulture)}");
                return;
            }

            var invitationDto = await AddAsync(specDto)
                 .ConfigureAwait(false);

            var invitationReceivers = await _testingPersonnelsRepository
                .GetTestingPersonnelInvitationReceiversByWorkingAreaAsync(WorkingArea.Pooling, specDto.Date.DayOfWeek)
                .ConfigureAwait(false);

            foreach(var receiver in invitationReceivers)
            {
                var invitationConfirmationToken = await _invitationConfirmationTokensRepository
                    .AddTestingPersonnelInvitationConfirmationTokenAsync(receiver.TestingPersonnelId, invitationDto.Id)
                    .ConfigureAwait(false);

                await _mailSender.SendInvitationForPoolingAssignmentAsync(receiver.Email, invitationConfirmationToken, invitationDto.InvitationForDate)
                    .ConfigureAwait(false);
            }
        }

        public async Task<TestingPersonnelInvitationConfirmedShiftsDto> ConfirmInvitationAsync(TestingPersonnelInvitationConfirmDto confirmDto)
        {
            if(confirmDto == null)
            {
                throw new ArgumentNullException(nameof(confirmDto));
            }

            var testingPersonnelAndInvitationData = await _invitationConfirmationTokensRepository
                .GetTestingPersonnelAndInvitationByConfirmationTokenAsync(confirmDto.Token)
                .ConfigureAwait(false);

            if (testingPersonnelAndInvitationData == null)
            {
                ValidationDictionary
                    .AddModelError("The token is not valid", $"The provided token has been already used or doesn't exist.");

                return null;
            }

            var doesConfirmationExist = await _testingPersonnelConfirmationsRepository
                .CheckIfConfirmationExistsForInvitationAndTestingPersonnelAsync(testingPersonnelAndInvitationData.InvitationId, testingPersonnelAndInvitationData.TestingPersonnelId)
                .ConfigureAwait(false);

            if (doesConfirmationExist)
            {
                ValidationDictionary
                    .AddModelError("Confirmation already exists", $"Confirmation for this invitation already exists.");

                await _invitationConfirmationTokensRepository
                .DisposeInvitationConfirmationToken(confirmDto.Token)
                .ConfigureAwait(false);

                return null;
            }

            var testingPersonnelConfirmSpecDto = new TestingPersonnelConfirmationSpecDto
            {
                InvitationId = testingPersonnelAndInvitationData.InvitationId,
                PersonnelId = testingPersonnelAndInvitationData.TestingPersonnelId,
                ShiftNumbers = confirmDto.Shifts
            };

            var shiftsBooked = await _testingPersonnelConfirmationsRepository
                .AddConfirmationOfInvitationAsync(testingPersonnelConfirmSpecDto)
                .ConfigureAwait(false);

            await _invitationConfirmationTokensRepository
                .DisposeInvitationConfirmationToken(confirmDto.Token)
                .ConfigureAwait(false);

            if (shiftsBooked.ShiftsBooked.Any())
            {
                await _mailSender
                    .SendConfirmationForPoolingAssignmentAsync(testingPersonnelAndInvitationData.Email,
                    testingPersonnelAndInvitationData.InvitationDate, shiftsBooked.ShiftsBooked)
                    .ConfigureAwait(false);
            }

            return shiftsBooked;
        }

        public async Task CancelConfirmationAsync(TestingPersonnelCancelConfrimationSpecDto specDto)
        {
            if (specDto == null)
                throw new ArgumentNullException(nameof(specDto));

            var invitationId = await _testingPersonnelInvitationsRepository.GetInvitationIdByDateAsync(specDto.Date.Date)
                .ConfigureAwait(false);
            if (invitationId == Guid.Empty)
            {
                ValidationDictionary
                    .AddModelError("Invitation for date was not found", $"{specDto.Date.ToString(dateFormat, CultureInfo.InvariantCulture)}");
                return;
            }

            var testingPersonnelId = await _testingPersonnelsRepository.GetTestingPersonnelIdByEmailAsync(specDto.Email)
                .ConfigureAwait(false);

            if (testingPersonnelId == Guid.Empty)
            {
                ValidationDictionary
                    .AddModelError("Testing personnel with email was not found", $"{specDto.Email}");
                return;
            }

            await _testingPersonnelConfirmationsRepository
                .CancelInvitationConfirmationAsync(invitationId, testingPersonnelId, specDto.CanceledByUserId)
                .ConfigureAwait(false);
        }

        public async Task<TestingPersonnelInvitationConfirmedShiftsDto> CreateConfirmationAsync(TestingPersonnelManuallyAddedConfirmationDto confirmDto)
        {
            if (confirmDto == null)
                throw new ArgumentNullException(nameof(confirmDto));

            var invitationId = await _testingPersonnelInvitationsRepository.GetInvitationIdByDateAsync(confirmDto.Date.Date)
                .ConfigureAwait(false);
            if (invitationId == Guid.Empty)
            {
                ValidationDictionary
                    .AddModelError("Invitation for date was not found", $"{confirmDto.Date.ToString(dateFormat, CultureInfo.InvariantCulture)}");
                return null;
            }

            var doesUserExist = await _testingPersonnelsRepository
                .AnyAsync(tp => tp.Id == confirmDto.TestingPersonnelId)
                .ConfigureAwait(false);

            if (!doesUserExist)
            {
                ValidationDictionary
                    .AddModelError("Testing personnel was not found", $"Testing personnel was not found");
                return null;
            }

            var doesConfirmationExist = await _testingPersonnelConfirmationsRepository
                .CheckIfConfirmationExistsForSelectedShiftsAsync(invitationId, confirmDto.TestingPersonnelId, confirmDto.Shifts)
                .ConfigureAwait(false);

            if (doesConfirmationExist)
            {
                ValidationDictionary
                    .AddModelError("Confirmation already exists for selected testing personnel", $"Confirmation already exists for selected testing personnel");
                return null;
            }

            var testingPersonnelConfirmSpecDto = new TestingPersonnelConfirmationSpecDto
            {
                InvitationId = invitationId,
                PersonnelId = confirmDto.TestingPersonnelId,
                ShiftNumbers = confirmDto.Shifts
            };

            var confirmedShifts = await _testingPersonnelConfirmationsRepository
                .AddConfirmationOfInvitationAsync(testingPersonnelConfirmSpecDto)
                .ConfigureAwait(false);

            if (!confirmedShifts.ShiftsBooked.Any())
            {
                ValidationDictionary
                    .AddModelError("Shift capacity is already full", $"Shift capacity is already full");
                return null;
            }

            return confirmedShifts;
        }

        public async Task IncreaseShiftCountAsync(TestingPersonnelInvitationIncreaseShiftSpecDto specDto)
        {
            if (specDto == null)
                throw new ArgumentNullException(nameof(specDto));

            var doesInvitationExist = await _testingPersonnelInvitationsRepository.AnyAsync(i => i.Id == specDto.InvitationId)
                .ConfigureAwait(false);

            if (!doesInvitationExist)
            {
                ValidationDictionary
                    .AddModelError("The invitation was not found", $"The invitation was not found");
                return;
            }

            await _testingPersonnelInvitationsRepository.IncreaseShiftCountAsync(specDto.InvitationId, specDto.ShiftNumber, capacityToAdd)
                .ConfigureAwait(false);
        }
    }
}
