﻿#region Copyright
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
using Elyon.Fastly.Api.Domain.Dtos.Cantons;
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
        private readonly IFixedTestingPersonnelCancelationService _fixedTestingPersonnelCancelationService;
        private readonly ICantonsService _cantonService;
        private readonly ITestingPersonnelConfirmationsWithoutInvitationService _testingPersonnelConfirmationsWithoutInvitationService;

        public LaboratoryController(ITestingPersonnelsService testingPersonnelsService,
            ITestingPersonnelInvitationsService testingPersonnelInvitationsService,
            IFixedTestingPersonnelCancelationService fixedTestingPersonnelCancelationService,
            ICantonsService cantonService,
            ITestingPersonnelConfirmationsWithoutInvitationService testingPersonnelConfirmationsWithoutInvitationService)
        {
            _testingPersonnelsService = testingPersonnelsService ?? throw new ArgumentNullException(nameof(testingPersonnelsService));
            _testingPersonnelsService.ValidationDictionary = new ValidationDictionary(ModelState);
            _testingPersonnelInvitationsService = testingPersonnelInvitationsService ?? throw new ArgumentNullException(nameof(testingPersonnelInvitationsService));
            _testingPersonnelInvitationsService.ValidationDictionary = _testingPersonnelsService.ValidationDictionary;
            _fixedTestingPersonnelCancelationService = fixedTestingPersonnelCancelationService ?? throw new ArgumentNullException(nameof(fixedTestingPersonnelCancelationService));
            _fixedTestingPersonnelCancelationService.ValidationDictionary = _testingPersonnelsService.ValidationDictionary;
            _cantonService = cantonService ?? throw new ArgumentNullException(nameof(cantonService));
            _cantonService.ValidationDictionary = _testingPersonnelsService.ValidationDictionary;
            _testingPersonnelConfirmationsWithoutInvitationService = 
                testingPersonnelConfirmationsWithoutInvitationService ?? throw new ArgumentNullException(nameof(testingPersonnelConfirmationsWithoutInvitationService));
            _testingPersonnelConfirmationsWithoutInvitationService.ValidationDictionary = _testingPersonnelsService.ValidationDictionary;
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
                .CreateTestingPersonnelAsync(specDto)
                .ConfigureAwait(false);

            if (!_testingPersonnelsService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                {
                    errors = _testingPersonnelsService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok(testingPersonnel.Id);
        }

        [HttpGet("tests")]
        [AuthorizeUser(RoleType.University, RoleType.Laboratory)]
        public async Task<ActionResult<TestsDataWithIsEarliestDateDto>> GetTestsDataAsync(DateTime startDate)
        {
            var testsData = await _testingPersonnelsService
                .GetTestsDataDtoAsync(startDate)
                .ConfigureAwait(false);

            return testsData;
        }

        [HttpGet("testsForDate")]
        [AuthorizeUser(RoleType.University, RoleType.Laboratory)]
        public async Task<ActionResult<TestsDataDto>> GetTestsDataForDateAsync(DateTime date)
        {
            TestsDataDto testsData = await _testingPersonnelsService
                .GetTestsDataDtoForDateAsync(date)
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

        [HttpPut("invitations/increaseShiftCount")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult> IncreaseShiftCountAsync([FromBody] TestingPersonnelInvitationIncreaseShiftSpecDto specDto)
        {
            if (specDto == null)
            {
                return BadRequest();
            }

            await _testingPersonnelInvitationsService
                .IncreaseShiftCountAsync(specDto)
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

        [HttpPost("invitations/createConfirmation")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult<TestingPersonnelInvitationConfirmedShiftsDto>> CreateConfirmationAsync([FromBody]
            TestingPersonnelManuallyAddedConfirmationDto confirmDto)
        {
            if (confirmDto == null)
            {
                return BadRequest();
            }

            var shiftNumbers = await _testingPersonnelInvitationsService
                .CreateConfirmationAsync(confirmDto)
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

        [HttpPost("fixedPersonnel/cancelForDate")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult> CancelFixedTestingPersonnelForDateAsync([FromBody] CancelFixedTestingPersonnelForDateSpecDto specDto)
        {
            if (specDto == null)
            {
                return BadRequest();
            }

            var loggedUserId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            specDto.CanceledByUserId = Guid.Parse(loggedUserId);

            await _fixedTestingPersonnelCancelationService
                .CancelFixedTestingPersonnelForDateAsync(specDto)
                .ConfigureAwait(false);

            if (!_fixedTestingPersonnelCancelationService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                {
                    errors = _fixedTestingPersonnelCancelationService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok();
        }

        [HttpGet("cantons/{id}")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult<CantonDto>> GetCantonByIdAsync(Guid id)
        {
            return await _cantonService.GetByIdAsync(id)
                .ConfigureAwait(false);
        }

        [HttpPost("cantons")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult<Guid>> CreateCantonWithSamplesCountAsync([FromBody] CantonSpecDto cantonSpecDto)
        {
            if (cantonSpecDto == null)
            {
                return BadRequest();
            }

            var cantonDto = await _cantonService
                .CreateCantonWithSamplesCountAsync(cantonSpecDto)
                .ConfigureAwait(false);

            if (!_cantonService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                {
                    errors = _cantonService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok(cantonDto.Id);
        }

        [HttpPut("cantons")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult> UpdateCantonWithSamplesCountAsync([FromBody] CantonDto cantonDto)
        {
            if (cantonDto == null)
            {
                return BadRequest();
            }

            await _cantonService
                .UpdateCantonWithSamplesCountAsync(cantonDto)
                .ConfigureAwait(false);

            if (!_cantonService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                {
                    errors = _cantonService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok();
        }

        [HttpDelete("cantons/{id}")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult> DeleteCantonAsync(Guid id)
        {
            var isCantonExisting = await _cantonService
                 .AnyAsync(x => x.Id == id)
                 .ConfigureAwait(false);
            if (!isCantonExisting)
            {
                return NotFound();
            }

            await _cantonService
                .DeleteCantonAsync(id)
                .ConfigureAwait(false);

            if (!_cantonService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                {
                    errors = _cantonService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok();
        }

        [HttpPost("confirmationsWithoutInvitation")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult<Guid>> CreateConfirmationWithoutInvitationAsync(
            [FromBody] TestingPersonnelConfirmationsWithoutInvitationSpecDto specDto)
        {
            if (specDto == null)
            {
                return BadRequest();
            }

            await _testingPersonnelConfirmationsWithoutInvitationService
                .CreateConfirmationWithoutInvitationAsync(specDto)
                .ConfigureAwait(false);

            if (!_testingPersonnelConfirmationsWithoutInvitationService.ValidationDictionary.IsValid())
            {
                return BadRequest(new
                {
                    errors = _testingPersonnelConfirmationsWithoutInvitationService.ValidationDictionary.GetErrorMessages()
                });
            }

            return Ok();
        }

        [HttpDelete("confirmationsWithoutInvitation/{id}")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult> DeleteConfirmationWithoutInvitationAsync(Guid id)
        {
            bool doesConfirmationExist = await _testingPersonnelConfirmationsWithoutInvitationService
                 .DoesConfirmationExistAsync(id)
                 .ConfigureAwait(false);
            if (!doesConfirmationExist)
            {
                return NotFound();
            }

            await _testingPersonnelConfirmationsWithoutInvitationService
                .DeleteConfirmationWithoutInvitationAsync(id)
                .ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("availableTemporaryPersonal")]
        [AuthorizeUser(RoleType.Laboratory)]
        public async Task<ActionResult<List<AvailableTemporaryPersonnelDto>>> GetAvailableTemporaryPersonnelAsync([FromQuery] AvailableTemporaryPersonnelSpecDto specDto)
        {
            List<AvailableTemporaryPersonnelDto> availablePersonnel = await _testingPersonnelsService
                .GetAvailableTemporaryPersonnelAsync(specDto)
                .ConfigureAwait(false);

            return availablePersonnel;
        }
    }
}