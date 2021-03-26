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
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.DomainServices;
using Elyon.Fastly.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IAuthorizeService _authService;
        private readonly ILamaCompaniesService _lamaCompaniesService;

        public ProfileController(IAuthorizeService authService, ILamaCompaniesService lamaCompaniesService)
        {
            _authService = authService;
            _lamaCompaniesService = lamaCompaniesService ?? throw new ArgumentNullException(nameof(lamaCompaniesService));
            _lamaCompaniesService.ValidationDictionary = new ValidationDictionary(ModelState);
        }

        [HttpGet]
        [AuthorizeUser(RoleType.Logistics, RoleType.University, RoleType.Laboratory, RoleType.State)]
        public async Task<ActionResult<LamaCompanyProfileDto>> GetLamaCompanyProfileByIdAsync(Guid id)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var hasAccessToOrganization = await _authService
                .HasAccessToLamaCompanyAsync(Guid.Parse(userId), id)
                .ConfigureAwait(false);
            if (!hasAccessToOrganization)
            {
                return Unauthorized();
            }

            var lamaCompanyProfileDto = await _lamaCompaniesService
                .GetLamaCompanyProfileAsync(id)
                .ConfigureAwait(false);

            if (lamaCompanyProfileDto == null)
            {
                return NotFound();
            }

            return lamaCompanyProfileDto;
        }

        [HttpPut]
        [AuthorizeUser(RoleType.Logistics, RoleType.University, RoleType.Laboratory, RoleType.State)]
        public async Task<ActionResult> UpdateLamaCompanyProfile([FromBody] LamaCompanyProfileSpecDto dto)
        {
            if(dto == null)
            {
                return BadRequest();
            }

            var userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var hasAccessToOrganization = await _authService
                .HasAccessToLamaCompanyAsync(Guid.Parse(userId), dto.Id)
                .ConfigureAwait(false);
            if (!hasAccessToOrganization)
            {
                return Unauthorized();
            }

            await _lamaCompaniesService
                .UpdateLamaCompanyProfileAsync(dto, Guid.Parse(userId))
                .ConfigureAwait(false);

            if (!_lamaCompaniesService.ValidationDictionary.IsValid())
            {
                return BadRequest(_lamaCompaniesService.ValidationDictionary.GetModelState());
            }

            return NoContent();
        }
    }
}
