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
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Controllers
{
    [Route("api/organizationNotes")]
    [ApiController]
    public class OrganizationNotesController : ControllerBase
    {
        private readonly IOrganizationNotesService _organizationNotesService;

        public OrganizationNotesController(IOrganizationNotesService organizationNotesService)
        {
            _organizationNotesService = organizationNotesService;
        }

        [HttpPost]
        [AuthorizeUser(RoleType.University)]
        public async Task<ActionResult<Guid>> CreateNote(OrganizationNoteSpecDto dto)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var newNote = await _organizationNotesService
                .AddAsync(dto, new Guid(userId))
                .ConfigureAwait(false);

            if (!_organizationNotesService.ValidationDictionary.IsValid())
            {
                return BadRequest(_organizationNotesService.ValidationDictionary.GetModelState());
            }

            return Ok(newNote.Id);
        }

        [HttpGet]
        [AuthorizeUser(RoleType.University)]
        public async Task<ActionResult<List<OrganizationNoteDto>>> GetOrganizationNotes([FromQuery] Guid organizationId)
        {
            var pager = new Paginator
            {
                CurrentPage = 1,
                PageSize = int.MaxValue
            };

            var notes = await _organizationNotesService
                .GetListAsync(pager, dto => dto.OrganizationId == organizationId, dto => dto.CreatedOn)
                .ConfigureAwait(false);

            return Ok(notes);
        }
    }
}
