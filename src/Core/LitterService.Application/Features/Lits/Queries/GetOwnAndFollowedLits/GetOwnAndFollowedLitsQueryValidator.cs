using FluentValidation;

namespace LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits
{
    public class GetOwnAndFollowedLitsQueryValidator : AbstractValidator<GetOwnAndFollowedLitsQuery>
    {
        public GetOwnAndFollowedLitsQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The {PropertyName} cannot be empty.");
        }
    }
}