
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.Domain.Services;
using System;
using System.Threading.Tasks;
using Prime.Sdk.Logging;

namespace Elyon.Fastly.Api.DomainServices
{
    public class SubOrganizationsService : BaseCrudService<SubOrganizationDto>, ISubOrganizationsService
    {

        private readonly ISubOrganizationsRepository _subOrganizationsRepository;
        private readonly IOrganizationsRepository _organizationsRepository;
        private readonly ILog _log;

        public SubOrganizationsService(ISubOrganizationsRepository subOrganizationsRepository, 
            IOrganizationsRepository organizationsRepository, 
            ILogFactory logFactory)
            : base(subOrganizationsRepository)
        {
            if (logFactory == null)
                throw new ArgumentNullException(nameof(logFactory));

            _subOrganizationsRepository = subOrganizationsRepository;
            _organizationsRepository = organizationsRepository;
            _log = logFactory.CreateLog(this);
        }

        public async Task<SubOrganizationDto> CreateSubOrganizationAsync(Guid organizationId, SubOrganizationSpecDto specDto)
        {
            if (specDto == null)
            {
                return null;
            }
            specDto.OrganizationId = organizationId;
            var createdDto = await AddAsync<SubOrganizationSpecDto>(specDto)
                .ConfigureAwait(false);
            await _organizationsRepository
                .UpdateTimeStampAsync(organizationId)
                .ConfigureAwait(false);
            return createdDto;
        }

        public async Task UpdateSubOrganizationAsync(Guid organizationId, Guid id, SubOrganizationDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            
            var subOrganizationInDb = await _subOrganizationsRepository
                .GetByIdAsync(id)
                .ConfigureAwait(false);
            if (subOrganizationInDb == null)
            {
                var message = "Invalid organizationId or Id";
                throw new ArgumentNullException(message);
            }
            if (subOrganizationInDb.OrganizationId != organizationId || subOrganizationInDb.Id != id)
            {
                var message = "Invalid organizationId or Id";
                throw new ArgumentNullException(message);
            }
            dto.OrganizationId = organizationId;
            dto.Id = id;
            await UpdateAsync(dto).ConfigureAwait(false);
            await _organizationsRepository
                .UpdateTimeStampAsync(organizationId)
                .ConfigureAwait(false);
        }

        public async Task DeleteSubOrganizationAsync(Guid organizationId, Guid id)
        {
            var subOrganizationInDb = await _subOrganizationsRepository
                .GetByIdAsync(id)
                .ConfigureAwait(false);
            if (subOrganizationInDb == null)
            {
                var message = "Invalid organizationId or Id";
                throw new ArgumentNullException(message);
            }
            if (subOrganizationInDb.OrganizationId != organizationId || subOrganizationInDb.Id != id)
            {
                var message = "Invalid organizationId or Id";
                throw new ArgumentNullException(message);
            }
            await DeleteAsync(subOrganizationInDb).ConfigureAwait(false);
            await _organizationsRepository
                .UpdateTimeStampAsync(organizationId)
                .ConfigureAwait(false);
        }
    }
}
