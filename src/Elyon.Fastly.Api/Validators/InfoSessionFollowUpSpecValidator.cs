using Elyon.Fastly.Api.Domain.Dtos.InfoSessionFollowUps;
using FluentValidation;

namespace Elyon.Fastly.Api.Validators
{
    public class InfoSessionFollowUpSpecValidator : AbstractValidator<InfoSessionFollowUpSpecDto>
    {
        public InfoSessionFollowUpSpecValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message must not be null or empty space");

            RuleFor(x => x.Recievers)
                .NotEmpty()
                .WithMessage("Must have some email receivers.");

            RuleFor(x => x.Recievers)
                .ForEach(ruleBuilder => ruleBuilder.EmailAddress());
        }
    }
}
