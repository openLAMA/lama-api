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
using Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels;
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class TestingPersonnelConfirmationsRepository : BaseRepository, ITestingPersonnelConfirmationsRepository
    {
        public TestingPersonnelConfirmationsRepository(
           Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper)
           : base(contextFactory, mapper)
        {

        }

        public async Task<TestingPersonnelInvitationConfirmedShiftsDto> AddConfirmationOfInvitationAsync(TestingPersonnelConfirmationSpecDto specDto)
        {
            if (specDto == null)
            {
                throw new ArgumentNullException(nameof(specDto));
            }
            var confirmedShifts = new TestingPersonnelInvitationConfirmedShiftsDto()
            {
                ShiftsBooked = new List<int>()
            };

            await using var context = ContextFactory.CreateDataContext(null);
            await using var transaction = context.Database.BeginTransaction();
            var confirmations = context.TestingPersonnelConfirmations;

            var invitation = await context.TestingPersonnelInvitations
                .Where(x => x.Id == specDto.InvitationId)
                .Include(x => x.TestingPersonnelConfirmations)
                .Select(x => new
                {
                    RequiredPersonnelCountShift1 = x.RequiredPersonnelCountShift1,
                    RequiredPersonnelCountShift2 = x.RequiredPersonnelCountShift2,
                    TestingPersonnelConfirmationsShift1 = x.TestingPersonnelConfirmations.Where(conf => conf.ShiftNumber == ShiftNumber.First).Count(),
                    TestingPersonnelConfirmationsShift2 = x.TestingPersonnelConfirmations.Where(conf => conf.ShiftNumber == ShiftNumber.Second).Count()
                })
                .FirstAsync()
                .ConfigureAwait(false);

            if (invitation.RequiredPersonnelCountShift1 > invitation.TestingPersonnelConfirmationsShift1 ||
                invitation.RequiredPersonnelCountShift2 > invitation.TestingPersonnelConfirmationsShift2)
            {
                foreach (var shiftNumber in specDto.ShiftNumbers)
                {
                    var entity = Mapper.Map<TestingPersonnelConfirmationSpecDto, TestingPersonnelConfirmation>(specDto);
                    entity.Id = Guid.NewGuid();
                    entity.AcceptedOn = DateTime.UtcNow;

                    if (shiftNumber == ShiftNumber.First &&
                        invitation.RequiredPersonnelCountShift1 > invitation.TestingPersonnelConfirmationsShift1)
                    {
                        entity.ShiftNumber = ShiftNumber.First;
                        confirmations.Add(entity);
                        confirmedShifts.ShiftsBooked.Add(1);
                    }
                    else if (shiftNumber == ShiftNumber.Second &&
                        invitation.RequiredPersonnelCountShift2 > invitation.TestingPersonnelConfirmationsShift2)
                    {
                        entity.ShiftNumber = ShiftNumber.Second;
                        confirmations.Add(entity);
                        confirmedShifts.ShiftsBooked.Add(2);
                    }
                }

                await context.SaveChangesAsync().ConfigureAwait(false);

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            return confirmedShifts;
        }
    }
}
