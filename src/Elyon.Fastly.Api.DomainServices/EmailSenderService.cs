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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.DomainServices.Helpers;
using Elyon.Fastly.EmailJob.RestClient;
using Elyon.Fastly.EmailJob.RestClient.Models;
using UrlHelper = Elyon.Fastly.Api.Helpers.UrlHelper;

namespace Elyon.Fastly.Api.DomainServices
{
    public class EmailSenderService : BaseService, IEmailSenderService
    {
        private readonly string _baseFrontendUrl;
        private const string LoginConfirmationUrl = "/login-confirmation?token={token}";
        private const string RegisterConfirmationUrl = "/register-confirmation?token={token}";
        private const string PoolingAssignmentConfirmationUrl = "/poolingassignment-confirmation?token={token}&shifts={shift}";
        private const string InfoSessionFollowUpConfirmationUrl = "/follow-up-email-confirmation?token={token}&accepted={isAccepted}";
        private const int _companyOrganizationTypeId = 82000;
        private const int _smeOrganizationTypeId = 99990;
        private const int _campOrganizationTypeId = 82006;

        private readonly IEmailJobClient _emailClient;

        public EmailSenderService(IEmailJobClient emailClient, string baseFrontendUrl)
        {
            _emailClient = emailClient;
            _baseFrontendUrl = baseFrontendUrl;
        }

        public async Task SendLoginConfirmationAsync(string receiver, string confirmationToken)
        {
            await _emailClient.EmailsApi.SendEmailAsync(new EmailSpecModel
            {
                Receiver = receiver,
                TemplateName = "LoginConfirmation",
                Parameters = new Dictionary<string, string>
                {
                    { 
                        "EmailVerificationLink", 
                            ConstructUrl(LoginConfirmationUrl, new[] { ("token", confirmationToken) })
                    }
                }
            }).ConfigureAwait(false);
        }

        public async Task SendRegisterConfirmationAsync(string receiver, string confirmationToken)
        {
            await _emailClient.EmailsApi.SendEmailAsync(new EmailSpecModel
            {
                Receiver = receiver,
                TemplateName = "RegistrationConfirmation",
                Parameters = new Dictionary<string, string>
                {
                    {
                        "EmailVerificationLink",
                            ConstructUrl(RegisterConfirmationUrl, new[] { ("token", confirmationToken) })
                    }
                }
            }).ConfigureAwait(false);
        }

        public async Task SendInvitationForPoolingAssignmentAsync(string receiver, 
            string confirmationToken, DateTime poolingDate)
        {
            await _emailClient.EmailsApi.SendEmailAsync(new EmailSpecModel
            {
                Receiver = receiver,
                TemplateName = "InvitationForPoolingAssignment",
                Parameters = new Dictionary<string, string>
                {
                    {
                        "MorningRegistrationLink",
                        ConstructUrl(PoolingAssignmentConfirmationUrl, new[]
                        {
                            ("token", confirmationToken),
                            ("shift", "1")
                        })
                    },
                    {
                        "AfternoonRegistrationLink",
                        ConstructUrl(PoolingAssignmentConfirmationUrl, new[]
                        {
                            ("token", confirmationToken),
                            ("shift", "2")
                        })
                    },
                    {
                        "WholeDayRegistrationLink",
                        ConstructUrl(PoolingAssignmentConfirmationUrl, new[]
                        {
                            ("token", confirmationToken),
                            ("shift", "1,2")
                        })
                    },
                    {
                        "date", poolingDate.ToString("d", CultureInfo.CreateSpecificCulture("de-CH"))
                    }
                }
            }).ConfigureAwait(false);
        }

        public async Task SendConfirmationForPoolingAssignmentAsync(string receiver, 
            DateTime poolingDate, ICollection<int> shifts)
        {
            if (shifts == null)
                throw new ArgumentNullException(nameof(shifts));

            string shiftsText = string.Empty;
            if (shifts.Contains(1) && shifts.Contains(2))
                shiftsText = "Ganztags";
            else if (shifts.Contains(1))
                shiftsText = "Vormittag";
            else if (shifts.Contains(2))
                shiftsText = "Nachmittag";

            await _emailClient.EmailsApi.SendEmailAsync(new EmailSpecModel
            {
                Receiver = receiver,
                TemplateName = "RegistrationConfirmationForPoolingAssignment",
                Parameters = new Dictionary<string, string>
                {
                    {
                        "date", poolingDate.ToString("d", CultureInfo.CreateSpecificCulture("de-CH"))
                    },
                    {
                        "shifts", shiftsText
                    }
                }
            }).ConfigureAwait(false);
        }

        public async Task SendInfoSessionFollowUpEmailAsync(string receiver, string messageContent, string confirmationToken)
        {
            await _emailClient.EmailsApi.SendEmailAsync(new EmailSpecModel
            {
                Receiver = receiver,
                TemplateName = "InfoSessionFollowUp",
                Parameters = new Dictionary<string, string>
                {
                    { "Content", messageContent },
                    { 
                        "FollowUpAcceptLink",
                        ConstructUrl(InfoSessionFollowUpConfirmationUrl, 
                            new[] { ("token", confirmationToken), ("isAccepted", "true") })
                    },
                    {
                        "FollowUpDeclineLink",
                        ConstructUrl(InfoSessionFollowUpConfirmationUrl, 
                                new[] { ("token", confirmationToken), ("isAccepted", "false") })
                    }
                }
            }).ConfigureAwait(false);
        }

        public async Task SendOnboardingEmailAsync(string receiver, IEnumerable<string> ccReceivers, int organizationTypeId, Dictionary<string, string> parameters)
        {
            string templateName = string.Empty;
            List<string> attachmentsFilesHashes = new List<string>();
            if (organizationTypeId == _companyOrganizationTypeId)
            {
                templateName = "CompanyOnboarding";
                attachmentsFilesHashes = EmailAttachments.GetCompanyOnboardingAttachmentHashes();
            }
            else if (organizationTypeId == _smeOrganizationTypeId)
            {
                templateName = "SMEOnboarding";
                attachmentsFilesHashes = EmailAttachments.GetSMEOnboardingAttachmentHashes();
            }
            else if (organizationTypeId == _campOrganizationTypeId)
            {
                templateName = "CampOnboarding";
                attachmentsFilesHashes = EmailAttachments.GetCampsOnboardingAttachmentHashes();
            }

            await _emailClient.EmailsApi.SendEmailAsync(new EmailSpecModel
            {
                Receiver = receiver,
                CcReceivers = ccReceivers?.ToList(),
                TemplateName = templateName,
                AttachmentFilesHashes = attachmentsFilesHashes,
                Parameters = parameters
            }).ConfigureAwait(false);
        }

        private string ConstructUrl(string relativeUrl, IEnumerable<(string, string)> paramValues)
        {
            var url = UrlHelper.UrlCombine(_baseFrontendUrl, relativeUrl);

            foreach (var param in paramValues)
                url = url.Replace($"{{{param.Item1}}}", param.Item2, 
                    StringComparison.InvariantCultureIgnoreCase);

            return url;
        }

        public async Task SendEmailForEpaadAsync(string receiver, Dictionary<string, string> parameters)
        {
            await _emailClient.EmailsApi.SendEmailAsync(new EmailSpecModel
            {
                Receiver = receiver,
                TemplateName = "EmailForEpaad",
                Parameters = parameters
            }).ConfigureAwait(false);
        }
    }
}