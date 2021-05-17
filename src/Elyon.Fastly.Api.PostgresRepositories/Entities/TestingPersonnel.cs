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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Elyon.Fastly.Api.PostgresRepositories.Entities
{
    public class TestingPersonnel : BaseEntityWithId
    {
        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public Guid StatusId { get; set; }

        public virtual TestingPersonnelStatus Status { get; set; }

        public Employeer Employeer { get; set; }

        public bool HasFixedWorkingDays { get; set; }

        public TestingPersonnelType Type { get; set; }

        public Shift MondayShift { get; set; }

        public Shift TuesdayShift { get; set; }

        public Shift WednesdayShift { get; set; }

        public Shift ThursdayShift { get; set; }

        public Shift FridayShift { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public virtual ICollection<TestingPersonnelWorkingArea> TestingPersonnelWorkingAreas { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CA2227 // Collection properties should be read only
        public virtual ICollection<TestingPersonnelConfirmation> TestingPersonnelConfirmations { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CA2227 // Collection properties should be read only
        public virtual ICollection<FixedTestingPersonnelCancelation> FixedTestingPersonnelCancelations { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
