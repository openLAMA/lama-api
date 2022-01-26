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

using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUps;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.DomainServices
{
    public class InfoSessionFollowUpService : BaseService, IInfoSessionFollowUpService
    {
        private readonly IInfoSessionFollowUpRepository _infoSessionFollowUpRepository;
        private readonly IOrganizationsRepository _organizationsRepository;
        private readonly IEmailSenderService _emailSenderService;
        private const int _companyOrganizationTypeId = 82000;
        private const int _smeOrganizationTypeId = 99990;
        private const int _campOrganizationTypeId = 82006;

        public InfoSessionFollowUpService(IInfoSessionFollowUpRepository infoSessionFollowUpRepository,
            IEmailSenderService emailSenderService,
            IOrganizationsRepository organizationsRepository)
        {
            _infoSessionFollowUpRepository = infoSessionFollowUpRepository;
            _organizationsRepository = organizationsRepository;
            _emailSenderService = emailSenderService;
        }

        public async Task SendInfoSessionFollowUpEmailAsync(InfoSessionFollowUpSpecDto specDto)
        {
            if (specDto == default)
                throw new ArgumentNullException(nameof(specDto));

            var organization = await _organizationsRepository
                .GetByIdAsync(specDto.OrganizationId).ConfigureAwait(false);

            if (organization.OrganizationTypeId == _smeOrganizationTypeId)
            {
                if (!ValidateOrganizationForOnboarding(organization, specDto.OrganizationContactPersonId))
                    return;

                await SendSMEOnboardingEmailAsync(specDto, organization).ConfigureAwait(false);
                await _organizationsRepository.UpdateIsOnboardingEmailSent(true, organization.Id).ConfigureAwait(false);
            }
            else if (organization.OrganizationTypeId == _companyOrganizationTypeId)
            {
                if (!ValidateOrganizationForOnboarding(organization, specDto.OrganizationContactPersonId))
                    return;

                await SendCompanyOnboardingEmailAsync(specDto, organization).ConfigureAwait(false);
                await _organizationsRepository.UpdateIsOnboardingEmailSent(true, organization.Id).ConfigureAwait(false);
            }
            else if (organization.OrganizationTypeId == _campOrganizationTypeId)
            {
                if (!ValidateOrganizationForOnboarding(organization, specDto.OrganizationContactPersonId))
                    return;

                await SendCampOnboardingEmailAsync(specDto, organization).ConfigureAwait(false);
                await _organizationsRepository.UpdateIsOnboardingEmailSent(true, organization.Id).ConfigureAwait(false);
            }
            else
            {
                if (string.IsNullOrEmpty(specDto.Message))
                {
                    ValidationDictionary.AddModelError("Message", "Message must not be null or empty space");
                    return;
                }

                if (!ValidateOrganizationForNewFollowUp(organization))
                    return;

                string generatedToken = await AddFollowUpAsync(organization).ConfigureAwait(false);

                await SendInfoSessionFollowUpEmailAsync(specDto, generatedToken).ConfigureAwait(false);
            }
        }

        private async Task SendInfoSessionFollowUpEmailAsync(InfoSessionFollowUpSpecDto specDto, string generatedToken)
        {
            foreach (var receiver in specDto.Receivers)
            {
                await _emailSenderService.SendInfoSessionFollowUpEmailAsync(receiver, specDto.Message, generatedToken)
                    .ConfigureAwait(false);
            }
        }

        private async Task SendCompanyOnboardingEmailAsync(InfoSessionFollowUpSpecDto specDto, OrganizationDto organization)
        {
            var receiversList = specDto.Receivers.ToList();
            for (int i = 0; i < receiversList.Count; i++)
            {
                var OrganizationShortcutName = organization.OrganizationShortcutName;
                if(OrganizationShortcutName == null){
                    OrganizationShortcutName = " ";
                }
                var parameters = new Dictionary<string, string>
                    {
                        { "CompanyShortcut", OrganizationShortcutName },
                        { "supportPerson", organization.SupportPerson.Name }
                    };

                await _emailSenderService.SendOnboardingEmailAsync(
                    receiversList[i], 
                    (receiversList.Count > 1 && i == 0) ? null : specDto.CcReceivers, 
                    organization.OrganizationTypeId, 
                    parameters)
                    .ConfigureAwait(false);
            }
        }

        private async Task SendSMEOnboardingEmailAsync(InfoSessionFollowUpSpecDto specDto, OrganizationDto organization)
        {
            foreach (var receiver in specDto.Receivers)
            {
                var OrganizationShortcutName = organization.OrganizationShortcutName;
                if(OrganizationShortcutName == null){
                    OrganizationShortcutName = " ";
                }
                var parameters = new Dictionary<string, string>
                    {
                        { "areaPharmacy", organization.Area },
                        { "CompanyShortcut", OrganizationShortcutName },
                        { "firstTestingDate", organization.FirstTestTimestamp.Value.ToString("d", CultureInfo.CreateSpecificCulture("de-CH")) },
                        { "TestingDay", organization.FirstTestTimestamp.Value.ToString("dddd", CultureInfo.CreateSpecificCulture("de-CH")) }
                    };

                await _emailSenderService.SendOnboardingEmailAsync(receiver, null, organization.OrganizationTypeId, parameters).ConfigureAwait(false);
            }
        }

        private async Task SendCampOnboardingEmailAsync(InfoSessionFollowUpSpecDto specDto, OrganizationDto organization)
        {
            var parameters = new Dictionary<string, string>
                {
                    { "numberOfTestParticipants", organization.NumberOfSamples.ToString(CultureInfo.InvariantCulture) },
                    { "supportPerson", organization.SupportPerson.Name },
                    { "OrgContactPersonName", organization.Contacts.First(c => c.Id == specDto.OrganizationContactPersonId).Name },
                    { "OrgContactPersonEmail", organization.Contacts.First(c => c.Id == specDto.OrganizationContactPersonId).Email }
                };

            var receiversList = specDto.Receivers.ToList();
            for (int i = 0; i < receiversList.Count; i++)
            {
                await _emailSenderService.SendOnboardingEmailAsync(
                    receiversList[i],
                    (receiversList.Count > 1 && i == 0) ? null : specDto.CcReceivers,
                    organization.OrganizationTypeId, 
                    parameters)
                    .ConfigureAwait(false);
            }
        }

        private bool ValidateOrganizationForOnboarding(OrganizationDto organization, Guid? organizationContactPersonId)
        {
            if (organization == null)
            {
                ValidationDictionary.AddModelError("Organization", "Organization with specified Id does not exist");
                return false;
            }

            var isValid = true;
            if (organization.OrganizationType.Id == _smeOrganizationTypeId)
            {
                if (!organization.FirstTestTimestamp.HasValue || organization.FirstTestTimestamp == default(DateTime))
                {
                    ValidationDictionary.AddModelError("First Test Date",
                        "First Test is required.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(organization.Area))
                {
                    ValidationDictionary.AddModelError("Organization Area",
                        "Organization Area is required.");
                    isValid = false;
                }
            }
            else if (organization.OrganizationType.Id == _companyOrganizationTypeId)
            {
                if (organization.SupportPerson == null)
                {
                    ValidationDictionary.AddModelError("Organization Support Person",
                        "Organization Support Person is required.");
                    isValid = false;
                }
            }
            else if (organization.OrganizationType.Id == _campOrganizationTypeId)
            {
                if (organization.SupportPerson == null)
                {
                    ValidationDictionary.AddModelError("Organization Support Person",
                        "Organization Support Person is required.");
                    isValid = false;
                }

                if (!organization.Contacts.Any(c => c.Id == organizationContactPersonId))
                {
                    ValidationDictionary.AddModelError("Organization Contact Person not found",
                        "Organization Contact Person not found.");
                    isValid = false;
                }
            }

            return isValid;
        }

        public async Task ChangeFollowUpStatusAsync(InfoSessionFollowUpResponseSpecDto specDto)
        {
            if (specDto == default)
                throw new ArgumentNullException(nameof(specDto));

            var currentStatus = await  _infoSessionFollowUpRepository.GetStatusByTokenAsync(specDto.Token)
                .ConfigureAwait(false);

            if (currentStatus == InfoSessionFollowUpStatus.Accepted || currentStatus == InfoSessionFollowUpStatus.Declined)
            {
                ValidationDictionary.AddModelError("FollowUp Status",
                    "Organization has already accepted or declined the follow up email");

                return;
            }

            await _infoSessionFollowUpRepository.UpdateStatusAsync(specDto.Token, specDto.IsAccepted ?
                    InfoSessionFollowUpStatus.Accepted :
                    InfoSessionFollowUpStatus.Declined).ConfigureAwait(false);
        }

        public async Task UpdateFollowUpStatusAsync(InfoSessionFollowUpUpdateSpecDto specDto)
        {
            if (specDto == default)
                throw new ArgumentNullException(nameof(specDto));
            
            var organization = await _organizationsRepository
                .GetByIdAsync(specDto.OrganizationId).ConfigureAwait(false);
            
            if (!ValidateOrganizationForFollowUpUpdate(organization, specDto.NewStatus))
                return;
            
            await _infoSessionFollowUpRepository.UpdateStatusAsync(specDto.OrganizationId, specDto.NewStatus)
                .ConfigureAwait(false);
        }

        private bool ValidateOrganizationForNewFollowUp(OrganizationDto organization)
        {
            if (organization == default)
            {
                ValidationDictionary.AddModelError("OrganizationId", "Organization with specified Id does not exist");

                return false;
            }

            if (organization.FollowUpStatus == InfoSessionFollowUpStatus.Accepted ||
                organization.FollowUpStatus == InfoSessionFollowUpStatus.Declined)
            {
                ValidationDictionary.AddModelError("FollowUp Status",
                    "Organization has already accepted or declined the follow up email");

                return false;
            }

            return true;
        }

        private bool ValidateOrganizationForFollowUpUpdate(OrganizationDto organization,
            InfoSessionFollowUpStatus newStatus)
        {
            if (organization == default)
            {
                ValidationDictionary.AddModelError("OrganizationId", "Organization with specified Id does not exist");

                return false;
            }
            
            if (organization.FollowUpStatus == newStatus)
            {
                ValidationDictionary.AddModelError("No change", "Organization already has specified status");

                return false;
            }

            if (organization.FollowUpStatus == InfoSessionFollowUpStatus.NotSent)
            {
                ValidationDictionary.AddModelError("Follow up", "First send follow up in order to update it.");

                return false;
            }

            return true;
        }
        
        private async Task<string> AddFollowUpAsync(OrganizationDto organization)
        {
            string generatedToken;
            if (organization.FollowUpStatus == InfoSessionFollowUpStatus.Sent)
            {
                generatedToken = await _infoSessionFollowUpRepository.GetTokenByOrganizationIdAsync(organization.Id)
                    .ConfigureAwait(false);
            }
            else
            {
                generatedToken = await _infoSessionFollowUpRepository
                   .AddInfoSessionFollowUpAsync(organization.Id).ConfigureAwait(false);
            }

            return generatedToken;
        }
    }
}
