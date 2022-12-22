using FluentValidation;

namespace LitterService.Application.Features.Followings.Commands.DeleteFollowing
{
    public class DeleteFollowingCommandValidator : AbstractValidator<DeleteFollowingCommand>
    {
        public DeleteFollowingCommandValidator()
        {
            RuleFor(x => x.Followed).NotEmpty().WithMessage("The {PropertyName} cannot be empty.");
            RuleFor(x => x.Follower).NotEmpty().WithMessage("The {PropertyName} cannot be empty.");
        }
    }
}