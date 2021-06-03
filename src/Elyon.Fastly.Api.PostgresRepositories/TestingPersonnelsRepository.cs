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
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class TestingPersonnelsRepository : BaseCrudRepository<TestingPersonnel, TestingPersonnelDto>, ITestingPersonnelsRepository
    {
        private const int OneDayPeriod = 1;
        private const int DaysPeriod = 28;
        
        private readonly DateTime _testsHistoryEarliestDate = new DateTime(2021, 3, 1);
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

        public async Task<TestsDataWithIsEarliestDateDto> GetTestsDataDtoAsync(DateTime startDate, bool isForOneDate)
        {
            const int companyOrganizationId = 82000;
            const int pharmacyOrganizationId = 82001;
            const int nursingHomesOrganizationId = 82003;
            const int hospitalOrganizationId = 82004;
            const int smeOrganizationId = 99990;

            startDate = startDate.Date;
            var endDate = startDate.AddDays(DaysPeriod);
            if (isForOneDate)
            {
                endDate = startDate.AddDays(OneDayPeriod);
            }

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
                    InvitationId = x.Id,
                    InvitationDate = x.InvitationForDate,
                    Shift1 = new 
                    {
                        RequiredPersonnelCountShift = x.RequiredPersonnelCountShift1,
                        ShiftNumber = ShiftNumber.First,
                        ConfirmedNotCanceledEmployeesCount = x.TestingPersonnelConfirmations.Where(conf => conf.ShiftNumber == ShiftNumber.First && !conf.CanceledOn.HasValue).Count(),
                        ConfirmedEmployees = x.TestingPersonnelConfirmations.Where(conf => conf.ShiftNumber == ShiftNumber.First).OrderByDescending(conf => conf.CanceledOn)
                        .Select(c => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(c.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(c.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(c.TestingPersonnel.Email),
                            IsCanceled = c.CanceledOn.HasValue
                        })
                    },
                    Shift2 = new
                    {
                        RequiredPersonnelCountShift = x.RequiredPersonnelCountShift2,
                        ShiftNumber = ShiftNumber.Second,
                        ConfirmedNotCanceledEmployeesCount = x.TestingPersonnelConfirmations.Where(conf => conf.ShiftNumber == ShiftNumber.Second && !conf.CanceledOn.HasValue).Count(),
                        ConfirmedEmployees = x.TestingPersonnelConfirmations.Where(conf => conf.ShiftNumber == ShiftNumber.Second).OrderByDescending(conf => conf.CanceledOn)
                        .Select(c => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(c.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(c.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(c.TestingPersonnel.Email),
                            IsCanceled = c.CanceledOn.HasValue
                        })                        
                    }
                })
                .ToListAsync()
                .ConfigureAwait(false);

            var fixedPersonnel = await context.TestingPersonnels
                .Where(x => x.Type == TestingPersonnelType.Fixed && x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == WorkingArea.Pooling))
                .Include(x => x.FixedTestingPersonnelCancelations)
                .ToListAsync()
                .ConfigureAwait(false);

            var cantonsWithWeekdaysSamples = await context.Cantons
                .Where(c => c.CantonWeekdaysSamples != null)
                .Include(c => c.CantonWeekdaysSamples)
                .OrderBy(c => c.Name)
                .ToListAsync()
                .ConfigureAwait(false);

            var confirmationsWithoutInvitation = await context.TestingPersonnelConfirmationsWithoutInvitation
                .Where(c => c.Date >= startDate.Date && c.Date < endDate.Date)
                .Include(c => c.TestingPersonnel)
                .ToListAsync()
                .ConfigureAwait(false);

            var testDatesValues = new List<TestsDataDto>();

            var currentDate = startDate;
            while (currentDate < endDate)
            {
                var invitation = invitations.FirstOrDefault(x => x.InvitationDate.Date == currentDate.Date);
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    var testsDataValue = new TestsDataDto
                    {
                        InvitationId = invitation?.InvitationId,
                        Date = currentDate,
                        InvitationAlreadySent = invitation != null,
                        Shifts = new List<TestDataPerShiftDto>()
                        {
                            new TestDataPerShiftDto()
                            { 
                                ShiftNumber = invitation == null ? ShiftNumber.First : invitation.Shift1.ShiftNumber,
                                RequiredPersonnelCountShift = invitation == null ? 0 : invitation.Shift1.RequiredPersonnelCountShift,
                                ConfirmedNotCanceledEmployeesCount = invitation == null ? 0 : invitation.Shift1.ConfirmedNotCanceledEmployeesCount,
                                ConfirmedEmployees = invitation == null ? new List<TestingPersonnelTestDataDto>() : invitation.Shift1.ConfirmedEmployees.DistinctBy(x => x.Email).ToList(),
                                FixedEmployees = GetFixedTestingPersonnelForWeekday(fixedPersonnel, currentDate, Shift.First),
                                ConfirmedWithoutInvitation = GetConfirmedTestingPersonnelWithoutInvitationForWeekday(confirmationsWithoutInvitation, currentDate, ShiftNumber.First)
                            },
                            new TestDataPerShiftDto()
                            {
                                ShiftNumber = invitation == null ? ShiftNumber.Second : invitation.Shift2.ShiftNumber,
                                RequiredPersonnelCountShift = invitation == null ? 0 : invitation.Shift2.RequiredPersonnelCountShift,
                                ConfirmedNotCanceledEmployeesCount = invitation == null ? 0 : invitation.Shift2.ConfirmedNotCanceledEmployeesCount,
                                ConfirmedEmployees = invitation == null ? new List<TestingPersonnelTestDataDto>() : invitation.Shift2.ConfirmedEmployees.DistinctBy(x => x.Email).ToList(),
                                FixedEmployees = GetFixedTestingPersonnelForWeekday(fixedPersonnel, currentDate, Shift.Second),
                                ConfirmedWithoutInvitation = GetConfirmedTestingPersonnelWithoutInvitationForWeekday(confirmationsWithoutInvitation, currentDate, ShiftNumber.Second)
                            }
                        },
                        CantonsSamplesData = GetTestDataPerCantonAndWeekday(cantonsWithWeekdaysSamples, currentDate)
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

                    testsDataValue.CantonsSamples = testsDataValue.CantonsSamplesData.Sum(x => x.Samples);
                    testsDataValue.TotalSamples = testsDataValue.Samples + testsDataValue.CantonsSamples;
                }

                currentDate = currentDate.AddDays(1);
            }

            var testsDataWithEarliestDate = new TestsDataWithIsEarliestDateDto()
            { 
                IsEarliestDate = startDate == _testsHistoryEarliestDate,
                TestsData = testDatesValues
            };

            return testsDataWithEarliestDate;
        }

        private List<TestingPersonnelTestDataDto> GetConfirmedTestingPersonnelWithoutInvitationForWeekday(List<TestingPersonnelConfirmationsWithoutInvitation> confirmationsWithoutInvitation, DateTime date, ShiftNumber shift)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return confirmationsWithoutInvitation
                        .Where(x => x.Date.DayOfWeek == DayOfWeek.Monday && x.ShiftNumber == shift)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(x.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(x.TestingPersonnel.Email),
                            IsCanceled = false,
                            ConfirmationWithoutInvitationId = x.Id
                        }).ToList();
                case DayOfWeek.Tuesday:
                    return confirmationsWithoutInvitation
                        .Where(x => x.Date.DayOfWeek == DayOfWeek.Tuesday && x.ShiftNumber == shift)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(x.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(x.TestingPersonnel.Email),
                            IsCanceled = false,
                            ConfirmationWithoutInvitationId = x.Id
                        }).ToList();
                case DayOfWeek.Wednesday:
                    return confirmationsWithoutInvitation
                        .Where(x => x.Date.DayOfWeek == DayOfWeek.Wednesday && x.ShiftNumber == shift)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(x.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(x.TestingPersonnel.Email),
                            IsCanceled = false,
                            ConfirmationWithoutInvitationId = x.Id
                        }).ToList();
                case DayOfWeek.Thursday:
                    return confirmationsWithoutInvitation
                        .Where(x => x.Date.DayOfWeek == DayOfWeek.Thursday && x.ShiftNumber == shift)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(x.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(x.TestingPersonnel.Email),
                            IsCanceled = false,
                            ConfirmationWithoutInvitationId = x.Id
                        }).ToList();
                case DayOfWeek.Friday:
                    return confirmationsWithoutInvitation
                        .Where(x => x.Date.DayOfWeek == DayOfWeek.Friday && x.ShiftNumber == shift)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.TestingPersonnel.FirstName),
                            LastName = _aesCryptography.Decrypt(x.TestingPersonnel.LastName),
                            Email = _aesCryptography.Decrypt(x.TestingPersonnel.Email),
                            IsCanceled = false,
                            ConfirmationWithoutInvitationId = x.Id
                        }).ToList();
                default:
                    return new List<TestingPersonnelTestDataDto>();
            }
        }

        private static List<TestDataPerCantonDto> GetTestDataPerCantonAndWeekday(List<Canton> cantonsWithWeekdaysSamples, DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return cantonsWithWeekdaysSamples
                        .Select(x => new TestDataPerCantonDto
                        {
                            CantonId = x.Id,
                            CantonName = x.Name,
                            CantonShortName = x.ShortName,
                            Samples = x.CantonWeekdaysSamples.MondaySamples
                        }).ToList();
                case DayOfWeek.Tuesday:
                    return cantonsWithWeekdaysSamples
                        .Select(x => new TestDataPerCantonDto
                        {
                            CantonId = x.Id,
                            CantonName = x.Name,
                            CantonShortName = x.ShortName,
                            Samples = x.CantonWeekdaysSamples.TuesdaySamples
                        }).ToList();
                case DayOfWeek.Wednesday:
                    return cantonsWithWeekdaysSamples
                        .Select(x => new TestDataPerCantonDto
                        {
                            CantonId = x.Id,
                            CantonName = x.Name,
                            CantonShortName = x.ShortName,
                            Samples = x.CantonWeekdaysSamples.WednesdaySamples
                        }).ToList();
                case DayOfWeek.Thursday:
                    return cantonsWithWeekdaysSamples
                        .Select(x => new TestDataPerCantonDto
                        {
                            CantonId = x.Id,
                            CantonName = x.Name,
                            CantonShortName = x.ShortName,
                            Samples = x.CantonWeekdaysSamples.ThursdaySamples
                        }).ToList();
                case DayOfWeek.Friday:
                    return cantonsWithWeekdaysSamples
                        .Select(x => new TestDataPerCantonDto
                        {
                            CantonId = x.Id,
                            CantonName = x.Name,
                            CantonShortName = x.ShortName,
                            Samples = x.CantonWeekdaysSamples.FridaySamples
                        }).ToList();
                default:
                    return new List<TestDataPerCantonDto>();
            }
        }

        private List<TestingPersonnelTestDataDto> GetFixedTestingPersonnelForWeekday(List<TestingPersonnel> fixedPersonnel, DateTime date, Shift shift)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return fixedPersonnel
                        .Where(x => x.MondayShift == shift || x.MondayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        { 
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email),
                            IsCanceled = x.FixedTestingPersonnelCancelations.Any(c => c.CanceledDate == date.Date)
                        }).ToList();
                case DayOfWeek.Tuesday:
                    return fixedPersonnel
                        .Where(x => x.TuesdayShift == shift || x.TuesdayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email),
                            IsCanceled = x.FixedTestingPersonnelCancelations.Any(c => c.CanceledDate == date.Date)
                        }).ToList();
                case DayOfWeek.Wednesday:
                    return fixedPersonnel
                        .Where(x => x.WednesdayShift == shift || x.WednesdayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email),
                            IsCanceled = x.FixedTestingPersonnelCancelations.Any(c => c.CanceledDate == date.Date)
                        }).ToList();
                case DayOfWeek.Thursday:
                    return fixedPersonnel
                        .Where(x => x.ThursdayShift == shift || x.ThursdayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email),
                            IsCanceled = x.FixedTestingPersonnelCancelations.Any(c => c.CanceledDate == date.Date)
                        }).ToList();
                case DayOfWeek.Friday:
                    return fixedPersonnel
                        .Where(x => x.FridayShift == shift || x.FridayShift == Shift.FullDay)
                        .Select(x => new TestingPersonnelTestDataDto
                        {
                            FirstName = _aesCryptography.Decrypt(x.FirstName),
                            LastName = _aesCryptography.Decrypt(x.LastName),
                            Email = _aesCryptography.Decrypt(x.Email),
                            IsCanceled = x.FixedTestingPersonnelCancelations.Any(c => c.CanceledDate == date.Date)
                        }).ToList();
                default:
                    return new List<TestingPersonnelTestDataDto>();
            }
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
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) && 
                        (x.Type == TestingPersonnelType.Normal || (x.Type == TestingPersonnelType.Fixed && x.MondayShift == Shift.None));
                case DayOfWeek.Tuesday:
                    return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) &&
                        (x.Type == TestingPersonnelType.Normal || (x.Type == TestingPersonnelType.Fixed && x.TuesdayShift == Shift.None));
                case DayOfWeek.Wednesday:
                    return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) &&
                        (x.Type == TestingPersonnelType.Normal || (x.Type == TestingPersonnelType.Fixed && x.WednesdayShift == Shift.None));
                case DayOfWeek.Thursday:
                    return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) &&
                        (x.Type == TestingPersonnelType.Normal || (x.Type == TestingPersonnelType.Fixed && x.ThursdayShift == Shift.None));
                case DayOfWeek.Friday:
                    return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) &&
                        (x.Type == TestingPersonnelType.Normal || (x.Type == TestingPersonnelType.Fixed && x.FridayShift == Shift.None));
                default:
                    return x => x.TestingPersonnelWorkingAreas.Any(wa => wa.Area == workingArea) &&
                        (x.Type == TestingPersonnelType.Normal || x.Type == TestingPersonnelType.Fixed);
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

        public async Task<Guid> GetTestingPersonnelIdByEmailAsync(string testingPersonnelEmail)
        {
            if (testingPersonnelEmail == null)
                throw new ArgumentNullException(nameof(testingPersonnelEmail));

#pragma warning disable CA1308 // Normalize strings to uppercase
            var encryptedEmail = _aesCryptography.Encrypt(testingPersonnelEmail.ToLower(CultureInfo.InvariantCulture));
#pragma warning restore CA1308 // Normalize strings to uppercase
            await using var context = ContextFactory.CreateDataContext(null);

            return await context.TestingPersonnels
                .Where(item => item.Email == encryptedEmail)
                .Select(tp => tp.Id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<Guid> GetTestingPersonnelIdByEmailAndTypeAsync(string testingPersonnelEmail, TestingPersonnelType type)
        {
            if (testingPersonnelEmail == null)
                throw new ArgumentNullException(nameof(testingPersonnelEmail));

#pragma warning disable CA1308 // Normalize strings to uppercase
            var encryptedEmail = _aesCryptography.Encrypt(testingPersonnelEmail.ToLower(CultureInfo.InvariantCulture));
#pragma warning restore CA1308 // Normalize strings to uppercase
            await using var context = ContextFactory.CreateDataContext(null);

            return await context.TestingPersonnels
                .Where(item => item.Email == encryptedEmail && item.Type == type)
                .Select(tp => tp.Id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<List<AvailableTemporaryPersonnelDto>> GetAvailableTemporaryPersonnelForDateAndShiftAsync(DateTime testingDate, ShiftNumber shiftNumber)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            return await context.TestingPersonnels
                .Where(item => item.Type == TestingPersonnelType.Temporary && !item.TestingPersonnelConfirmationsWithoutInvitations.Any(
                    x => x.Date.Date == testingDate.Date && x.ShiftNumber == shiftNumber))
                .Select(tp => new AvailableTemporaryPersonnelDto() 
                {
                    TestingPersonnelId = tp.Id,
                    Name = $"{_aesCryptography.Decrypt(tp.FirstName)} {_aesCryptography.Decrypt(tp.LastName)}"
                })
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
