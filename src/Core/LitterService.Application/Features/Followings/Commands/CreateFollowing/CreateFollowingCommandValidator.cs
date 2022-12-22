using FluentValidation;

namespace LitterService.Application.Features.Followings.Commands.CreateFollowing
{
    public class CreateFollowingCommandValidator : AbstractValidator<CreateFollowingCommand>
    {
        public CreateFollowingCommandValidator()
        {
            RuleFor(x => x.Followed).NotEmpty().WithMessage("The {PropertyName} cannot be empty.");
            RuleFor(x => x.Follower).NotEmpty().WithMessage("The {PropertyName} cannot be empty.");
            RuleFor(x => x.Followed).NotEqual(x => x.Follower).WithMessage("User cannot follow itself.");
        }
    }
}