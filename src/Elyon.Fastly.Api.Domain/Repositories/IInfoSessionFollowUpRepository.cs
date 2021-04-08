using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUp;
using System;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Domain.Repositories
{
    public interface IInfoSessionFollowUpRepository
    {
        Task<string> AddInfoSessionFollowUpAsync(Guid organizationId);

        Task UpdateStatusAsync(string token, InfoSessionFollowUpStatus newStatus);

        Task<string> GetTokenByOrganizationIdAsync(Guid organizationId);

        Task<InfoSessionFollowUpStatus> GetStatusByTokenAsync(string token);
    }
}
