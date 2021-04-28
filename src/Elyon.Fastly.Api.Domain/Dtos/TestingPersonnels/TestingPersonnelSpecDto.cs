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

using Elyon.Fastly.Api.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Elyon.Fastly.Api.Domain.Dtos.TestingPersonnels
{
    public class TestingPersonnelSpecDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Guid StatusId { get; set; }

        public Employeer Employeer { get; set; }

        [Required]
        public bool HasFixedWorkingDays { get; set; }

        [Required]
        public Shift MondayShift { get; set; }

        [Required]
        public Shift TuesdayShift { get; set; }

        [Required]
        public Shift WednesdayShift { get; set; }

        [Required]
        public Shift ThursdayShift { get; set; }

        [Required]
        public Shift FridayShift { get; set; }

        [Required]
#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<TestingPersonnelWorkingAreaSpecDto> WorkingAreas { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
