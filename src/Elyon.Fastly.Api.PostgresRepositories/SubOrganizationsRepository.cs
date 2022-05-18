
using AutoMapper;
using Elyon.Fastly.Api.Domain.Dtos.Organizations;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class SubOrganizationsRepository : BaseCrudRepository<SubOrganization, SubOrganizationDto>, ISubOrganizationsRepository
    {

        public SubOrganizationsRepository(
            Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper)
            : base(contextFactory, mapper)
        {

        }
    }
}
