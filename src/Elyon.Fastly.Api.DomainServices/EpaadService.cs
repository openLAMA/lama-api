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

using Elyon.Fastly.Api.Domain.Dtos.EpaadDtos;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using Newtonsoft.Json;
using Prime.Sdk.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.DomainServices
{
    public class EpaadService : IEpaadService
    {
        private const string AuthSchema = "Bearer";

        private readonly string _epaadEmail;
        private readonly string _epaadPassword;
        private readonly string _baseEpaadUrl;
        private readonly IOrganizationsRepository _organizationsRepository;
        private readonly ILog _log;

        private IHttpClientFactory _httpClientFactory;

#pragma warning disable CA1054 // Uri parameters should not be strings
        public EpaadService(IHttpClientFactory clientFactory, IOrganizationsRepository organizationsRepository, ILogFactory logFactory,
            string baseEpaadUrl, string epaadEmail, string epaadPassword)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (logFactory == null)
                throw new ArgumentNullException(nameof(logFactory));

            if (clientFactory == null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }

            _httpClientFactory = clientFactory;
            _baseEpaadUrl = baseEpaadUrl;
            _epaadEmail = epaadEmail;
            _epaadPassword = epaadPassword;
            _organizationsRepository = organizationsRepository;
            _log = logFactory.CreateLog(this);
        }

        public async Task<PushEpaadOrganizationResponseDto> PushOrganizationToEpaadAsync(PushEpaadOrganizationDto ePaadOrganizationDto)
        {
            var epaadOrganizationJson = JsonConvert.SerializeObject(ePaadOrganizationDto);

            using var epaadOrganization = new StringContent(
                epaadOrganizationJson,
                Encoding.Default,
                "application/json");
            
            var jwtToken = await GetEpaadJWTTokenAsync()
                .ConfigureAwait(false);

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseEpaadUrl);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthSchema, jwtToken.Token);
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            using var httpResponse = await client
                .PostAsync("organizations", epaadOrganization)
#pragma warning restore CA2234 // Pass system uri objects instead of strings
                .ConfigureAwait(false);

            httpResponse.EnsureSuccessStatusCode();

            using var stream = await httpResponse.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);
            using var streamReader = new StreamReader(stream);
            using var json = new JsonTextReader(streamReader);
            var jsonSerializer = new JsonSerializer();
            var ePaadPushResponse = jsonSerializer
                .Deserialize<PushEpaadOrganizationResponseDto>(json);

            return ePaadPushResponse;
        }

        private async Task<EpaadJwtTokenResponseDto> GetEpaadJWTTokenAsync()
        {
            var epaadCredentialsDto = new EpaadAuthCredentialsDto
            {
                Email = _epaadEmail,
                Password = _epaadPassword
            };

            var epaadCredentialsJson = JsonConvert.SerializeObject(epaadCredentialsDto);

            using var epaadCredentials = new StringContent(
                epaadCredentialsJson,
                Encoding.Default,
                "application/json");

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseEpaadUrl);

#pragma warning disable CA2234 // Pass system uri objects instead of strings
            using var httpResponse = await client
               .PostAsync("authenticate", epaadCredentials)
#pragma warning restore CA2234 // Pass system uri objects instead of strings
                .ConfigureAwait(false);

            httpResponse.EnsureSuccessStatusCode();

            using var stream = await httpResponse.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);
            using var streamReader = new StreamReader(stream);
            using var json = new JsonTextReader(streamReader);
            var jsonSerializer = new JsonSerializer();
            var ePaadJwtTokenResponse = jsonSerializer
                .Deserialize<EpaadJwtTokenResponseDto>(json);

            return ePaadJwtTokenResponse;
        }

        public async Task UpdateNumberOfRegisteredEmployeesFromEpaadPerOrganizationAsync()
        {
            var organizationsWithEpaadId = await _organizationsRepository
                .GetOrganizationsWithEpaadIdAsync()
                .ConfigureAwait(false);

            if (organizationsWithEpaadId.Any())
            {
                var epaadAllOrganizations = await GetAllOrganizationsFromEpaadAsync().ConfigureAwait(false);

                if (epaadAllOrganizations.Any())
                {
                    var dbEpaadIds = organizationsWithEpaadId.Select(x => x.EpaadId.ToString(CultureInfo.InvariantCulture));
                    var epaadOrganizationsWithExisingEpaadId = epaadAllOrganizations.Where(x => dbEpaadIds.Contains(x.EpaadId));

                    foreach (var dbEpaadOrg in epaadOrganizationsWithExisingEpaadId)
                    {
                        var epaadOrgDto = new EpaadOrganizationDto
                        {
                            EpaadId = Convert.ToInt32(dbEpaadOrg.EpaadId, CultureInfo.InvariantCulture),
                            RegisteredEmployees = dbEpaadOrg.RegisteredEmployees
                        };

                        await _organizationsRepository
                            .UpdateRegisteredEmployeesPerOrganizationAsync(epaadOrgDto)
                            .ConfigureAwait(false);
                    }
                }
            }
        }

        private async Task<List<EpaadOrganizationResponseDto>> GetAllOrganizationsFromEpaadAsync()
        {
            try
            {                
                var jwtToken = await GetEpaadJWTTokenAsync().ConfigureAwait(false);
                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_baseEpaadUrl);
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthSchema, jwtToken.Token);
#pragma warning disable CA2234 // Pass system uri objects instead of strings
                using var httpResponse = await client
                    .GetAsync("get_organizations")
#pragma warning restore CA2234 // Pass system uri objects instead of strings
                .ConfigureAwait(false);

                httpResponse.EnsureSuccessStatusCode();

                using var stream = await httpResponse.Content
                    .ReadAsStreamAsync()
                    .ConfigureAwait(false);
                using var streamReader = new StreamReader(stream);
                using var json = new JsonTextReader(streamReader);
                var jsonSerializer = new JsonSerializer();
                var ePaadOrganizations = jsonSerializer
                    .Deserialize<List<EpaadOrganizationResponseDto>>(json);

                return ePaadOrganizations;
            }
            catch(Exception ex)
            {
                _log.Error(ex, ex.Message);
                throw;
            }            
        }
    }
}