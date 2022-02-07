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

using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prime.Sdk.Logging;
using System.Linq.Expressions;
using Elyon.Fastly.Api.Domain;
using System.Globalization;

namespace Elyon.Fastly.Api.DomainServices
{
    public class OrganizationsService : BaseCrudService<OrganizationDto>, IOrganizationsService
    {
        private const string DefaultEpaadOrganizationState = "BL";
        private const int companyOrganizationTypeId = 82000;
        private const int smeOrganizationTypeId = 99990;
        private const string smeOrgTypePoolFirstName = "KMU Baselland";
        private const int campOrganizationTypeId = 82006;
        private const string zeroNumberOfPools = "0";

        private readonly ISupportPersonOrgTypeDefaultRepository _supportPersonOrgTypeDefaultRepository;
        private readonly IOrganizationsRepository _organizationsRepository;
        private readonly IOrganizationTypesRepository _organizationTypesRepository;
        private readonly ICitiesRepository _citiesRepository;
        private readonly IAuthorizeService _authorizeService;
        private readonly IUsersService _usersService;
        private readonly IEpaadService _epaadService;
        private readonly IEmailSenderService _mailSender;
        private readonly ILog _log;

        private readonly Dictionary<int, string> organizationTypes = new Dictionary<int, string>()
        {
            { 82000, "Unternehmen" },
            { 82001, "Apotheke" },
            { 82002, "Schule" },
            { 82003, "Altersheim" },
            { 82004, "Spital" },
            { 82006, "Lager" },
            { 99990, "KMU" }
        };

        public OrganizationsService(IOrganizationsRepository organizationsRepository,
            ISupportPersonOrgTypeDefaultRepository supportPersonOrgTypeDefaultRepository, IOrganizationTypesRepository organizationTypesRepository,
            ICitiesRepository citiesRepository, IAuthorizeService authorizeService, IUsersService usersService, IEpaadService epaadService,
            IEmailSenderService mailSender, ILogFactory logFactory)
            : base(organizationsRepository)
        {
            if (logFactory == null)
                throw new ArgumentNullException(nameof(logFactory));

            _supportPersonOrgTypeDefaultRepository = supportPersonOrgTypeDefaultRepository;
            _organizationsRepository = organizationsRepository;
            _organizationTypesRepository = organizationTypesRepository;
            _citiesRepository = citiesRepository;
            _authorizeService = authorizeService;
            _usersService = usersService;
            _epaadService = epaadService;
            _mailSender = mailSender;
            _log = logFactory.CreateLog(this);
        }

        public async Task<OrganizationDto> CreateOrganizationAsync(OrganizationSpecDto specDto)
        {
            if (specDto == null)
            {
                return null;
            }
                  
            _log.Info($"Creating new organization \"{specDto.Name}\"...");

            var supportPerson = await _supportPersonOrgTypeDefaultRepository
                .GetSupportPersonByOrganizationTypeAsync(specDto.TypeId)
                .ConfigureAwait(false);
            specDto.SupportPersonId = supportPerson.SupportPersonId;

            var contactEmails = specDto.Contacts.Select(u => u.Email);
            var existingEmails = await GetExistingContactEmailsAsync(contactEmails, default)
                .ConfigureAwait(false);

            if (existingEmails.Any())
            {
                ValidationDictionary.AddModelError("Already Existing Emails", string.Join(", ", existingEmails));

                return null;
            }

            var createdDto = await AddAsync<OrganizationSpecDto>(specDto)
                .ConfigureAwait(false);

            return createdDto;
        }        

        public async Task<ShortContactInfoDto> GetSupportPersonByOrganizationTypeAsync(int organizationTypeId)
        {
            _log.Info($"Getting info for organization with id {organizationTypeId}...");

            return await _supportPersonOrgTypeDefaultRepository
                .GetSupportPersonByOrganizationTypeAsync(organizationTypeId)
                .ConfigureAwait(false);
        }

        private async Task<List<string>> GetExistingContactEmailsAsync(IEnumerable<string> emails, Guid organizationId)
        {
            return await _usersService
                .GetExistingContactEmailsAsync(emails, organizationId)
                .ConfigureAwait(false);
        }

        public async Task<OrganizationDashboardDto> GetOrganizationDashboardInfoAsync(Guid id)
        {
            return await _organizationsRepository
                .GetOrganizationDashboardInfoAsync(id)
                .ConfigureAwait(false);
        }

        public async Task<OrganizationProfileDto> GetOrganizationProfileAsync(Guid id)
        {
            return await _organizationsRepository
                .GetOrganizationProfileAsync(id)
                .ConfigureAwait(false);
        }

        public async Task UpdateOrganizationProfileAsync(OrganizationProfileSpecDto dto, Guid userId)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var contactEmails = dto.Contacts.Select(u => u.Email);
            var contactUserIds = dto.Contacts.Select(u => u.Id);
            var existingEmails = await GetExistingContactEmailsAsync(contactEmails, dto.Id)
                .ConfigureAwait(false);

            if (existingEmails.Any())
            {
                ValidationDictionary.AddModelError("Already Existing Emails", string.Join(", ", existingEmails));

                return;
            }

            var isLoggedUserPartOfOrganization = await _organizationsRepository
               .IsUserPartOfOrganizationAsync(dto.Id, userId)
               .ConfigureAwait(false);

            if (isLoggedUserPartOfOrganization && !contactUserIds.Contains(userId))
            {
                ValidationDictionary
                    .AddModelError("Delete yourself", "You cannot delete yourself from the contact list of the organization");

                return;
            }
            
            await _organizationsRepository
                .UpdateOrganizationProfileAsync(dto)
                .ConfigureAwait(false);

            var orgStatusCalculationDto = await _organizationsRepository
                .GetOrganizationStatusCalculationDataAsync(dto.Id)
                .ConfigureAwait(false);

            var orgStatus = CalculateOrganizationStatus(orgStatusCalculationDto, true);

            await _organizationsRepository
                .UpdateOrganizationStatusAsync(dto.Id, orgStatus)
                .ConfigureAwait(false);
        }

        public async Task<PagedResults<OrganizationDto>> GetAllOrganizationsAsync(
            Paginator paginator,
            Expression<Func<OrganizationDto, bool>> dtoFilter,
            string orderBy,
            bool isAscending)
        {
            return await _organizationsRepository
                .GetAllOrganizationsAsync(paginator, dtoFilter, orderBy, isAscending)
                .ConfigureAwait(false);
        }

        public async Task UpdateOrganizationAsync(OrganizationDto dto, Guid loggedUserId)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var organizationInDb = await _organizationsRepository
                .GetByIdAsync(dto.Id)
                .ConfigureAwait(false);

            if (organizationInDb.RegisteredEmployees > 0 && organizationInDb.OrganizationShortcutName != dto.OrganizationShortcutName)
            {
                ValidationDictionary.AddModelError("Organization shortcut name cannot be changed",
                    "Organization shortcut name cannot be changed when registered employees are greater than 0.");

                return;
            }

            if ((dto.ExclusionStartDate.HasValue && !dto.ExclusionEndDate.HasValue) ||
                (!dto.ExclusionStartDate.HasValue && dto.ExclusionEndDate.HasValue))
            {
                ValidationDictionary.AddModelError("Both exclusion dates must have value",
                    "One of the exclusion dates has value and the other does not.");

                return;
            }    

            var existinOrganizationType = await _organizationTypesRepository
                .AnyOrganizationTypeAsync(x => x.Id == dto.OrganizationTypeId)
                .ConfigureAwait(false);

            if (!existinOrganizationType)
            {
                ValidationDictionary
                    .AddModelError("Organization type not found", $"Organization type with that id doesn't exist.");

                return;
            }

            var contactEmails = dto.Contacts.Select(u => u.Email);
            var contactUserIds = dto.Contacts.Select(u => u.Id);
            var existingEmails = await GetExistingContactEmailsAsync(contactEmails, dto.Id)
                .ConfigureAwait(false);

            if (existingEmails.Any())
            {
                ValidationDictionary.AddModelError("Already Existing Emails", string.Join(", ", existingEmails));

                return;
            }

            var isLoggedUserPartOfOrganization = await _organizationsRepository
                .IsUserPartOfOrganizationAsync(dto.Id, loggedUserId)
                .ConfigureAwait(false);

            if (isLoggedUserPartOfOrganization && !contactUserIds.Contains(loggedUserId))
            {
                ValidationDictionary
                    .AddModelError("Delete yourself", "You cannot delete yourself from the contact list of the organization");

                return;
            }

            dto.LastUpdatedOn = DateTime.UtcNow;

            await UpdateAsync(dto).ConfigureAwait(false);

            var orgStatusCalculationDto = await _organizationsRepository
                .GetOrganizationStatusCalculationDataAsync(dto.Id)
                .ConfigureAwait(false);

            dto.Status = CalculateOrganizationStatus(orgStatusCalculationDto, true);

            await _organizationsRepository
                .UpdateOrganizationStatusAsync(dto.Id, dto.Status)
                .ConfigureAwait(false);
            
            /*var organizationEpaadId = await _organizationsRepository
                .GetOrganizationEpaadIdAsync(dto.Id)
                .ConfigureAwait(false);

            if (organizationEpaadId.HasValue)
            {
                await UpdateOrganizationinEpaadAsync(organizationEpaadId.Value, dto)
                    .ConfigureAwait(false);
            }*/
        }

        public async Task<List<ShortContactInfoDto>> GetSupportPeopleByOrganizationTypeAsync(int organizationTypeId)
        {
            return await _supportPersonOrgTypeDefaultRepository
                .GetSupportPeopleByOrganizationTypeAsync(organizationTypeId)
                .ConfigureAwait(false);
        }

        public async Task ChangeOrganizationActiveStatusAsync(OrganizationActiveStatusDto dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var orgStatusCalculationDto = await _organizationsRepository
               .GetOrganizationStatusCalculationDataAsync(dto.Id)
               .ConfigureAwait(false);

            dto.Status = !dto.IsActive ? OrganizationStatus.NotActive : CalculateOrganizationStatus(orgStatusCalculationDto, false);

            await _organizationsRepository
                .ChangeOrganizationActiveStatusAsync(dto)
                .ConfigureAwait(false);
        }

        private static OrganizationStatus CalculateOrganizationStatus(OrganizationStatusCalculationDto dto, bool isUpdateAction)
        {
            if (((isUpdateAction && dto.Status != OrganizationStatus.NotActive) ||
                (!isUpdateAction && dto.Status == OrganizationStatus.NotActive)) &&
                (dto.FirstTestTimestamp.HasValue || dto.SecondTestTimestamp.HasValue ||
                dto.ThirdTestTimestamp.HasValue || dto.FourthTestTimestamp.HasValue || 
                dto.FifthTestTimestamp.HasValue))
            {
                return OrganizationStatus.Onboarded;
            }
            else if (((isUpdateAction && dto.Status != OrganizationStatus.NotActive) ||
                (!isUpdateAction && dto.Status == OrganizationStatus.NotActive)) &&
                dto.OnboardingTimestamp.HasValue && dto.Status != OrganizationStatus.Onboarded)
            {
                return OrganizationStatus.PendingOnboarding;
            }
            else if (((isUpdateAction && dto.Status != OrganizationStatus.NotActive) ||
                (!isUpdateAction && dto.Status == OrganizationStatus.NotActive)) &&
                dto.TrainingTimestamp.HasValue && dto.Status != OrganizationStatus.Onboarded && dto.Status != OrganizationStatus.PendingOnboarding)
            {
                return OrganizationStatus.TrainingDateSet;
            }

            return dto.Status;
        }

        public async Task PushOrganizationToEPaadAsync(OrganizationDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.OrganizationTypeId == campOrganizationTypeId)
            {
                ValidationDictionary
                    .AddModelError("Camps can not be pushed to Epaad", $"Camps can not be pushed to Epaad.");
                return;
            }

            var existinOrganizationType = await _organizationTypesRepository
               .AnyOrganizationTypeAsync(x => x.Id == dto.OrganizationTypeId)
               .ConfigureAwait(false);

            if (!existinOrganizationType)
            {
                ValidationDictionary
                    .AddModelError("Organization type not found", $"Organization type with that id doesn't exist.");

                return;
            }

            var city = await _citiesRepository
                .GetCityEpaadDtoAsync(dto.CityId)
                .ConfigureAwait(false);

            var organizationCreationDate = await _organizationsRepository
               .GetOrganizationCreationDateAsync(dto.Id)
               .ConfigureAwait(false);

            var epaadOrgDto = new PushEpaadOrganizationDto
            {
                ContactPersonEmail = dto.Contacts.First().Email,
                ContactPersonName = dto.Contacts.First().Name,
                ContactPersonPhone = dto.Contacts.First().PhoneNumber,
                OrganizationTypeId = dto.OrganizationTypeId == smeOrganizationTypeId ? companyOrganizationTypeId : dto.OrganizationTypeId,
                OrganizationName = dto.Name,
                City = city.Name,
                CountryShortName = city.CountryShortName,
                Zip = dto.Zip ?? city.ZipCode,
                FilterText = dto.OrganizationShortcutName,
                State = DefaultEpaadOrganizationState,
                ActiveSince = organizationCreationDate,
                Address = dto.Address,
                PoolLastname = dto.Name,
                PoolFirstName = dto.OrganizationTypeId == smeOrganizationTypeId ? smeOrgTypePoolFirstName : default
            };

            var response = await _epaadService
                .PushOrganizationToEpaadAsync(epaadOrgDto)
                .ConfigureAwait(false);

            if(response == null || !response.EpaadId.HasValue)
            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                throw new ArgumentNullException(paramName: "Epaad response");
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
            }
            
            await _organizationsRepository
                .UpdateEpaadIdAsync(response.EpaadId.Value, dto.Id)
                .ConfigureAwait(false);
        }

        private async Task UpdateOrganizationinEpaadAsync(int organizationEpaadId, OrganizationDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var city = await _citiesRepository
                .GetCityEpaadDtoAsync(dto.CityId)
                .ConfigureAwait(false);

            var organizationCreationDate = await _organizationsRepository
               .GetOrganizationCreationDateAsync(dto.Id)
               .ConfigureAwait(false);

            var epaadOrgDto = new PushEpaadOrganizationUpdateDto
            {
                ContactPersonEmail = dto.Contacts.First().Email,
                ContactPersonName = dto.Contacts.First().Name,
                ContactPersonPhone = dto.Contacts.First().PhoneNumber,
                OrganizationTypeId = dto.OrganizationTypeId == smeOrganizationTypeId ? companyOrganizationTypeId : dto.OrganizationTypeId,
                OrganizationName = dto.Name,
                City = city.Name,
                CountryShortName = city.CountryShortName,
                Zip = dto.Zip ?? city.ZipCode,
                FilterText = dto.OrganizationShortcutName,
                State = DefaultEpaadOrganizationState,
                ActiveSince = organizationCreationDate,
                Address = dto.Address,
                PoolFirstName = dto.OrganizationTypeId == smeOrganizationTypeId ? smeOrgTypePoolFirstName : default
            };

            await _epaadService
                .UpdateOrganizationInEpaadAsync(organizationEpaadId, epaadOrgDto)
                .ConfigureAwait(false);
        }

        public async Task SetIsStaticPoolingAsync(OrganizationIsStaticPoolingDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            await _organizationsRepository
                .SetIsStaticPoolingAsync(dto)
                .ConfigureAwait(false);
        }

        public async Task SetIsContractReceivedAsync(Guid organizationId)
        {
            await _organizationsRepository
                .SetIsContractReceivedAsync(organizationId)
                .ConfigureAwait(false);
        }

        public async Task SendEmailForEpaadAsync(OrganizationSendEmailForEpaadDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var organizationDto = await _organizationsRepository
                .GetByIdAsync(dto.OrganizationId)
                .ConfigureAwait(false);

            if (organizationDto == null)
            {
                ValidationDictionary
                    .AddModelError("Organization not found", $"Organization not found.");
                return;
            }

            if (organizationDto.OrganizationTypeId != campOrganizationTypeId && !organizationDto.IsStaticPooling)
            {
                ValidationDictionary
                    .AddModelError("Organization type is not a Camp or organization is not with Static Pooling", $"Organization type is not a Camp or organization is not with Static Pooling.");
                return;
            }

            var city = await _citiesRepository
                .GetCityEpaadDtoAsync(organizationDto.CityId)
                .ConfigureAwait(false);

            var parameters = new Dictionary<string, string>
                {
                    { "organizationName", organizationDto.Name },
                    { "country", city.CountryShortName },
                    { "canton", DefaultEpaadOrganizationState },
                    { "zip", organizationDto.Zip ?? city.ZipCode },
                    { "city", city.Name },
                    { "street", organizationDto.Address },
                    { "email", string.Join(", ", organizationDto.Contacts.Select(c => c.Email)) },
                    { "numberOfSamples", organizationDto.NumberOfSamples.ToString(CultureInfo.InvariantCulture) },
                    { "numberOfPools", organizationDto.NumberOfPools != null ? organizationDto.NumberOfPools.ToString() : zeroNumberOfPools },
                    { "activeSince", organizationDto.CreatedOn.ToString("d", CultureInfo.CreateSpecificCulture("de-CH")) },
                    { "organizationType", organizationTypes[organizationDto.OrganizationType.Id] }
                };

            foreach (var receiver in dto.Receivers)
            {
                await _mailSender.SendEmailForEpaadAsync(receiver, parameters).ConfigureAwait(false);
            }
        }
    }
}
