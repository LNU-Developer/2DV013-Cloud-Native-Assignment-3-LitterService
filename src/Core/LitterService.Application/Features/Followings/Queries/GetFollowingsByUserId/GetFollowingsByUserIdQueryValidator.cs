using FluentValidation;

namespace LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId
{
    public class GetFollowingsByUserIdQueryValidator : AbstractValidator<GetFollowingsByUserIdQuery>
    {
        public GetFollowingsByUserIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The {PropertyName} cannot be empty.");
        }
    }
}