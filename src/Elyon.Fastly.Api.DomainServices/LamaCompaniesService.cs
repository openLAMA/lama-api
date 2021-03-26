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

using Elyon.Fastly.Api.Domain.Dtos.LamaCompanies;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.DomainServices
{
    public class LamaCompaniesService : BaseCrudService<LamaCompanyDto>, ILamaCompaniesService
    {
        private readonly ILamaCompaniesRepository _lamaCompaniesRepository;
        private readonly IUsersService _usersService;

        public LamaCompaniesService(ILamaCompaniesRepository lamaCompaniesRepository, IUsersService usersService)
            : base(lamaCompaniesRepository)
        {
            _lamaCompaniesRepository = lamaCompaniesRepository;
            _usersService = usersService;
        }

        public async Task<LamaCompanyProfileDto> GetLamaCompanyProfileAsync(Guid lamaCompanyId)
        {
            return await _lamaCompaniesRepository
                .GetLamaCompanyProfileAsync(lamaCompanyId)
                .ConfigureAwait(false);
        }

        public async Task UpdateLamaCompanyProfileAsync(LamaCompanyProfileSpecDto lamaCompanyProfileDto, Guid loggedUserId)
        {
            if (lamaCompanyProfileDto == null)
            {
                throw new ArgumentNullException(nameof(lamaCompanyProfileDto));
            }

            var userEmails = lamaCompanyProfileDto.Users.Select(u => u.Email);
            var userIds = lamaCompanyProfileDto.Users.Select(u => u.Id);
            var existingEmails = await GetExistingContactEmailsAsync(userEmails, lamaCompanyProfileDto.Id)
                .ConfigureAwait(false);

            if (existingEmails.Any())
            {
                ValidationDictionary
                    .AddModelError("Existing emails", $"Email(s) {string.Join(", ", existingEmails)} already exist");

                return;
            }

            if (!userIds.Contains(loggedUserId))
            {
                ValidationDictionary
                    .AddModelError("Delete yourself", "You cannot delete yourself from the contact list of the organization");

                return;
            }

            foreach(var lamaUser in lamaCompanyProfileDto.Users)
            {
                if(lamaUser.SupportDefaultOrganizationTypes != null)
                {
                    var organizationTypeIds = lamaUser.SupportDefaultOrganizationTypes.Select(x => x.OrganizationTypeId);
                    if (!CheckForDuplicates(organizationTypeIds))
                    {
                        ValidationDictionary
                            .AddModelError($"Duplicate organization types for user {lamaUser.Id}",
                            "You cannot set duplicate organization types");
                    }

                }                
            }

            if (!ValidationDictionary.IsValid())
            {
                return;
            }

            await _lamaCompaniesRepository
                .UpdateLamaCompanyProfileAsync(lamaCompanyProfileDto)
                .ConfigureAwait(false);
        }

        private async Task<List<string>> GetExistingContactEmailsAsync(IEnumerable<string> emails, Guid organizationId)
        {
            return await _usersService
                .GetExistingContactEmailsAsync(emails, organizationId)
                .ConfigureAwait(false);
        }

        private static bool CheckForDuplicates(IEnumerable<int> organizationTypeIds)
        {
            if (organizationTypeIds.Any())
            {
                var distincedEmails = organizationTypeIds.Distinct();

                if (distincedEmails.Count() != organizationTypeIds.Count())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
