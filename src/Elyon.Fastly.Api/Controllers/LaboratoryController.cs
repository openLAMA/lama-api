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
using Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels;
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
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Controllers
{
    [Route("api/laboratory")]
    [ApiController]
    [Authorize]
    public class LaboratoryController : ControllerBase
    {
        private readonly ITestingPersonnelsService _testingPersonnelsService;
        private readonly ITestingPersonnelInvitationsService _testingPersonnelInvitationsService;

        public LaboratoryController(ITestingPersonnelsService testingPersonnelsService,
            ITestingPersonnelInvitationsService testingPersonnelInvitationsService)
        {
            _testingPersonnelsService = testingPersonnelsService ?? throw new ArgumentNullException(nameof(testingPersonnelsService));
            _testingPersonnelsService.ValidationDictionary = new ValidationDictionary(ModelState);
            _testingPersonnelInvitationsService = testingPersonnelInvitationsService ?? throw new ArgumentNullException(nameof(testingPersonnelInvitationsService));
            _testingPersonnelInvitationsService.ValidationDictionary = _testingPersonnelsService.ValidationDictionary;
        }

        [HttpGet("personnelStatuses")]
        [AuthorizeUser(RoleType.University, RoleType.Laboratory)]
        public async Task<ActionResult<List<TestingPersonnelStatusDto>>> GetTestingPersonnelStatusesAsync()
        {
            var testingPersonnelStatuses = await _testingPersonnelsService
                .GetAllTestingPersonnelStatusesAsync()
                .ConfigureAwait(false);

            return testingPersonnelStatuses;
        }


        [HttpGet("testingPersonnel")]
        [AuthorizeUser(RoleType.University, RoleType.Laboratory)]
        public async Task<ActionResult<PagedResults<TestingPersonnelDto>>> GetTestingPersonnelsAsync()
        {
            var pager = new Paginator
            {
                CurrentPage = 1,
                PageSize = int.MaxValue
            };

            var testingPersonnels = await _testingPersonnelsService
                .GetListAsync(pager, x => true, null)
                .ConfigureAwait(false);

            return testingPersonnels;
        }

        [HttpPost("testingPersonnel")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult<PagedResults<TestingPersonnelDto>>> CreateTestingPersonnelAsync([FromBody] TestingPersonnelSpecDto specDto)
        {
            if(specDto == null)
            {
                return BadRequest();
            }

            var testingPersonnel = await _testingPersonnelsService
                .AddAsync(specDto)
                .ConfigureAwait(false);

            return Ok(testingPersonnel.Id);
        }

        [HttpGet("tests")]
        [AuthorizeUser(RoleType.University, RoleType.Laboratory)]
        public async Task<ActionResult<List<TestsDataDto>>> GetTestsDataAsync()
        {
            var testsData = await _testingPersonnelsService
                .GetTestsDataDtoAsync()
                .ConfigureAwait(false);

            return testsData;
        }

        [HttpGet("testsForDate")]
        [AuthorizeUser(RoleType.University, RoleType.Laboratory)]
        public async Task<ActionResult<TestsDataDto>> GetTestsDataForDateAsync(DateTime date)
        {
            TestsDataDto testsData = await _testingPersonnelsService
                .GetTestsDataDtoAsync(date)
                .ConfigureAwait(false);

            return testsData;
        }

        [HttpPost("invitations")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult> CreateInvitationsAsync([FromBody] TestingPersonnelInvitationSpecDto specDto)
        {
            if (specDto == null)
            {
                return BadRequest();
            }

            var loggedUserId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            specDto.SentByUserId = Guid.Parse(loggedUserId);

            await _testingPersonnelInvitationsService
                .CreateInvitationAsync(specDto)
                .ConfigureAwait(false);

            if (!_testingPersonnelInvitationsService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                { 
                    errors = _testingPersonnelInvitationsService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok();
        }

        [HttpPost("invitations/confirm")]
        [AllowAnonymous]
        public async Task<ActionResult<TestingPersonnelInvitationConfirmedShiftsDto>> ConfirmInvitationAsync([FromBody] 
            TestingPersonnelInvitationConfirmDto confirmDto)
        {
            if (confirmDto == null)
            {
                return BadRequest();
            }
            
            var shiftNumbers = await _testingPersonnelInvitationsService
                .ConfirmInvitationAsync(confirmDto)
                .ConfigureAwait(false);

            if (!_testingPersonnelInvitationsService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                {
                    errors = _testingPersonnelInvitationsService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok(shiftNumbers);
        }

        [HttpPut("invitations/cancelConfirmation")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult> CancelConfirmationAsync([FromBody] TestingPersonnelCancelConfrimationSpecDto specDto)
        {
            if (specDto == null)
            {
                return BadRequest();
            }

            var loggedUserId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            specDto.CanceledByUserId = Guid.Parse(loggedUserId);

            await _testingPersonnelInvitationsService
                .CancelConfirmationAsync(specDto)
                .ConfigureAwait(false);

            if (!_testingPersonnelInvitationsService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                {
                    errors = _testingPersonnelInvitationsService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok();
        }

        [HttpDelete("testingPersonnel/{id}")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult> DeleteTestingPersonnelAsync(Guid id)
        {
           var isTestingPersonnelExisting = await _testingPersonnelsService
                .AnyAsync(x => x.Id == id)
                .ConfigureAwait(false);
            if (!isTestingPersonnelExisting)
            {
                return NotFound();
            }

            await _testingPersonnelsService
                .DeleteAsync(id)
                .ConfigureAwait(false);

            return Ok();
        }
    }
}