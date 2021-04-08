using AutoMapper;
using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUps;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class InfoSessionFollowUpRepository : BaseRepository, IInfoSessionFollowUpRepository
    {
        public InfoSessionFollowUpRepository(Prime.Sdk.Db.Common.IDbContextFactory<ApiContext> contextFactory, IMapper mapper) 
            : base(contextFactory, mapper)
        {
        }

        public async Task<string> AddInfoSessionFollowUpAsync(Guid organizationId)
        {
            var entity = new InfoSessionFollowUp
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                Token = Guid.NewGuid().ToString(),
                Status = InfoSessionFollowUpStatus.Sent
            };

            var context = ContextFactory.CreateDataContext(null);
            await context.Set<InfoSessionFollowUp>().AddAsync(entity).ConfigureAwait(false);
            context.Entry(entity).State = EntityState.Detached;

            var organizationItems = context.Set<Organization>();
            var organization = await organizationItems.AsNoTracking()
                    .FirstOrDefaultAsync(o => o.Id == entity.OrganizationId)
                    .ConfigureAwait(false);

            organization.InfoSessionFollowUpId = entity.Id;
            context.Entry(organization).Property(org => org.InfoSessionFollowUpId).IsModified = true;
            organizationItems.Update(organization);

            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity.Token;
        }

        public async Task UpdateStatusAsync(string token, InfoSessionFollowUpStatus newStatus)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));

            var context = ContextFactory.CreateDataContext(null);
            var items = context.Set<InfoSessionFollowUp>();
            var entityToUpdate = await items.AsNoTracking()
                .FirstOrDefaultAsync(item => item.Token == token)
                .ConfigureAwait(false);

            if (entityToUpdate != default)
            {
                entityToUpdate.Status = newStatus;
                context.Entry(entityToUpdate).Property(followUp => followUp.Status).IsModified = true;
                items.Update(entityToUpdate);

                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<string> GetTokenByOrganizationIdAsync(Guid organizationId)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Set<Organization>().AsNoTracking()
                .Include(item => item.InfoSessionFollowUp)
                .FirstOrDefaultAsync(item => item.Id == organizationId)
                .ConfigureAwait(false);

            return entity?.InfoSessionFollowUp.Token ?? null;
        }

        public async Task<InfoSessionFollowUpStatus> GetStatusByTokenAsync(string token)
        {
            await using var context = ContextFactory.CreateDataContext(null);

            var entity = await context.Set<InfoSessionFollowUp>().AsNoTracking()
                .FirstOrDefaultAsync(item => item.Token == token)
                .ConfigureAwait(false);

            return entity.Status;
        } 
    }
}
