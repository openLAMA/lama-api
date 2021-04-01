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
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using Prime.Sdk.Logging;
using System;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.DomainServices
{
    public class OrganizationNotesService : BaseCrudService<OrganizationNoteDto>, IOrganizationNotesService
    {
        private readonly ILog _log;
        private readonly IUsersRepository _usersRepository;

        public OrganizationNotesService(IOrganizationNotesRepository organizationNotesRepository,
            IUsersRepository userRepository, ILogFactory logFactory)
            :base(organizationNotesRepository)
        {
            if (logFactory == null)
                throw new ArgumentNullException(nameof(logFactory));

            _log = logFactory.CreateLog(this);
            _usersRepository = userRepository;
        }

        public async Task<OrganizationNoteDto> AddAsync(OrganizationNoteSpecDto item, Guid creatorId)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (creatorId == default)
                throw new ArgumentNullException(nameof(item));

            var creator = await _usersRepository.GetByIdAsync(creatorId).ConfigureAwait(false);

            OrganizationNoteDto newNote = new OrganizationNoteDto
            {
                Text = item.Text,
                OrganizationId = item.OrganizationId,
                CreatorName = creator.Name,
                UserId = creatorId,
            };

            return await base.AddAsync(newNote)
                .ConfigureAwait(false);
        }
    }
}
