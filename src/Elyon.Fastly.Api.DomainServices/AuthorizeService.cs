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

using Elyon.Fastly.Api.Domain.Dtos;
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Helpers;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;

namespace Elyon.Fastly.Api.DomainServices
{
    public class AuthorizeService : IAuthorizeService
    {
        private const int JwtExperationPeriodMins = 1440 * 7;
        private const int LoginConfirmationTokenExperationPeriodMins = 30;
        
        private readonly IUsersRepository _usersRepository;
        private readonly IUserConfirmationTokensRepository _userConfirmationTokensRepository;
        private readonly IOrganizationsRepository _organizationsRepository;
        private readonly ILamaCompaniesRepository _lamaCompaniesRepository;
        private readonly IEmailSenderService _mailSender;

        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public AuthorizeService(IUsersRepository usersRepository, 
            IUserConfirmationTokensRepository userConfirmationTokensRepository,
            IOrganizationsRepository organizationsRepository, ILamaCompaniesRepository lamaCompaniesRepository, IEmailSenderService mailSender, 
            string jwtKey, string jwtIssuer, string jwtAudience)
        {
            _userConfirmationTokensRepository = userConfirmationTokensRepository;
            _usersRepository = usersRepository;
            _organizationsRepository = organizationsRepository;
            _lamaCompaniesRepository = lamaCompaniesRepository;
            _mailSender = mailSender;
            _jwtKey = jwtKey;
            _jwtIssuer = jwtIssuer;
            _jwtAudience = jwtAudience;
        }

        public async Task<bool> LoginAsync(string email)
        {
            var confirmationToken = await BuildConfirmationTokenAsync(email,
                    LoginConfirmationTokenExperationPeriodMins)
                .ConfigureAwait(false);

            if (confirmationToken == null)
                return false;

            await _mailSender.SendLoginConfirmationAsync(email, confirmationToken)
                .ConfigureAwait(false);

            return true;
        }

        public async Task<JWTokenDto> ConfirmLoginAsync(string confirmationToken)
        {
            var userOrganizationDto = await GetUserOrganizationDtoByConfirmationTokenAsync(confirmationToken)
                .ConfigureAwait(false);

            if(userOrganizationDto == null)
            {
                return null;
            }

            if (userOrganizationDto.OrganizationId.HasValue)
            {
                var organizationStatus = await _organizationsRepository
                    .GetOrganizationStatusByOrganizationIdAsync(userOrganizationDto.OrganizationId.Value)
                    .ConfigureAwait(false);

                if (organizationStatus == OrganizationStatus.NotConfirmed || organizationStatus == OrganizationStatus.NotActive)
                {
                    return null;
                }
            }

            var jwtToken = await GenerateJWTokenAsync(userOrganizationDto.UserId).ConfigureAwait(false);
            if (jwtToken == null)
                return null;

            await _userConfirmationTokensRepository
                .DisposeConfirmationToken(confirmationToken)
                .ConfigureAwait(false);

            return jwtToken;
        }

        public async Task<JWTokenDto> ConfirmRegistrationAsync(string confirmationToken)
        {
            var userOrganizationDto = await GetUserOrganizationDtoByConfirmationTokenAsync(confirmationToken)
                .ConfigureAwait(false);

            if (userOrganizationDto == null)
            {
                return null;
            }

            if (userOrganizationDto.OrganizationId.HasValue)
            {
                var organizationStatus = await _organizationsRepository
                    .GetOrganizationStatusByOrganizationIdAsync(userOrganizationDto.OrganizationId.Value)
                    .ConfigureAwait(false);

                if (organizationStatus == OrganizationStatus.NotActive)
                {
                    return null;
                }
            }

            var jwtToken = await GenerateJWTokenAsync(userOrganizationDto.UserId).ConfigureAwait(false);
            if (jwtToken == null)
                return null;

            await _userConfirmationTokensRepository
                .DisposeConfirmationToken(confirmationToken)
                .ConfigureAwait(false);

            await _organizationsRepository
                   .ChangeOrganizationStatusAsync(userOrganizationDto.UserId, OrganizationStatus.PendingContact)
                   .ConfigureAwait(false);

            return jwtToken;
        }

        private async Task<JWTokenDto> GenerateJWTokenAsync(Guid userId)
        {
            var user = await GenerateJwtUserDataAsync(userId)
                .ConfigureAwait(false);

            var claims = PopulateTokenClaims(user);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(JwtExperationPeriodMins),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return new JWTokenDto { Token = tokenString, Email = user.Email, UserName = user.UserName };
        }

        private static List<Claim> PopulateTokenClaims(JWTokenUserDataDto user)
        {
            return new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                new Claim("OrgOrCompanyId", user.OrganizationOrCompanyId.ToString()),
                new Claim(ClaimTypes.Role, user.RoleType.ToString())
            };
        }

        private async Task<JWTokenUserDataDto> GenerateJwtUserDataAsync(Guid userId)
        {
            return await _usersRepository.GenerateJwtUserDataAsync(userId)
                .ConfigureAwait(false);
        }

        public async Task<string> BuildConfirmationTokenAsync(string userEmail, 
            int expirationPeriodInMinutes)
        {
            var userDto = await _usersRepository
              .GetUserByEmailAsync(email: userEmail)
              .ConfigureAwait(false);

            if (userDto == null)
            {
                return null;
            }

            var expirationTime = DateTime.UtcNow.AddMinutes(expirationPeriodInMinutes);

            return await _userConfirmationTokensRepository
                .AddUserConfirmationTokenAsync(userDto.Id, expirationTime)
                .ConfigureAwait(false);
        }

        private async Task<UserOrganizationDto> GetUserOrganizationDtoByConfirmationTokenAsync(string token)
        {            
            var userOrganizationDto = await _userConfirmationTokensRepository
                .GetUserOrgByConfirmationTokenAsync(token, DateTime.UtcNow)
                .ConfigureAwait(false);                   

            return userOrganizationDto;           
        }

        public async Task<bool> HasAccessToOrganizationAsync(string roleType, Guid userId, Guid organizationId)
        {
            if (roleType == RoleType.Organization.ToString() && 
                !await _organizationsRepository.IsUserPartOfOrganizationAsync(organizationId, userId).ConfigureAwait(false))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> HasAccessToLamaCompanyAsync(Guid userId, Guid lamaCompanyId)
        {
            return await _lamaCompaniesRepository
                .IsUserPartOfLamaCompanyAsync(lamaCompanyId, userId)
                .ConfigureAwait(false);
        }
    }
}
