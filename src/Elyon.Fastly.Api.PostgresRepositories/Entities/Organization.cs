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
using Elyon.Fastly.Api.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elyon.Fastly.Api.PostgresRepositories.Entities
{
    public class Organization : BaseEntityWithId
    {
        public int? EpaadId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int OrganizationTypeId { get; set; }

        public virtual OrganizationType OrganizationType { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime LastUpdatedOn { get; set; }

        public Guid CityId { get; set; }

        public virtual City City { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        public DateTime? TrainingTimestamp { get; set; }

        public DateTime? OnboardingTimestamp { get; set; }

        public DateTime? FirstTestTimestamp { get; set; }

        public DateTime? SecondTestTimestamp { get; set; }

        public DateTime? ThirdTestTimestamp { get; set; }

        public DateTime? FourthTestTimestamp { get; set; }

        public DateTime? FifthTestTimestamp { get; set; }

        public DateTime? ExclusionStartDate { get; set; }

        public DateTime? ExclusionEndDate { get; set; }

        [Required]
        public int NumberOfSamples { get; set; }

        public int? NumberOfPools { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public virtual ICollection<User> Contacts { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CA2227 // Collection properties should be read only
        public virtual ICollection<SubOrganization> SubOrganizations { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CA2227 // Collection properties should be read only
        public virtual ICollection<OrganizationNote> Notes { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        [ForeignKey("SupportPersonId")]
        public Guid SupportPersonId { get; set; }

        public virtual User SupportPerson { get; set; }

        public OrganizationStatus Status { get; set; }

        [MaxLength(100)]
        public string Manager { get; set; }

        public int? StudentsCount { get; set; }

        public int? EmployeesCount { get; set; }

        public int? RegisteredEmployees { get; set; }

        [MaxLength(100)]
        public string Area { get; set; }

        [MaxLength(100)]
        public string County { get; set; }

        public int? PrioLogistic { get; set; }

        public SchoolType? SchoolType { get; set; }

        public int? NumberOfBags { get; set; }

        public int? NaclLosing { get; set; }

        public int? AdditionalTestTubes { get; set; }

        public int? NumberOfRakoBoxes { get; set; }

        [MaxLength(200)]
        public string PickupLocation { get; set; }

        [MaxLength(100)]
        public string OrganizationShortcutName { get; set; }

        public Guid? InfoSessionFollowUpId { get; set; }

        public InfoSessionFollowUp InfoSessionFollowUp { get; set; }
    }
}
