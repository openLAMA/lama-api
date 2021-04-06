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

using Elyon.Fastly.Api.Domain;
using Elyon.Fastly.Api.Domain.Dtos;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.DomainServices;
using Elyon.Fastly.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationTypesService _organizationTypesService;
        private readonly IOrganizationsService _organizationsService;
        private readonly IAuthorizeService _authService;

        public OrganizationsController(IOrganizationTypesService organizationTypesService,
            IOrganizationsService organizationsService, IAuthorizeService authService)
        {
            _organizationTypesService = organizationTypesService;
            _organizationsService = organizationsService ?? throw new ArgumentNullException(nameof(organizationsService));
            _authService = authService;
            _organizationsService.ValidationDictionary = new ValidationDictionary(ModelState);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<OrganizationTypeDto>>> GetOrganizationTypesAsync()
        {
            var organizationTypesDtos = await _organizationTypesService
                .GetAllOrganizationTypesAsync()
                .ConfigureAwait(false);

            return organizationTypesDtos;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> CreateOrganization([FromBody] OrganizationSpecDto specDto)
        {
            var createdDto = await _organizationsService
                .CreateOrganizationAsync(specDto)
                .ConfigureAwait(false);

            if (createdDto == null)
            {
                return BadRequest(new { errors = _organizationsService.ValidationDictionary.GetErrorMessages() });
            }

            return Ok(createdDto.Id);
        }

        [HttpPost("confirmRegistration")]
        public async Task<ActionResult<JWTokenDto>> ConfirmRegistration(UserConfirmationSpecDto confirmationSpecDto)
        {
            if (confirmationSpecDto == null)
            {
                return BadRequest();
            }

            var jwtToken = await _authService
                .ConfirmRegistrationAsync(confirmationSpecDto.ConfirmationToken)
                .ConfigureAwait(false);

            if (jwtToken == null)
            {
                return Unauthorized();
            }

            return jwtToken;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<OrganizationProfileDto>> GetOrganizationProfileByIdAsync(Guid id)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var roleType = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            var hasAccessToOrganization = await _authService
                .HasAccessToOrganizationAsync(roleType, Guid.Parse(userId), id)
                .ConfigureAwait(false);
            if (!hasAccessToOrganization)
            {
                return Unauthorized();
            }

            var organizationDto = await _organizationsService
                .GetOrganizationProfileAsync(id)
                .ConfigureAwait(false);

            if (organizationDto == null)
            {
                return NotFound();
            }

            return organizationDto;
        }

        [Authorize]
        [HttpGet("dashboardInfo")]
        public async Task<ActionResult<OrganizationDashboardDto>> GetDashboardInfoForOrganizationAsync(Guid id)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var roleType = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            var hasAccessToOrganization = await _authService
                .HasAccessToOrganizationAsync(roleType, Guid.Parse(userId), id)
                .ConfigureAwait(false);
            if (!hasAccessToOrganization)
            {
                return Unauthorized();
            }

            var organizationDto = await _organizationsService
                .GetOrganizationDashboardInfoAsync(id)
                .ConfigureAwait(false);

            if (organizationDto == null)
            {
                return NotFound();
            }

            return organizationDto;
        }

        [HttpPut("profile")]
        [AuthorizeUser(RoleType.Organization, RoleType.University, RoleType.Laboratory, RoleType.State)]
        public async Task<ActionResult> UpdateOrganizationProfile([FromBody] OrganizationProfileSpecDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var roleType = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            var hasAccessToOrganization = await _authService
                .HasAccessToOrganizationAsync(roleType, Guid.Parse(userId), dto.Id)
                .ConfigureAwait(false);
            if (!hasAccessToOrganization)
            {
                return Unauthorized();
            }

            await _organizationsService
                .UpdateOrganizationProfileAsync(dto, Guid.Parse(userId))
                .ConfigureAwait(false);

            if (!_organizationsService.ValidationDictionary.IsValid())
            {
                return BadRequest(new { errors = _organizationsService.ValidationDictionary.GetErrorMessages() });
            }

            return NoContent();
        }

        [HttpGet]
        [AuthorizeUser(RoleType.Logistics, RoleType.University, RoleType.Laboratory, RoleType.State)]
        public async Task<ActionResult<PagedResults<OrganizationDto>>> GetOrganizationsAsync(string orderBy, bool isAscending)
        {
            var pager = new Paginator
            {
                CurrentPage = 1,
                PageSize = int.MaxValue
            };

            var organizationDtos = await _organizationsService
                .GetAllOrganizationsAsync(pager, x => true, orderBy, isAscending)
                .ConfigureAwait(false);

            return organizationDtos;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrganizationDto>> GetOrganizationByIdAsync(Guid id)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var roleType = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            var hasAccessToOrganization = await _authService
                .HasAccessToOrganizationAsync(roleType, Guid.Parse(userId), id)
                .ConfigureAwait(false);
            if (!hasAccessToOrganization)
            {
                return Unauthorized();
            }

            var organizationDto = await _organizationsService
                .GetByIdAsync(id)
                .ConfigureAwait(false);

            return organizationDto;
        }

        [HttpPut]
        [AuthorizeUser(RoleType.University, RoleType.Laboratory, RoleType.State)]
        public async Task<ActionResult> UpdateOrganization([FromBody] OrganizationDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            
            await _organizationsService
                .UpdateOrganizationAsync(dto, Guid.Parse(userId))
                .ConfigureAwait(false);

            if (!_organizationsService.ValidationDictionary.IsValid())
            {
                return BadRequest(new { errors = _organizationsService.ValidationDictionary.GetErrorMessages() });
            }

            return NoContent();
        }

        [HttpPost("activeStatus")]
        [AuthorizeUser(RoleType.University, RoleType.Laboratory, RoleType.State)]
        public async Task<ActionResult> ChangeOrganizationActiveStateAsync(OrganizationActiveStatusDto dto)
        {
            if(dto == null)
            {
                return BadRequest();
            }

            await _organizationsService
                .ChangeOrganizationActiveStatusAsync(dto)
                .ConfigureAwait(false);

            if (!_organizationsService.ValidationDictionary.IsValid())
            {
                return BadRequest(new { errors = _organizationsService.ValidationDictionary.GetErrorMessages() });
            }

            return NoContent();
        }

        [HttpPost("pushToEPaad")]
        [AuthorizeUser(RoleType.University)]
        public async Task<ActionResult> PushOrganizationToEPaad([FromBody] OrganizationDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            await _organizationsService
              .PushOrganizationToEPaadAsync(dto)
              .ConfigureAwait(false);

            if (!_organizationsService.ValidationDictionary.IsValid())
            {
                return BadRequest(new { errors = _organizationsService.ValidationDictionary.GetErrorMessages() });
            }

            return Ok();
        }
    }
}
