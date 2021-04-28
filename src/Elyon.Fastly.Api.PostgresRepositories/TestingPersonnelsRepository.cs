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
using Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels;
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Elyon.Fastly.Api.PostgresRepositories.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class TestingPersonnelsRepository : BaseCrudRepository<TestingPersonnel, TestingPersonnelDto>, ITestingPersonnelsRepository
    {
        private const int DaysPeriod = 14;

        private readonly IAESCryptography _aesCryptography;

        public TestingPersonnelsRepository(Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory,
            IMapper mapper, IAESCryptography aesCryptography)
            : base(contextFactory, mapper)
        {
            _aesCryptography = aesCryptography;
        }

        protected override void OnBeforeInsert(TestingPersonnel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var utcNow = DateTime.UtcNow;

            entity.CreatedOn = utcNow;
            entity.LastUpdatedOn = utcNow;

            base.OnBeforeInsert(entity);
        }

        public async Task<List<TestsDataDto>> GetTestsDataDtoAsync()
        {
            const int companyOrganizationId = 82000;
            const int pharmacyOrganizationId = 82001;
            const int nursingHomesOrganizationId = 82003;
            const int hospitalOrganizationId = 82004;
            const int smeOrganizationId = 99990;

            var startDate = DateTime.UtcNow.Date;
            var endDate = startDate.AddDays(DaysPeriod);

            await using var context = ContextFactory.CreateDataContext(null);
            var orgs = await context.Organizations
                .Where(x => x.Status != OrganizationStatus.NotActive &&
                    ((x.FirstTestTimestamp.HasValue && x.FirstTestTimestamp.Value.Date < endDate.Date) ||
                    (x.SecondTestTimestamp.HasValue && x.SecondTestTimestamp.Value.Date < endDate.Date) ||
                    (x.ThirdTestTimestamp.HasValue && x.ThirdTestTimestamp.Value.Date < endDate.Date) ||
                    (x.FourthTestTimestamp.HasValue && x.FourthTestTimestamp.Value.Date < endDate.Date) ||
                    (x.FifthTestTimestamp.HasValue && x.FifthTestTimestamp.Value.Date < endDate.Date)))
                .Select(x => new
                {
                    TotalSamples = x.NumberOfSamples,
                    RegisteredEmployees = x.RegisteredEmployees,
                    FirstDate = x.FirstTestTimestamp,
                    SecondDate = x.SecondTestTimestamp,
                    ThirdDate = x.ThirdTestTimestamp,
                    FourthDate = x.FourthTestTimestamp,
                    FifthDate = x.FifthTestTimestamp,
                    ExclusionStartDate = x.ExclusionStartDate,
                    ExclusionEndDate = x.ExclusionEndDate,
                    IsCompanyOrPharmacyOrNursingOrganization = x.OrganizationTypeId == companyOrganizationId || x.OrganizationTypeId == pharmacyOrganizationId
                    || x.OrganizationTypeId == nursingHomesOrganizationId || x.OrganizationTypeId == hospitalOrganizationId
                    || x.OrganizationTypeId == smeOrganizationId
                })
                .ToListAsync()
                .ConfigureAwait(false);

            var invitations = await context.TestingPersonnelInvitations
                .Where(x => x.InvitationForDate >= startDate.Date && x.InvitationForDate < endDate.Date)
                .Include(x => x.TestingPersonnelConfirmations)
                .ThenInclude(x => x.TestingPersonnel)
                .Select(x => new 
                { 
                    InvitationDate = x.InvitationForDate,
                    Shift1 = new 
                    {
                        RequiredPersonnelCountShift = x.RequiredPersonnelCountShift1,
                        ShiftNumber = ShiftNumber.First,
                        ConfirmedEmployees = x.TestingPersonnelConfirmations.Where(conf => conf.ShiftNumber == ShiftNumber.First)
                        .Select(c => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(c.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(c.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(c.TestingPersonnel.Email)
                        })
                    },
                    Shift2 = new
                    {
                        RequiredPersonnelCountShift = x.RequiredPersonnelCountShift2,
                        ShiftNumber = ShiftNumber.Second,
                        ConfirmedEmployees = x.TestingPersonnelConfirmations.Where(conf => conf.ShiftNumber == ShiftNumber.Second)
                        .Select(c => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(c.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(c.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(c.TestingPersonnel.Email)
                        })                        
                    }
                })
                .ToListAsync()
                .ConfigureAwait(false);

            var fixedPersonnel = await context.TestingPersonnels
                .Where(x => x.HasFixedWorkingDays == true && x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == WorkingArea.Pooling))
                .ToListAsync()
                .ConfigureAwait(false);

            var testDatesValues = new List<TestsDataDto>();

            var currentDate = startDate;
            while (currentDate < endDate)
            {
                var invitation = invitations.FirstOrDefault(x => x.InvitationDate.Date == currentDate.Date);
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    List<TestingPersonnelTestDataDto> fixedPersonnelForWeekdayShift1 = GetFixedTestingPersonnelForWeekday(fixedPersonnel, currentDate.DayOfWeek, Shift.First);
                    List<TestingPersonnelTestDataDto> fixedPersonnelForWeekdayShift2 = GetFixedTestingPersonnelForWeekday(fixedPersonnel, currentDate.DayOfWeek, Shift.Second);
                    var testsDataValue = new TestsDataDto
                    {
                        Date = currentDate,
                        InvitationAlreadySent = invitation != null,
                        Shifts = new List<TestDataPerShiftDto>()
                        {
                            new TestDataPerShiftDto()
                            { 
                                ShiftNumber = invitation == null ? ShiftNumber.First : invitation.Shift1.ShiftNumber,
                                RequiredPersonnelCountShift = invitation == null ? 0 : invitation.Shift1.RequiredPersonnelCountShift,
                                ConfirmedEmployees = invitation == null ? new List<TestingPersonnelTestDataDto>() : invitation.Shift1.ConfirmedEmployees.DistinctBy(x => x.Email).ToList(),
                                FixedEmployees = fixedPersonnelForWeekdayShift1
                            },
                            new TestDataPerShiftDto()
                            {
                                ShiftNumber = invitation == null ? ShiftNumber.Second : invitation.Shift2.ShiftNumber,
                                RequiredPersonnelCountShift = invitation == null ? 0 : invitation.Shift2.RequiredPersonnelCountShift,
                                ConfirmedEmployees = invitation == null ? new List<TestingPersonnelTestDataDto>() : invitation.Shift2.ConfirmedEmployees.DistinctBy(x => x.Email).ToList(),
                                FixedEmployees = fixedPersonnelForWeekdayShift2
                            }
                        }
                    };
                    testDatesValues.Add(testsDataValue);

                    foreach (var organization in orgs)
                    {
                        if ((CheckIfInTestDate(organization.FirstDate, currentDate) ||
                            CheckIfInTestDate(organization.SecondDate, currentDate) ||
                            CheckIfInTestDate(organization.ThirdDate, currentDate) ||
                            CheckIfInTestDate(organization.FourthDate, currentDate) ||
                            CheckIfInTestDate(organization.FifthDate, currentDate)) &&
                            !CheckIfDateIsExcluded(organization.ExclusionStartDate, organization.ExclusionEndDate, currentDate))
                        {
                            var allTestingDates = new List<DateTime?>();
                            allTestingDates.Add(organization.FirstDate);
                            allTestingDates.Add(organization.SecondDate);
                            allTestingDates.Add(organization.ThirdDate);
                            allTestingDates.Add(organization.FourthDate);
                            allTestingDates.Add(organization.FifthDate);

                            var countOfTestingDates = allTestingDates.Where(x => x.HasValue).Count();
                            testsDataValue.Samples += organization.IsCompanyOrPharmacyOrNursingOrganization
                                ? organization.RegisteredEmployees / countOfTestingDates ?? 0
                                : organization.TotalSamples / countOfTestingDates;
                        }
                    }
                }

                currentDate = currentDate.AddDays(1);
            }

            return testDatesValues;
        }

        private List<TestingPersonnelTestDataDto> GetFixedTestingPersonnelForWeekday(List<TestingPersonnel> fixedPersonnel, DayOfWeek dayOfWeek, Shift shift)
        {
            var testingPersonnelForWeekday = new List<TestingPersonnelTestDataDto>();
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    testingPersonnelForWeekday = fixedPersonnel
                        .Where(x => x.MondayShift == shift || x.MondayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        { 
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email)
                        }).ToList();
                    break;
                case DayOfWeek.Tuesday:
                    testingPersonnelForWeekday = fixedPersonnel
                        .Where(x => x.TuesdayShift == shift || x.TuesdayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email)
                        }).ToList();
                    break;
                case DayOfWeek.Wednesday:
                    testingPersonnelForWeekday = fixedPersonnel
                        .Where(x => x.WednesdayShift == shift || x.WednesdayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email)
                        }).ToList();
                    break;
                case DayOfWeek.Thursday:
                    testingPersonnelForWeekday = fixedPersonnel
                        .Where(x => x.ThursdayShift == shift || x.ThursdayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email)
                        }).ToList();
                    break;
                case DayOfWeek.Friday:
                    testingPersonnelForWeekday = fixedPersonnel
                        .Where(x => x.FridayShift == shift || x.FridayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email)
                        }).ToList();
                    break;
                default:
                    break;
            }

            return testingPersonnelForWeekday;
        }

        private static bool CheckIfDateIsExcluded(DateTime? exclusionStartDate, DateTime? exclusionEndDate,
            DateTime testDate)
        {
            return exclusionStartDate.HasValue && exclusionEndDate.HasValue
                && testDate >= exclusionStartDate.Value.Date && testDate <= exclusionEndDate.Value.Date;
        }

        private static bool CheckIfInTestDate(DateTime? organizationDate, DateTime testDate)
        {
            return organizationDate.HasValue && organizationDate.Value.Date <= testDate
                && organizationDate.Value.DayOfWeek == testDate.DayOfWeek;
        }

        public async Task<List<TestingPersonnelInvitationReceiverDto>> GetTestingPersonnelInvitationReceiversByWorkingAreaAsync(WorkingArea workingArea, DayOfWeek dayOfWeek)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var testingPersonnelInvitationReceivers = await context.TestingPersonnels
                .Where(GetFilterByWorkingAreaAndDayOfWeek(workingArea, dayOfWeek))
                .Select(x => new TestingPersonnelInvitationReceiverDto
                {
                    TestingPersonnelId = x.Id,
                    Email = _aesCryptography.Decrypt(x.Email)
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return testingPersonnelInvitationReceivers;
        }

        private static Expression<Func<TestingPersonnel, bool>> GetFilterByWorkingAreaAndDayOfWeek(WorkingArea workingArea, DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Monday)
            {
                return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) && x.MondayShift == Shift.None;
            }
            else if (dayOfWeek == DayOfWeek.Tuesday)
            {
                return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) && x.TuesdayShift == Shift.None;
            }
            else if (dayOfWeek == DayOfWeek.Wednesday)
            {
                return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) && x.WednesdayShift == Shift.None;
            }
            else if (dayOfWeek == DayOfWeek.Thursday)
            {
                return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) && x.ThursdayShift == Shift.None;
            }
            else if (dayOfWeek == DayOfWeek.Friday)
            {
                return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) && x.FridayShift == Shift.None;
            }
            else
            {
                return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea);
            }
        }

        public async Task<bool> CheckTestingPersonnelEmailExistAsync(string testingPersonnelEmail, Guid testingPersonnelId)
        {
            var encryptedEmail = _aesCryptography.Encrypt(testingPersonnelEmail);

            await using var context = ContextFactory.CreateDataContext(null);
            var existingEmails = await context.TestingPersonnels
                .AnyAsync(item => item.Email == encryptedEmail && item.Id != testingPersonnelId)
                .ConfigureAwait(false);

            return existingEmails;
        }
    }
}
