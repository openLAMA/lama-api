using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUps;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.DomainServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Controllers
{
    [Route("api/followUp/")]
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
                return BadRequest(_followUpService.ValidationDictionary.GetModelState());
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFollowUpStatusAsync(InfoSessionFollowUpUpdateSpecDto specDto)
        {
            await _followUpService.ChangeFollowUpStatusAsync(specDto).ConfigureAwait(false);

            if(!_followUpService.ValidationDictionary.GetModelState().IsValid)
            {
                return BadRequest(_followUpService.ValidationDictionary.GetModelState());
            }

            return Ok();
        } 
    }
}
