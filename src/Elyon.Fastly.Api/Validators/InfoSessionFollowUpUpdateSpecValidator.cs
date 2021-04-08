using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUp;
using FluentValidation;

namespace Elyon.Fastly.Api.Validators
{
    public class InfoSessionFollowUpUpdateSpecValidator : AbstractValidator<InfoSessionFollowUpUpdateSpecDto>
    {
        public InfoSessionFollowUpUpdateSpecValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("Must provide the token.");
        }
    }
}
