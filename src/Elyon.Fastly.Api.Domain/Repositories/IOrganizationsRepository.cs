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

using Elyon.Fastly.Api.Domain.Dtos.EpaadDtos;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Domain.Repositories
{
    public interface IOrganizationsRepository : IBaseCrudRepository<OrganizationDto>
    {
        Task<bool> IsUserPartOfOrganizationAsync(Guid organizationId, Guid userId);

        Task<OrganizationDashboardDto> GetOrganizationDashboardInfoAsync(Guid id);

        Task ChangeOrganizationStatusAsync(Guid userId, OrganizationStatus status);

        Task<OrganizationProfileDto> GetOrganizationProfileAsync(Guid id);

        Task UpdateOrganizationProfileAsync(OrganizationProfileSpecDto organizationDto);

        Task<PagedResults<OrganizationDto>> GetAllOrganizationsAsync(
            Paginator paginator,
            Expression<Func<OrganizationDto, bool>> dtoFilter,
            string orderBy,
            bool isAscending);

        Task<DateTime> GetOrganizationCreationDateAsync(Guid id);

        Task UpdateEpaadIdAsync(int epaadId, Guid organizationId);

        Task ChangeOrganizationActiveStatusAsync(OrganizationActiveStatusDto dto);

        Task<OrganizationStatus> GetOrganizationStatusByOrganizationIdAsync(Guid organizationId);

        Task<OrganizationStatusCalculationDto> GetOrganizationStatusCalculationDataAsync(Guid organizationId);

        Task<List<OrganizationEpaadInfoDto>> GetOrganizationsWithEpaadIdAsync();

        Task UpdateRegisteredEmployeesAndShortNamePerOrganizationAsync(EpaadOrganizationDto dto);

        Task UpdateOrganizationStatusAsync(Guid organizationId, OrganizationStatus status);

        Task<int?> GetOrganizationEpaadIdAsync(Guid organizationId);
    }
}
