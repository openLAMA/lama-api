using System;

namespace Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUp
{
    public class InfoSessionFollowUpUpdateSpecDto
    {
        public string Token { get; set; }
        public bool IsAccepted { get; set; }
    }
}
