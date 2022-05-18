using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using System.Threading.Tasks;
using System;

namespace Elyon.Fastly.Api.Domain.Repositories
{
    public interface ISubOrganizationsRepository : IBaseCrudRepository<SubOrganizationDto>
    {
    }
}
