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

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.IO;
using Elyon.Fastly.Api.Domain.Dtos;
using Elyon.Fastly.Api.Domain.Services;
using System.Text;

namespace Elyon.Fastly.Api.Controllers
{
    [Route("api/util")]
    [ApiController]
    public class UtilController : ControllerBase
    {

        private readonly IUsersService _usersService;

        public UtilController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpPost("export-data")]
        public async Task<IActionResult> ExportDataAsync([FromBody] SecretDto secret)
        {

            if (secret == null)
            {
                return BadRequest();
            }
            var exportDataDtos = await _usersService.ExportDataAsync().ConfigureAwait(false);
            var stream = new MemoryStream();
            using (var writeFile = new StreamWriter(stream, Encoding.UTF8, 128, true))
            {
                var line = "OrganizationId,EpaadId,OrganizationName,OrganizationShortcutName,OrganizationTypeId,OrganizationTypeName,OrganizationAddress,OrganizationCity,OrganizationCounty,OrganizationZip,OrganizationArea,OrganizationManager,OrganizationSupportPerson,OrganizationStatus, OrganizationSchoolType,OrganizationStudentsCount,IsOnboardingEmailSent,IsContractReceived,IsStaticPooling,OrganizationCreatedOn,OrganizationLastUpdatedOn,OnboardingTimestamp,FirstTestTimestamp,SecondTestTimestamp,ThirdTestTimestamp,FourthTestTimestamp,FifthTestTimestamp,OrganizationNumberOfSamples,OrganizationNumberOfPolls,OrganizationRegisteredEmployees,OrganizationContact1Name,OrganizationContact1Email,OrganizationContact1PhoneNumber,OrganizationContact1LandLineNumber,OrganizationContact2Name,OrganizationContact2Email,OrganizationContact2PhoneNumber,OrganizationContact2LandLineNumber,OrganizationContact3Name,OrganizationContact3Email,OrganizationContact3PhoneNumber,OrganizationContact3LandLineNumberUserId,Email,Name,PhoneNumber,LandLineNumber";
                writeFile.WriteLine(line);
                exportDataDtos.ForEach(exportData =>
                {
                    var line = exportData.OrganizationId + "," + exportData.EpaadId + "," +
                    "\""+exportData.OrganizationName + "\"," +
                    "\"" + exportData.OrganizationShortcutName + "\"," +
                    exportData.OrganizationTypeId + "," +
                    "\"" + exportData.OrganizationTypeName + "\"," +
                    "\"" + exportData.OrganizationAddress + "\"," +
                    "\"" + exportData.OrganizationCity + "\"," +
                    "\"" + exportData.OrganizationCounty + "\"," +
                    "\"" + exportData.OrganizationZip + "\"," +
                    "\"" + exportData.OrganizationArea + "\"," +
                    "\"" + exportData.OrganizationManager + "\"," +
                    "\"" + exportData.OrganizationSupportPerson + "\"," +
                    exportData.OrganizationStatus + "," +
                    exportData.OrganizationSchoolType + "," +
                    exportData.OrganizationStudentsCount + "," +
                    exportData.IsOnboardingEmailSent + "," +
                    exportData.IsContractReceived + "," +
                    exportData.IsStaticPooling + "," +
                    exportData.OrganizationCreatedOn + "," +
                    exportData.OrganizationLastUpdatedOn + "," +
                    exportData.OnboardingTimestamp + "," +
                    exportData.FirstTestTimestamp + "," +
                    exportData.SecondTestTimestamp + "," +
                    exportData.ThirdTestTimestamp + "," +
                    exportData.FourthTestTimestamp + "," +
                    exportData.FifthTestTimestamp + "," +
                    exportData.OrganizationNumberOfSamples + "," +
                    exportData.OrganizationNumberOfPolls + "," + exportData.OrganizationRegisteredEmployees + "," +
                    exportData.OrganizationContact1Name + "," + exportData.OrganizationContact1Email + "," + exportData.OrganizationContact1PhoneNumber + "," + exportData.OrganizationContact1LandLineNumber + "," +
                    exportData.OrganizationContact2Name + "," + exportData.OrganizationContact2Email + "," + exportData.OrganizationContact2PhoneNumber + "," + exportData.OrganizationContact2LandLineNumber + "," +
                    exportData.OrganizationContact3Name + "," + exportData.OrganizationContact3Email + "," + exportData.OrganizationContact3PhoneNumber + "," + exportData.OrganizationContact3LandLineNumber + "," +
                    exportData.UserId + "," + exportData.Email + "," + exportData.Name + "," +
                    exportData.PhoneNumber + "," + exportData.LandLineNumber;
                    writeFile.WriteLine(line);
                });
            }
            stream.Position = 0;
            return File(stream, "application/octet-stream", "data.csv");
        }
    }
}
