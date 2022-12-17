using System;
using System.Threading;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using LitterService.Domain.Entities;
using MediatR;

namespace LitterService.Application.Features.Followings.Commands.CreateFollowing
{

    public class CreateFollowingCommandHandler : IRequestHandler<CreateFollowingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateFollowingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreateFollowingCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Followings.ExistsAsync(x =>
                x.FollowingUserId == request.Follower &&
                x.FollowedUserId == request.Followed)
            )
                return Unit.Value;
            await _unitOfWork.Followings.AddAsync(new Following
            {
                FollowingUserId = request.Follower,
                FollowedUserId = request.Followed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}