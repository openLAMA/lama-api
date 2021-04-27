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
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.DomainServices.Helpers;
using Elyon.Fastly.EmailJob.RestClient;
using Elyon.Fastly.EmailJob.RestClient.Models;
using Microsoft.AspNetCore.Builder;
using Prime.Sdk.Logging;

namespace Elyon.Fastly.Api.DomainServices
{
    public static class SeedAttachmentsAppBuilderExtension
    {
        public static async Task SeedAttachments(this IApplicationBuilder appBuilder)
        {
            if (appBuilder == null)
                throw new ArgumentNullException(nameof(appBuilder));

            var logFactory = (ILogFactory)appBuilder.ApplicationServices.GetService(typeof(ILogFactory));
            var emailClient = (IEmailJobClient)appBuilder.ApplicationServices.GetService(typeof(IEmailJobClient));
            var attachmentsSeedService = (IAttachmentsSeedService)appBuilder.ApplicationServices.GetService(typeof(IAttachmentsSeedService));

            var log = logFactory.CreateLog(new SeedAttachmentsAppbuilder());
            var attachmentsSeedDto = await attachmentsSeedService.GetFirstAsync().ConfigureAwait(false);

            if (!attachmentsSeedDto.IsSeeded)
            {
                try
                {
                    log.Info($"Seed attachments files");
                    var attachmentsToSeed = EmailAttachments.GetCompanyOnboardingAttachments();
                    foreach (var attachment in attachmentsToSeed)
                    {
                        try
                        {
                            await emailClient.StorageApi
                            .AddFileAsync(new FileSpecModel
                            {
                                FileName = attachment.FileName,
                                Content = Convert.ToBase64String(attachment.GetContent())
                            })
                            .ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex, $"Seed failed for file name: {attachment.FileName}. Exception message: {ex.Message}");
                            throw;
                        }
                    }

                    attachmentsSeedDto.IsSeeded = true;
                    attachmentsSeedDto.SeededOn = DateTime.UtcNow;
                    await attachmentsSeedService.UpdateAsync(attachmentsSeedDto).ConfigureAwait(false);
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
                {
                    log.Error(ex, $"Attachments seed failed. Exception message: {ex.Message}");
                }
            }
        }
    }

    public class SeedAttachmentsAppbuilder
    { 
    }
}
