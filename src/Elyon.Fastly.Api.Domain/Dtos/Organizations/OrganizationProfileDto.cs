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
using System.Collections.Generic;

namespace Elyon.Fastly.Api.Domain.Dtos.Organizations
{
    public class OrganizationProfileDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int OrganizationTypeId { get; set; }

        public OrganizationTypeDto OrganizationType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public Guid CityId { get; set; }

        public string Zip { get; set; }

        public string Address { get; set; }

        public DateTime? TrainingTimestamp { get; set; }

        public DateTime? OnboardingTimestamp { get; set; }

        public DateTime? FirstTestTimestamp { get; set; }

        public DateTime? SecondTestTimestamp { get; set; }

        public DateTime? ThirdTestTimestamp { get; set; }

        public DateTime? FourthTestTimestamp { get; set; }

        public DateTime? FifthTestTimestamp { get; set; }

        public int NumberOfSamples { get; set; }

        public int? NumberOfPools { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<UserDto> Contacts { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        public Guid SupportPersonId { get; set; }

        public UserDto SupportPerson { get; set; }

        public OrganizationStatus Status { get; set; }

        public string DataPassword { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<SubOrganizationDto> SubOrganizations { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
