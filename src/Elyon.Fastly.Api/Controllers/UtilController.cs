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

using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Controllers
{
    [Route("api/util")]
    [ApiController]
    public class UtilController : ControllerBase
    {
        private readonly IUtilService _utilService;
        private readonly IOrganizationsService _organizationsService;

        public UtilController(IUtilService utilService, IOrganizationsService organizationsService)
        {
            _utilService = utilService ?? throw new ArgumentNullException(nameof(utilService));
            _organizationsService = organizationsService ?? throw new ArgumentNullException(nameof(organizationsService));
        }

        [HttpGet("organizations")]
        public async Task<ActionResult<List<OrganizationBasicDto>>> GetOrganizationsAsync([FromQuery] int typeId)
        {
            var organizationDtos = await _utilService.GetOrganizationsAsync(typeId).ConfigureAwait(false);
            return organizationDtos;
        }

        [HttpGet("organizations/{id}")]
        public async Task<ActionResult<OrganizationDetailDto>> GetOrganizationByIdAsync(Guid id)
        {
            var organizationDto = await _utilService
                .GetOrganizationByIdAsync(id)
                .ConfigureAwait(false);
            if (organizationDto.Name == null)
            {
                return NotFound();
            }
            return organizationDto;
        }

    }
}