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
    }
}
