using System;
using System.Collections.Generic;

namespace Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUps
{
    public class InfoSessionFollowUpSpecDto
    {
        public IEnumerable<string> Recievers { get; set; }
        public string Message { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
