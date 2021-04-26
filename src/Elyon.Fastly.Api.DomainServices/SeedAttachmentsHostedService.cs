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
using System.Threading;
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.DomainServices.Helpers;
using Elyon.Fastly.EmailJob.RestClient;
using Elyon.Fastly.EmailJob.RestClient.Models;
using Microsoft.Extensions.Hosting;
using Prime.Sdk.Logging;

namespace Elyon.Fastly.Api.DomainServices
{
    public class SeedAttachmentsHostedService : BackgroundService
    {
        private readonly IAttachmentsSeedService _attachmentsSeedService;
        private readonly IEmailJobClient _emailClient;
        private readonly ILog _log;

        public SeedAttachmentsHostedService(IAttachmentsSeedService attachmentsSeedService, IEmailJobClient emailClient, ILogFactory logFactory)
        {
            if (logFactory == null)
                throw new ArgumentNullException(nameof(logFactory));

            _attachmentsSeedService = attachmentsSeedService;
            _emailClient = emailClient;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var attachmentsSeedDto = await _attachmentsSeedService.GetFirstAsync().ConfigureAwait(false);

            if (!attachmentsSeedDto.IsSeeded)
            {
                try
                {
                    _log.Info($"Seed attachments files");
                    var attachmentsToSeed = EmailAttachments.GetCompanyOnboardingAttachments();
                    foreach (var attachment in attachmentsToSeed)
                    {
                        try
                        {
                            await _emailClient.StorageApi
                            .AddFileAsync(new FileSpecModel
                            {
                                FileName = attachment.FileName,
                                Content = Convert.ToBase64String(attachment.GetContent())
                            })
                            .ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            _log.Error(ex, $"Seed failed for file name: {attachment.FileName}. Exception message: {ex.Message}");
                            throw;
                        }
                    }

                    attachmentsSeedDto.IsSeeded = true;
                    attachmentsSeedDto.SeededOn = DateTime.UtcNow;
                    await _attachmentsSeedService.UpdateAsync(attachmentsSeedDto).ConfigureAwait(false);
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
                {
                    _log.Error(ex, $"Attachments seed failed. Exception message: {ex.Message}");
                }
            }
        }
    }
}
