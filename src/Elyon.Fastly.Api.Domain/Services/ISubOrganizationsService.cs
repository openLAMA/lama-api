
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using System;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Domain.Services
{
    public interface ISubOrganizationsService : IBaseCrudService<SubOrganizationDto>
    {
        Task<SubOrganizationDto> CreateSubOrganizationAsync(Guid organizationId, SubOrganizationSpecDto dto);
        Task UpdateSubOrganizationAsync(Guid organizationId, Guid id, SubOrganizationDto dto);
        Task DeleteSubOrganizationAsync(Guid organizationId, Guid id);
    }
}
