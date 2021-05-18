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
    public class TestingPersonnelInvitationsRepository : BaseCrudRepository<TestingPersonnelInvitation, TestingPersonnelInvitationDto>,
        ITestingPersonnelInvitationsRepository
    {
        public TestingPersonnelInvitationsRepository(Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory,
            IMapper mapper)
            : base(contextFactory, mapper)
        {
        }

        public async Task<Guid> GetInvitationIdByDateAsync(DateTime testDate)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            return await context.TestingPersonnelInvitations
                .Where(item => item.InvitationForDate.Date == testDate)
                .Select(i => i.Id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task IncreaseShiftCountAsync(Guid invitationId, ShiftNumber shiftNumber, int capacityToAdd)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var items = context.TestingPersonnelInvitations;
            var entity = await items.AsNoTracking()
                .FirstAsync(t => t.Id == invitationId)
                .ConfigureAwait(false);

            if (shiftNumber == ShiftNumber.First)
            {
                entity.RequiredPersonnelCountShift1 += capacityToAdd;
            }

            if (shiftNumber == ShiftNumber.Second)
            {
                entity.RequiredPersonnelCountShift2 += capacityToAdd;
            }

            context.Entry(entity).State = EntityState.Modified;
            context.TestingPersonnelInvitations.Update(entity);

            await context.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        protected override void OnBeforeInsert(TestingPersonnelInvitation entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.CreatedOn = DateTime.UtcNow;
            base.OnBeforeInsert(entity);
        }
    }
}
