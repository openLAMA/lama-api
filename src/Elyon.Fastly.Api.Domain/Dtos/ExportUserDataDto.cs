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

using Elyon.Fastly.Api.Domain.Enums;
using System;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;

namespace Elyon.Fastly.Api.Domain.Dtos
{
    public class ExportUserDataDto
    {
        public Guid? OrganizationId { get; set; }
        public int? EpaadId { get; set; }
        public string OrganizationName { get; set; }

        public int OrganizationTypeId { get; set; }

        public string OrganizationTypeName { get; set; }

        public string OrganizationCity { get; set; }

        public string OrganizationAddress { get; set; }


        public string OrganizationZip { get; set; }

        public string OrganizationCounty{ get; set; }

        public string OrganizationArea { get; set; }

        public string OrganizationManager { get; set; }

        public string OrganizationSupportPerson { get; set; }

        public OrganizationStatus? OrganizationStatus { get; set; }

        public SchoolType? OrganizationSchoolType { get; set; }

        public int? OrganizationStudentsCount { get; set; }

        public string OrganizationShortcutName { get; set; }

        public bool IsOnboardingEmailSent { get; set; }
        public bool IsContractReceived { get; set; }
        public bool IsStaticPooling { get; set; }

        public string PickupLocation { get; set; }

        public string OnboardingTimestamp { get; set; }
        public string FirstTestTimestamp { get; set; }

        public string SecondTestTimestamp { get; set; }

        public string ThirdTestTimestamp { get; set; }

        public string FourthTestTimestamp { get; set; }

        public string FifthTestTimestamp { get; set; }

        public string ReportingContact { get; set; }

        public string ReportingEmail { get; set; }

        public string OrganizationCreatedOn { get; set; }

        public string OrganizationLastUpdatedOn { get; set; }

        public int? OrganizationNumberOfSamples { get; set; }

        public int? OrganizationNumberOfPolls { get; set; }

        public int? OrganizationRegisteredEmployees { get; set; }

        public string OrganizationContact1Email { get; set; }

        public string OrganizationContact2Email { get; set; }

        public string OrganizationContact3Email { get; set; }

        public string OrganizationContact1PhoneNumber { get; set; }

        public string OrganizationContact2PhoneNumber { get; set; }

        public string OrganizationContact3PhoneNumber { get; set; }

        public string OrganizationContact1Name { get; set; }

        public string OrganizationContact2Name { get; set; }

        public string OrganizationContact3Name { get; set; }

        public string OrganizationContact1LandLineNumber { get; set; }

        public string OrganizationContact2LandLineNumber { get; set; }

        public string OrganizationContact3LandLineNumber { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string LandLineNumber { get; set; }
    }
}
