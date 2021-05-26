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
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class UsersRepository : BaseCrudRepository<User, UserDto>, IUsersRepository
    {
        private readonly IAESCryptography _aESCryptography;

        public UsersRepository(Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, 
            IMapper mapper, IAESCryptography aESCryptography)
            : base(contextFactory, mapper)
        {
            _aESCryptography = aESCryptography;
        }

        public async Task<JWTokenUserDataDto> GenerateJwtUserDataAsync(Guid userId)
        {
            await using var context = ContextFactory.CreateDataContext(null);
            var userInfo = await context.Users
                .Where(item => item.Id == userId)
                .Select(x => new
                {
                    LamaCompany = x.LamaCompany,
                    OrganizationId = x.OrganizationId,
                    Organization = x.Organization,
                    Email = _aESCryptography.Decrypt(x.Email),
                    UserName = _aESCryptography.Decrypt(x.Name)
                })
                .FirstAsync()
                .ConfigureAwait(false);
            
            var jwtUserData = new JWTokenUserDataDto { UserId = userId };
            if (userInfo.LamaCompany != null)
            {
                jwtUserData.RoleType = userInfo.LamaCompany.RoleType;
                jwtUserData.OrganizationOrCompanyId = userInfo.LamaCompany.Id;
                jwtUserData.Email = userInfo.Email;
                jwtUserData.UserName = userInfo.UserName;

            }
            else
            {
                jwtUserData.RoleType = RoleType.Organization;
                jwtUserData.OrganizationOrCompanyId = userInfo.OrganizationId ?? Guid.Empty;
                jwtUserData.OrganizationTypeId = userInfo.Organization?.OrganizationTypeId;
                jwtUserData.Email = userInfo.Email;
                jwtUserData.UserName = userInfo.UserName;
            }

            return jwtUserData;
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            var encryptedEmail = _aESCryptography.Encrypt(email.ToLower(CultureInfo.InvariantCulture));
#pragma warning restore CA1308 // Normalize strings to uppercase

            var user = await GetAsync(u => u.Email == encryptedEmail).ConfigureAwait(false);

            return user;
        }

        public async Task<List<string>> GetExistingContactEmailsAsync(
            IEnumerable<string> contactEmails, Guid excludeOrganizationId)
        {
            if(contactEmails == null)
            {
                throw new ArgumentNullException(nameof(contactEmails));
            }

            var encryptedEmails = new List<string>();
            foreach(var email in contactEmails)
            {
                encryptedEmails.Add(_aESCryptography.Encrypt(email));
            }

            await using var context = ContextFactory.CreateDataContext(null);
            var existingEmails = await context.Users
                .Where(item => encryptedEmails.Contains(item.Email) && ((item.OrganizationId.HasValue && item.OrganizationId != excludeOrganizationId)
                || (item.LamaCompanyId.HasValue && item.LamaCompanyId != excludeOrganizationId)))
                .Select(x => _aESCryptography.Decrypt(x.Email))
                .ToListAsync()
                .ConfigureAwait(false);

            return existingEmails;
        }
    }
}
