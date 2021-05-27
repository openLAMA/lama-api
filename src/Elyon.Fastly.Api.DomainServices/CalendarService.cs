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

using System;
using System.Globalization;
using System.Threading.Tasks;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using Elyon.Fastly.Api.Helpers;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace Elyon.Fastly.Api.DomainServices
{
    public class CalendarService : ICalendarService
    {
        private const int eventDurationInHours = 1;
        private const string dateFormat = "d-M-yyyy";
        private const string EditOrganizationPath = "university/edit-program-member/{organizationId}";

        private readonly IOrganizationsRepository _organizationsRepository;
        private readonly string _baseFrontendUrl;

        public CalendarService(IOrganizationsRepository organizationsRepository, string baseFrontendUrl)
        {
            _organizationsRepository = organizationsRepository;
            _baseFrontendUrl = baseFrontendUrl;
        }

        public async Task<string> GenerateOnboardingCalendarEventsAsync()
        {
            var organizationsOnboardingEventsDto = 
                await _organizationsRepository.GetOrganizationsOnboardingEventsDataAsync()
                    .ConfigureAwait(false);

            var calendar = new Ical.Net.Calendar();
            foreach (var eventDto in organizationsOnboardingEventsDto)
            {
                var linkToOrg = UrlHelper.UrlCombine(_baseFrontendUrl, EditOrganizationPath)
                    .Replace("{organizationId}", eventDto.Id.ToString(), StringComparison.InvariantCultureIgnoreCase);
                var icalEvent = new CalendarEvent
                {
                    Uid = $"{eventDto.OnboardingTimestamp.Date.ToString(dateFormat, CultureInfo.InvariantCulture)}-{eventDto.Name}",
                    Summary = eventDto.Name,
                    Description = $"Support Person: {eventDto.SupportPersonName}, {eventDto.SupportPersonEmail}.\nLink to organization: {linkToOrg}",
                    Location = eventDto.City,
                    Start = new CalDateTime(eventDto.OnboardingTimestamp),
                    End = new CalDateTime(eventDto.OnboardingTimestamp.AddHours(eventDurationInHours)),
                    Organizer = new Organizer()
                    {
                        CommonName = eventDto.SupportPersonName,
                        Value = new Uri($"mailto:{eventDto.SupportPersonEmail}")
                    }
                };

                calendar.Events.Add(icalEvent);
            }

            var iCalSerializer = new CalendarSerializer();
            return iCalSerializer.SerializeToString(calendar);
        }
    }
}
