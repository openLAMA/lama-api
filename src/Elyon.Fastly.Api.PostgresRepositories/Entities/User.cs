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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Elyon.Fastly.Api.PostgresRepositories.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntityWithId
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string LandLineNumber { get; set; }

        public Guid? LamaCompanyId { get; set; }

        public virtual LamaCompany LamaCompany { get; set; }

        public Guid? OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<Organization> SupportOrganizations { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

#pragma warning disable CA2227 // Collection properties should be read only
        public ICollection<SupportPersonOrgTypeDefaultMapping> SupportPersonOrgTypeDefaults { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
