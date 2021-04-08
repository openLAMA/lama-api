using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUp;
using System;

namespace Elyon.Fastly.Api.PostgresRepositories.Entities
{
    public class InfoSessionFollowUp : BaseEntityWithId
    {
        public string Token { get; set; }

        public InfoSessionFollowUpStatus Status { get; set; }

        public Guid OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}
