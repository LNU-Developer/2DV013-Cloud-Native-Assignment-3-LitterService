using System;
using System.Threading;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using MediatR;

namespace LitterService.Application.Features.Followings.Commands.DeleteFollowing
{

    public class DeleteFollowingCommandHandler : IRequestHandler<DeleteFollowingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFollowingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteFollowingCommand request, CancellationToken cancellationToken)
        {
            var following = await _unitOfWork.Followings.GetWithFilterAsync(x =>
                x.FollowingUserId == request.Follower &&
                x.FollowedUserId == request.Followed);

            if (following == null) return Unit.Value;
            following.IsDeleted = true;
            following.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}