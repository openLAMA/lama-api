using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUp;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using Prime.Sdk.Logging;
using System;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.DomainServices
{
    public class InfoSessionFollowUpService : BaseService, IInfoSessionFollowUpService
    {
        private readonly ILog _log;
        private readonly IInfoSessionFollowUpRepository _infoSessionFollowUpRepository;
        private readonly IOrganizationsRepository _organizationsRepository;
        private readonly IEmailSenderService _emailSenderService;

        public InfoSessionFollowUpService(ILogFactory logFactory, 
            IInfoSessionFollowUpRepository infoSessionFollowUpRepository,
            IEmailSenderService emailSenderService,
            IOrganizationsRepository organizationsRepository)
        {
            if (logFactory == null)
                throw new ArgumentNullException(nameof(logFactory));

            _infoSessionFollowUpRepository = infoSessionFollowUpRepository;
            _organizationsRepository = organizationsRepository;
            _emailSenderService = emailSenderService;
            _log = logFactory.CreateLog(this);
        }

        public async Task SendInfoSessionFollowUpEmailAsync(InfoSessionFollowUpSpecDto specDto)
        {
            if (specDto == default)
                throw new ArgumentNullException(nameof(specDto));

            var organization = await _organizationsRepository
                .GetByIdAsync(specDto.OrganizationId).ConfigureAwait(false);

            if (!ValidateOrganizationForNewFollowUp(organization))
                return;

            string generatedToken = await AddFollowUpAsync(organization).ConfigureAwait(false);

            foreach (var receiver in specDto.Recievers)
            {
                await _emailSenderService.SendInfoSessionFollowUpEmail(receiver, specDto.Message, generatedToken)
                    .ConfigureAwait(false);
            }
        }

        public async Task ChangeFollowUpStatusAsync(InfoSessionFollowUpUpdateSpecDto specDto)
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
