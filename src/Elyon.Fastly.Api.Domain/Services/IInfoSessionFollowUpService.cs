using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUp;
using System.Threading.Tasks;

namespace Elyon.Fastly.Api.Domain.Services
{
    public interface IInfoSessionFollowUpService : IBaseService
    {
        Task SendInfoSessionFollowUpEmailAsync(InfoSessionFollowUpSpecDto specDto);

        Task ChangeFollowUpStatusAsync(InfoSessionFollowUpUpdateSpecDto specDto);
    }
}
