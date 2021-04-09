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

using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUps;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.DomainServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Enums;
using Elyon.Fastly.Api.Helpers;
using Refit;

namespace Elyon.Fastly.Api.Controllers
{
    [Route("api/organization/followUp")]
    [ApiController]
    public class InfoSessionFollowUpController : ControllerBase
    {
        private readonly IInfoSessionFollowUpService _followUpService;

        public InfoSessionFollowUpController(IInfoSessionFollowUpService followUpService)
        {
            _followUpService = followUpService
                ?? throw new ArgumentNullException(nameof(followUpService));
            _followUpService.ValidationDictionary = new ValidationDictionary(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> SendFollowUpEmailAsync(InfoSessionFollowUpSpecDto specDto)
        {
            await _followUpService.SendInfoSessionFollowUpEmailAsync(specDto).ConfigureAwait(false);

            if (!_followUpService.ValidationDictionary.GetModelState().IsValid)
            {
                return BadRequest(new { errors = _followUpService.ValidationDictionary.GetErrorMessages() });
            }

            return NoContent();
        }

        [HttpPut]
        [AuthorizeUser(RoleType.University)]
        public async Task<IActionResult> UpdateFollowUpStatusAsync(InfoSessionFollowUpUpdateSpecDto specDto)
        {
            await _followUpService.UpdateFollowUpStatusAsync(specDto).ConfigureAwait(false);

            if(!_followUpService.ValidationDictionary.GetModelState().IsValid)
            {
                return BadRequest(new { errors = _followUpService.ValidationDictionary.GetErrorMessages() });
            }

            return NoContent();
        }

        [HttpPut("respond")]
        public async Task<IActionResult> RegisterUserResponseAsync(InfoSessionFollowUpResponseSpecDto specDto)
        {
            await _followUpService.ChangeFollowUpStatusAsync(specDto).ConfigureAwait(false);

            if(!_followUpService.ValidationDictionary.GetModelState().IsValid)
            {
                return BadRequest(new { errors = _followUpService.ValidationDictionary.GetErrorMessages() });
            }

            return NoContent();
        }
    }
}
