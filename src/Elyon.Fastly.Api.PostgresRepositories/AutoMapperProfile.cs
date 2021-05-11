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
using Elyon.Fastly.Api.Domain.Dtos.Cities;
using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUps;
using Elyon.Fastly.Api.Domain.Dtos.LamaCompanies;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using System.Globalization;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class AutoMapperProfile : Profile
    {
        private readonly IAESCryptography _aESCryptography;

#pragma warning disable CA1308 // Normalize strings to uppercase
        public AutoMapperProfile(IAESCryptography aESCryptography)
        {
            _aESCryptography = aESCryptography;

            CreateMap<User, UserDto>()
                .ForMember(opt => opt.Email,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.Email)))
                .ForMember(opt => opt.Name,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.Name)))
                .ForMember(opt => opt.PhoneNumber,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.PhoneNumber)))
                .ForMember(opt => opt.LandLineNumber,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.LandLineNumber)));
            CreateMap<UserDto, User>()
                .ForMember(opt => opt.Email,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Email.ToLower(CultureInfo.InvariantCulture))))
                .ForMember(opt => opt.Name,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Name)))
                .ForMember(opt => opt.PhoneNumber,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.PhoneNumber)))
                .ForMember(opt => opt.LandLineNumber,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.LandLineNumber)))
                .ForMember(opt => opt.LamaCompany, src => src.Ignore())
                .ForMember(opt => opt.LamaCompanyId, src => src.Ignore())
                .ForMember(opt => opt.Organization, src => src.Ignore())
                .ForMember(opt => opt.OrganizationId, src => src.Ignore())
                .ForMember(opt => opt.SupportOrganizations, src => src.Ignore())
                .ForMember(opt => opt.SupportPersonOrgTypeDefaults, src => src.Ignore())
                .ForMember(opt => opt.OrganizationNotes, src => src.Ignore());

            CreateMap<UserSpecDto, User>()
                .ForMember(opt => opt.Email,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Email.ToLower(CultureInfo.InvariantCulture))))
                .ForMember(opt => opt.Name,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Name)))
                .ForMember(opt => opt.PhoneNumber,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.PhoneNumber)))
                .ForMember(opt => opt.LandLineNumber,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.LandLineNumber)))
                .ForMember(opt => opt.LamaCompany, src => src.Ignore())
                .ForMember(opt => opt.LamaCompanyId, src => src.Ignore())
                .ForMember(opt => opt.Organization, src => src.Ignore())
                .ForMember(opt => opt.OrganizationId, src => src.Ignore())
                .ForMember(opt => opt.SupportOrganizations, src => src.Ignore())
                .ForMember(opt => opt.SupportPersonOrgTypeDefaults, src => src.Ignore())
                .ForMember(opt => opt.Id, src => src.Ignore())
                .ForMember(opt => opt.OrganizationNotes, src => src.Ignore());

            CreateMap<User, ShortContactInfoDto>()
                .ForMember(opt => opt.Name,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.Name)))
                .ForMember(opt => opt.SupportPersonId,
                    src => src.MapFrom(x => x.Id));

            CreateMap<City, CityDto>();

            CreateMap<SubOrganization, SubOrganizationDto>();
            CreateMap<SubOrganizationDto, SubOrganization>()
                .ForMember(opt => opt.Organization, src => src.Ignore());

            CreateMap<OrganizationSpecDto, Organization>()
                .ForMember(opt => opt.OrganizationTypeId,
                    src => src.MapFrom(x => x.TypeId))
                .ForMember(opt => opt.Id, src => src.Ignore())
                .ForMember(opt => opt.OrganizationType, src => src.Ignore())
                .ForMember(opt => opt.EpaadId, src => src.Ignore())
                .ForMember(opt => opt.OrganizationShortcutName, src => src.Ignore())
                .ForMember(opt => opt.CreatedOn, src => src.Ignore())
                .ForMember(opt => opt.LastUpdatedOn, src => src.Ignore())
                .ForMember(opt => opt.OnboardingTimestamp, src => src.Ignore())
                .ForMember(opt => opt.TrainingTimestamp, src => src.Ignore())
                .ForMember(opt => opt.FirstTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.SecondTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.ThirdTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.FourthTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.FifthTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.ExclusionStartDate, src => src.Ignore())
                .ForMember(opt => opt.ExclusionEndDate, src => src.Ignore())
                .ForMember(opt => opt.City, src => src.Ignore())
                .ForMember(opt => opt.SupportPerson, src => src.Ignore())
                .ForMember(opt => opt.Status, src => src.Ignore())
                .ForMember(opt => opt.Manager, src => src.Ignore())
                .ForMember(opt => opt.StudentsCount, src => src.Ignore())
                .ForMember(opt => opt.EmployeesCount, src => src.Ignore())
                .ForMember(opt => opt.RegisteredEmployees, src => src.Ignore())
                .ForMember(opt => opt.Area, src => src.Ignore())
                .ForMember(opt => opt.County, src => src.Ignore())
                .ForMember(opt => opt.PrioLogistic, src => src.Ignore())
                .ForMember(opt => opt.SchoolType, src => src.Ignore())
                .ForMember(opt => opt.NumberOfBags, src => src.Ignore())
                .ForMember(opt => opt.NaclLosing, src => src.Ignore())
                .ForMember(opt => opt.AdditionalTestTubes, src => src.Ignore())
                .ForMember(opt => opt.NumberOfRakoBoxes, src => src.Ignore())
                .ForMember(opt => opt.PickupLocation, src => src.Ignore())
                .ForMember(opt => opt.County, src => src.Ignore())
                .ForMember(opt => opt.SubOrganizations, src => src.Ignore())
                .ForMember(opt => opt.Notes, src => src.Ignore())
                .ForMember(opt => opt.InfoSessionFollowUp, src => src.Ignore())
                .ForMember(opt => opt.IsOnboardingEmailSent, src => src.Ignore())
                .ForMember(opt => opt.PlannedNumberOfSamples, src => src.Ignore());

            CreateMap<OrganizationProfileSpecDto, Organization>()
                .ForMember(opt => opt.OrganizationTypeId,
                    src => src.MapFrom(x => x.TypeId))
                .ForMember(opt => opt.OrganizationType, src => src.Ignore())
                .ForMember(opt => opt.EpaadId, src => src.Ignore())
                .ForMember(opt => opt.OrganizationShortcutName, src => src.Ignore())
                .ForMember(opt => opt.CreatedOn, src => src.Ignore())
                .ForMember(opt => opt.LastUpdatedOn, src => src.Ignore())
                .ForMember(opt => opt.OnboardingTimestamp, src => src.Ignore())
                .ForMember(opt => opt.TrainingTimestamp, src => src.Ignore())
                .ForMember(opt => opt.FirstTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.SecondTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.ThirdTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.FourthTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.FifthTestTimestamp, src => src.Ignore())
                .ForMember(opt => opt.ExclusionStartDate, src => src.Ignore())
                .ForMember(opt => opt.ExclusionEndDate, src => src.Ignore())
                .ForMember(opt => opt.City, src => src.Ignore())
                .ForMember(opt => opt.Status, src => src.Ignore())
                .ForMember(opt => opt.SupportPerson, src => src.Ignore())
                .ForMember(opt => opt.Manager, src => src.Ignore())
                .ForMember(opt => opt.StudentsCount, src => src.Ignore())
                .ForMember(opt => opt.EmployeesCount, src => src.Ignore())
                .ForMember(opt => opt.RegisteredEmployees, src => src.Ignore())
                .ForMember(opt => opt.Area, src => src.Ignore())
                .ForMember(opt => opt.County, src => src.Ignore())
                .ForMember(opt => opt.PrioLogistic, src => src.Ignore())
                .ForMember(opt => opt.SchoolType, src => src.Ignore())
                .ForMember(opt => opt.NumberOfBags, src => src.Ignore())
                .ForMember(opt => opt.NaclLosing, src => src.Ignore())
                .ForMember(opt => opt.AdditionalTestTubes, src => src.Ignore())
                .ForMember(opt => opt.NumberOfRakoBoxes, src => src.Ignore())
                .ForMember(opt => opt.PickupLocation, src => src.Ignore())
                .ForMember(opt => opt.County, src => src.Ignore())
                .ForMember(opt => opt.SubOrganizations, src => src.Ignore())
                .ForMember(opt => opt.Notes, src => src.Ignore())
                .ForMember(opt => opt.InfoSessionFollowUp, src => src.Ignore())
                .ForMember(opt => opt.IsOnboardingEmailSent, src => src.Ignore())
                .ForMember(opt => opt.PlannedNumberOfSamples, src => src.Ignore());


            CreateMap<OrganizationDto, Organization>()
                .ForMember(opt => opt.Manager,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Manager)))
                .ForMember(opt => opt.City, src => src.Ignore())
                .ForMember(opt => opt.CreatedOn, src => src.Ignore())
                .ForMember(opt => opt.LastUpdatedOn, src => src.Ignore())
                .ForMember(opt => opt.EpaadId, src => src.Ignore())
                .ForMember(opt => opt.Status, src => src.Ignore())
                .ForMember(opt => opt.RegisteredEmployees, src => src.Ignore())
                .ForMember(opt => opt.OrganizationType, src => src.Ignore())
                .ForMember(opt => opt.Notes, src => src.Ignore())
                .ForMember(opt => opt.InfoSessionFollowUp, src => src.Ignore())
                .ForMember(opt => opt.IsOnboardingEmailSent, src => src.Ignore())
                .ForMember(opt => opt.PlannedNumberOfSamples, src => src.Ignore());

            CreateMap<Organization, OrganizationDto>()
                .ForMember(opt => opt.Manager,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.Manager)))
                .ForMember(opt => opt.FollowUpStatus, 
                    src => src.MapFrom(x => x.InfoSessionFollowUp != null ? 
                        x.InfoSessionFollowUp.Status :
                        InfoSessionFollowUpStatus.NotSent));

            CreateMap<Organization, OrganizationProfileDto>();
            CreateMap<Organization, OrganizationDashboardDto>()
                .ForMember(opt => opt.TestDate1,
                    src => src.MapFrom(x => x.FirstTestTimestamp))
                .ForMember(opt => opt.TestDate2,
                    src => src.MapFrom(x => x.SecondTestTimestamp))
                .ForMember(opt => opt.TestDate3,
                    src => src.MapFrom(x => x.ThirdTestTimestamp))
                .ForMember(opt => opt.TestDate4,
                    src => src.MapFrom(x => x.FourthTestTimestamp))
                .ForMember(opt => opt.TestDate5,
                    src => src.MapFrom(x => x.FifthTestTimestamp))
                .ForMember(opt => opt.OnboardingDate,
                    src => src.MapFrom(x => x.OnboardingTimestamp));

            CreateMap<Organization, OrganizationEpaadInfoDto>();

            CreateMap<OrganizationType, OrganizationTypeDto>();

            CreateMap<TestingPersonnel, TestingPersonnelDto>()
                .ForMember(opt => opt.Email,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.Email)))
                .ForMember(opt => opt.FirstName,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.FirstName)))
                .ForMember(opt => opt.LastName,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.LastName)))
                .ForMember(opt => opt.WorkingAreas,
                    src => src.MapFrom(x => x.TestingPersonnelWorkingAreas));

            CreateMap<TestingPersonnelSpecDto, TestingPersonnel>()
                .ForMember(opt => opt.Email,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Email.ToLower(CultureInfo.InvariantCulture))))
                .ForMember(opt => opt.FirstName,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.FirstName)))
                .ForMember(opt => opt.LastName,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.LastName)))
                .ForMember(opt => opt.CreatedOn, src => src.Ignore())
                .ForMember(opt => opt.LastUpdatedOn, src => src.Ignore())
                .ForMember(opt => opt.Id, src => src.Ignore())
                .ForMember(opt => opt.Status, src => src.Ignore())
                .ForMember(opt => opt.TestingPersonnelConfirmations, src => src.Ignore())
                .ForMember(opt => opt.TestingPersonnelWorkingAreas,
                    src => src.MapFrom(x => x.WorkingAreas));

            CreateMap<TestingPersonnelWorkingAreaSpecDto, TestingPersonnelWorkingArea>()
                .ForMember(opt => opt.Area,
                    src => src.MapFrom(x => x.WorkingArea))
                .ForMember(opt => opt.Id, src => src.Ignore())
                .ForMember(opt => opt.TestingPersonnelId, src => src.Ignore())
                .ForMember(opt => opt.TestingPersonnel, src => src.Ignore());

            CreateMap<TestingPersonnelWorkingArea, TestingPersonnelWorkingAreaDto>();

            CreateMap<TestingPersonnelInvitationSpecDto, TestingPersonnelInvitation>()
                .ForMember(opt => opt.InvitationForDate,
                    src => src.MapFrom(x => x.Date))
                .ForMember(opt => opt.Id, src => src.Ignore())
                .ForMember(opt => opt.CreatedOn, src => src.Ignore())
                .ForMember(opt => opt.TestingPersonnelConfirmations, src => src.Ignore())
                .ForMember(opt => opt.SentByUser, src => src.Ignore());

            CreateMap<TestingPersonnelInvitation, TestingPersonnelInvitationDto>();
            CreateMap<TestingPersonnelConfirmationSpecDto, TestingPersonnelConfirmation>()
                .ForMember(opt => opt.TestingPersonnelInvitationId,
                    src => src.MapFrom(x => x.InvitationId))
                 .ForMember(opt => opt.TestingPersonnelId,
                    src => src.MapFrom(x => x.PersonnelId))
                .ForMember(opt => opt.Id, src => src.Ignore())
                .ForMember(opt => opt.ShiftNumber, src => src.Ignore())
                .ForMember(opt => opt.AcceptedOn, src => src.Ignore())
                .ForMember(opt => opt.TestingPersonnelInvitation, src => src.Ignore())
                .ForMember(opt => opt.TestingPersonnel, src => src.Ignore())
                .ForMember(opt => opt.CanceledByUserId, src => src.Ignore())
                .ForMember(opt => opt.CanceledOn, src => src.Ignore());

            CreateMap<LamaCompany, LamaCompanyProfileDto>();
            CreateMap<User, LamaUserDto>()
                .ForMember(opt => opt.Email,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.Email)))
                .ForMember(opt => opt.Name,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.Name)))
                .ForMember(opt => opt.PhoneNumber,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.PhoneNumber)))
                .ForMember(opt => opt.LandLineNumber,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.LandLineNumber)))
                .ForMember(opt => opt.SupportDefaultOrganizationTypes,
                    src => src.MapFrom(x => x.SupportPersonOrgTypeDefaults));

            CreateMap<SupportPersonOrgTypeDefaultMapping, SupportOrganizationTypeDto>()
                .ForMember(opt => opt.OrganizationTypeId,
                    src => src.MapFrom(x => x.OrganizationTypeId))
                .ForMember(opt => opt.OrganizationTypeName,
                    src => src.MapFrom(x => x.OrganizationType.Name))
                .ForMember(opt => opt.Id,
                    src => src.MapFrom(x => x.Id));
            CreateMap<SupportOrganizationTypeDto, SupportPersonOrgTypeDefaultMapping>()
               .ForMember(opt => opt.Id,
                   src => src.MapFrom(x => x.Id))
               .ForMember(opt => opt.OrganizationTypeId,
                   src => src.MapFrom(x => x.OrganizationTypeId))
               .ForMember(opt => opt.User,
                   src => src.Ignore())
                .ForMember(opt => opt.OrganizationType,
                   src => src.Ignore())
                .ForMember(opt => opt.OrganizationType,
                   src => src.Ignore())
               .ForMember(opt => opt.UserId,
                   src => src.Ignore());

            CreateMap<LamaCompanyProfileSpecDto, LamaCompany>()
                .ForMember(opt => opt.RoleType,
                    src => src.Ignore());

            CreateMap<LamaUserDto, User>()
                .ForMember(opt => opt.Email,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Email.ToLower(CultureInfo.InvariantCulture))))
                .ForMember(opt => opt.Name,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Name)))
                .ForMember(opt => opt.PhoneNumber,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.PhoneNumber)))
                .ForMember(opt => opt.LandLineNumber,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.LandLineNumber)))
                .ForMember(opt => opt.SupportPersonOrgTypeDefaults,
                    src => src.MapFrom(x => x.SupportDefaultOrganizationTypes))
                .ForMember(opt => opt.LamaCompany,
                    src => src.Ignore())
                .ForMember(opt => opt.LamaCompanyId,
                    src => src.Ignore())
                .ForMember(opt => opt.Organization,
                    src => src.Ignore())
                .ForMember(opt => opt.OrganizationId,
                    src => src.Ignore())
                .ForMember(opt => opt.SupportOrganizations,
                    src => src.Ignore())
                .ForMember(opt => opt.OrganizationNotes,
                    src => src.Ignore());

            CreateMap<OrganizationNoteDto, OrganizationNote>()
                .ForMember(opt => opt.CreatorName,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.CreatorName)))
                .ForMember(opt => opt.Text,
                    src => src.MapFrom(x => _aESCryptography.Encrypt(x.Text)))
                .ForMember(opt => opt.Organization,
                    src => src.Ignore())
                .ForMember(opt => opt.User,
                    src => src.Ignore());

            CreateMap<OrganizationNote, OrganizationNoteDto>()
                .ForMember(opt => opt.CreatorName,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.CreatorName)))
                .ForMember(opt => opt.Text,
                    src => src.MapFrom(x => _aESCryptography.Decrypt(x.Text)));
        }
#pragma warning restore CA1308 // Normalize strings to uppercase
    }
}
