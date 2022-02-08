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
using Elyon.Fastly.Api.Domain.Dtos;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class UtilRepository : BaseCrudRepository<Organization, OrganizationBasicDto>, IUtilRepository
    {
        private readonly IAESCryptography _aESCryptography;
        public UtilRepository(
            Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper, IAESCryptography aESCryptography)
            : base(contextFactory, mapper)
        {
            _aESCryptography = aESCryptography;
        }
        public async Task<List<OrganizationBasicDto>> GetOrganizationsAsync(int typeFilter)
        {
            var orgData = new List<OrganizationBasicDto>();
            await using var context = ContextFactory.CreateDataContext(null);
            var orgs = new List<Organization>();
            if (typeFilter > 0){
                if (typeFilter != 82002)
                {
                    orgs = await context.Organizations
                        .Where(item => item.OrganizationTypeId == typeFilter)
                        .Where(item => item.Status != OrganizationStatus.NotActive)
                        .Where(item => item.IsOnboardingEmailSent == true)
                        .Include(x => x.City)
                        .Include(x => x.OrganizationType)
                        .ToListAsync()
                        .ConfigureAwait(false);
                }
            }else{
                orgs = await context.Organizations
                    .Where(item => item.OrganizationTypeId != 82002)
                    .Where(item => item.Status != OrganizationStatus.NotActive)
                    .Where(item => item.IsOnboardingEmailSent == true)
                    .Include(x => x.City)
                    .Include(x => x.OrganizationType)
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            foreach (var org in orgs)
            {
                var orgDataDto = new OrganizationBasicDto();
                orgDataDto.Id = org.Id;
                if (org.OrganizationType != null)
                {
                    orgDataDto.Type = org.OrganizationType.Name;
                }
                if (org.City != null)
                {
                    orgDataDto.City = org.City.Name;
                }
                orgDataDto.TypeId = org.OrganizationTypeId;
                orgDataDto.Zip = org.Zip;
                orgDataDto.Name = org.Name;
                orgDataDto.ShortcutName = org.OrganizationShortcutName;
                orgDataDto.ReportingContact = org.ReportingContact;
                orgDataDto.ReportingEmail = org.ReportingEmail;
                orgData.Add(orgDataDto);
            }
            return orgData;
        }
        public async Task<OrganizationDetailDto> GetOrganizationByIdAsync(Guid id)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var org = await context.Organizations.AsNoTracking()
                .Include(x => x.Contacts)
                .Include(x => x.SupportPerson)
                .Include(x => x.OrganizationType)
                .Include(x => x.SubOrganizations)
                .Include(x => x.InfoSessionFollowUp)
                .Include(x => x.City)
                .FirstOrDefaultAsync(item => item.Id == id)
                .ConfigureAwait(false);
            var orgDataDto = new OrganizationDetailDto();
            if (org != null)
            {
                if (org.OrganizationTypeId != 82002)
                {
                    orgDataDto.Id = org.Id;
                    if (org.OrganizationType != null)
                    {
                        orgDataDto.Type = org.OrganizationType.Name;
                    }
                    if (org.City != null)
                    {
                        orgDataDto.City = org.City.Name;
                    }
                    orgDataDto.TypeId = org.OrganizationTypeId;
                    orgDataDto.Zip = org.Zip;
                    orgDataDto.Name = org.Name;
                    orgDataDto.ShortcutName = org.OrganizationShortcutName;
                    orgDataDto.ReportingContact = org.ReportingContact;
                    orgDataDto.ReportingEmail = org.ReportingEmail;

                    orgDataDto.Contacts = new List<UserDto>();
                    if (org.Contacts != null)
                    {
                        foreach (var contact in org.Contacts)
                        {
                            var userDto = Mapper.Map<User, UserDto>(contact);
                            orgDataDto.Contacts.Add(userDto);
                        }
                    }
                    orgDataDto.SubOrganizations = new List<SubOrganizationDto>();
                    if (org.SubOrganizations != null)
                    {
                        foreach (var subOrganization in org.SubOrganizations)
                        {
                            var subOrganizationDto = Mapper.Map<SubOrganization, SubOrganizationDto>(subOrganization);
                            orgDataDto.SubOrganizations.Add(subOrganizationDto);
                        }
                    }
                    if (org.SupportPerson != null)
                    {
                        var supportPersonDto = Mapper.Map<User, UserDto>(org.SupportPerson);
                        orgDataDto.SupportPerson = supportPersonDto;
                    }
                    if (org.InfoSessionFollowUp != null)
                    {
                        orgDataDto.FollowUpStatus = org.InfoSessionFollowUp.Status;
                    }
                    orgDataDto.EpaadId = org.EpaadId;
                    orgDataDto.CreatedOn = org.CreatedOn;
                    orgDataDto.LastUpdatedOn = org.LastUpdatedOn;
                    orgDataDto.Address = org.Address;
                    orgDataDto.TrainingTimestamp = org.TrainingTimestamp;
                    orgDataDto.OnboardingTimestamp = org.OnboardingTimestamp;
                    orgDataDto.FirstTestTimestamp = org.FirstTestTimestamp;
                    orgDataDto.SecondTestTimestamp = org.SecondTestTimestamp;
                    orgDataDto.ThirdTestTimestamp = org.ThirdTestTimestamp;
                    orgDataDto.FourthTestTimestamp = org.FourthTestTimestamp;
                    orgDataDto.FifthTestTimestamp = org.FifthTestTimestamp;
                    orgDataDto.ExclusionStartDate = org.ExclusionStartDate;
                    orgDataDto.ExclusionEndDate = org.ExclusionEndDate;
                    orgDataDto.NumberOfSamples = org.NumberOfSamples;
                    orgDataDto.NumberOfPools = org.NumberOfPools;
                    orgDataDto.SupportPersonId = org.SupportPersonId;
                    orgDataDto.Status = org.Status;
                    if (org.Manager != null)
                    {
                        orgDataDto.Manager = _aESCryptography.Decrypt(org.Manager);
                    }

                    orgDataDto.StudentsCount = org.StudentsCount;
                    orgDataDto.EmployeesCount = org.EmployeesCount;
                    orgDataDto.RegisteredEmployees = org.RegisteredEmployees;
                    orgDataDto.Area = org.Area;
                    orgDataDto.County = org.County;
                    orgDataDto.PrioLogistic = org.PrioLogistic;
                    orgDataDto.SchoolType = org.SchoolType;
                    orgDataDto.NumberOfBags = org.NumberOfBags;
                    orgDataDto.NaclLosing = org.NaclLosing;
                    orgDataDto.AdditionalTestTubes = org.AdditionalTestTubes;
                    orgDataDto.NumberOfRakoBoxes = org.NumberOfRakoBoxes;
                    orgDataDto.PickupLocation = org.PickupLocation;
                    orgDataDto.IsOnboardingEmailSent = org.IsOnboardingEmailSent;
                    orgDataDto.IsStaticPooling = org.IsStaticPooling;
                    orgDataDto.IsContractReceived = org.IsContractReceived;
                }
            }
            return orgDataDto;
        }
        public async Task<List<ExportUserDataDto>> ExportDataAsync()
        {
            var userData = new List<ExportUserDataDto>();
            await using var context = ContextFactory.CreateDataContext(null);
            var users = await context.Users
                .Where(item => item.OrganizationId != null)
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationType)
                .Include(x => x.Organization)
                .ThenInclude(x => x.City)
                .Include(x => x.Organization)
                .ThenInclude(x => x.SupportPerson)
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var user in users)
            {
                var userDataDto = new ExportUserDataDto();
                userDataDto.OrganizationId = user.OrganizationId;
                if (user.Organization != null)
                {
                    userDataDto.EpaadId = user.Organization.EpaadId;
                    userDataDto.OrganizationTypeId = user.Organization.OrganizationTypeId;
                    if (user.Organization.OrganizationType != null)
                    {
                        userDataDto.OrganizationTypeName = user.Organization.OrganizationType.Name;
                    }
                    userDataDto.OrganizationAddress = user.Organization.Address;
                    if (user.Organization.City != null)
                    {
                        userDataDto.OrganizationCity = user.Organization.City.Name;
                    }

                    if (user.Organization.SupportPerson != null)
                    {
                        userDataDto.OrganizationSupportPerson = _aESCryptography.Decrypt(user.Organization.SupportPerson.Email);

                    }
                    if (user.Organization.Manager != null)
                    {
                        userDataDto.OrganizationManager = _aESCryptography.Decrypt(user.Organization.Manager);
                    }

                    userDataDto.OrganizationZip = user.Organization.Zip;
                    userDataDto.OrganizationArea = user.Organization.Area;
                    userDataDto.OrganizationCounty = user.Organization.County;
                    userDataDto.OrganizationStatus = user.Organization.Status;
                    userDataDto.OrganizationSchoolType = user.Organization.SchoolType;
                    userDataDto.OrganizationStudentsCount = user.Organization.StudentsCount;
                    userDataDto.OrganizationShortcutName = user.Organization.OrganizationShortcutName;
                    userDataDto.IsOnboardingEmailSent = user.Organization.IsOnboardingEmailSent;
                    userDataDto.IsContractReceived = user.Organization.IsContractReceived;
                    userDataDto.IsStaticPooling = user.Organization.IsStaticPooling;
                    userDataDto.PickupLocation = user.Organization.PickupLocation;
                    if (user.Organization.OnboardingTimestamp != null)
                    {
                        userDataDto.OnboardingTimestamp = user.Organization.OnboardingTimestamp?.ToString(new CultureInfo("en-us"));
                    }
                    if (user.Organization.FirstTestTimestamp != null)
                    {
                        userDataDto.FirstTestTimestamp = user.Organization.FirstTestTimestamp?.ToString(new CultureInfo("en-us"));
                    }
                    if (user.Organization.SecondTestTimestamp != null)
                    {
                        userDataDto.SecondTestTimestamp = user.Organization.SecondTestTimestamp?.ToString(new CultureInfo("en-us"));
                    }
                    if (user.Organization.ThirdTestTimestamp != null)
                    {
                        userDataDto.ThirdTestTimestamp = user.Organization.ThirdTestTimestamp?.ToString(new CultureInfo("en-us"));
                    }
                    if (user.Organization.FourthTestTimestamp != null)
                    {
                        userDataDto.FourthTestTimestamp = user.Organization.FourthTestTimestamp?.ToString(new CultureInfo("en-us"));
                    }
                    if (user.Organization.FifthTestTimestamp != null)
                    {
                        userDataDto.FifthTestTimestamp = user.Organization.FifthTestTimestamp?.ToString(new CultureInfo("en-us"));
                    }

                    userDataDto.OrganizationName = user.Organization.Name;
                    if (user.Organization.CreatedOn != null)
                    {
                        userDataDto.OrganizationCreatedOn = user.Organization.CreatedOn.ToString(new CultureInfo("en-us"));
                    }
                    if (user.Organization.LastUpdatedOn != null)
                    {
                        userDataDto.OrganizationLastUpdatedOn = user.Organization.LastUpdatedOn.ToString(new CultureInfo("en-us"));
                    }
                    userDataDto.OrganizationNumberOfSamples = user.Organization.NumberOfSamples;
                    userDataDto.OrganizationNumberOfPolls = user.Organization.NumberOfPools;
                    userDataDto.OrganizationRegisteredEmployees = user.Organization.RegisteredEmployees;
                    var contacts = user.Organization.Contacts.ToList();
                    var count = 0;
                    contacts.ForEach(contact => {
                        count++;
                        if (count == 1)
                        {
                            userDataDto.OrganizationContact1Email = _aESCryptography.Decrypt(contact.Email);
                            userDataDto.OrganizationContact1Name = _aESCryptography.Decrypt(contact.Name);
                            userDataDto.OrganizationContact1PhoneNumber = _aESCryptography.Decrypt(contact.PhoneNumber);
                            userDataDto.OrganizationContact1LandLineNumber = _aESCryptography.Decrypt(contact.LandLineNumber);
                        }
                        if (count == 2)
                        {
                            userDataDto.OrganizationContact2Email = _aESCryptography.Decrypt(contact.Email);
                            userDataDto.OrganizationContact2Name = _aESCryptography.Decrypt(contact.Name);
                            userDataDto.OrganizationContact2PhoneNumber = _aESCryptography.Decrypt(contact.PhoneNumber);
                            userDataDto.OrganizationContact2LandLineNumber = _aESCryptography.Decrypt(contact.LandLineNumber);
                        }
                        if (count == 3)
                        {
                            userDataDto.OrganizationContact3Email = _aESCryptography.Decrypt(contact.Email);
                            userDataDto.OrganizationContact3Name = _aESCryptography.Decrypt(contact.Name);
                            userDataDto.OrganizationContact3PhoneNumber = _aESCryptography.Decrypt(contact.PhoneNumber);
                            userDataDto.OrganizationContact3LandLineNumber = _aESCryptography.Decrypt(contact.LandLineNumber);
                        }

                    });
                }
                userDataDto.UserId = user.Id;
                userDataDto.Email = _aESCryptography.Decrypt(user.Email);
                userDataDto.Name = _aESCryptography.Decrypt(user.Name);
                userDataDto.PhoneNumber = _aESCryptography.Decrypt(user.PhoneNumber);
                userDataDto.LandLineNumber = _aESCryptography.Decrypt(user.LandLineNumber);
                userDataDto.ReportingContact = user.Organization.ReportingContact;
                userDataDto.ReportingEmail = user.Organization.ReportingEmail;
                userData.Add(userDataDto);
            }
            return userData;
        }

    }
}
