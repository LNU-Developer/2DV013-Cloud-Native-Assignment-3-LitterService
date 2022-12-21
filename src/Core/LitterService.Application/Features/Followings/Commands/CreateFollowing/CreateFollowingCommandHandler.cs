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
            var oldFollow = await _unitOfWork.Followings.GetWithFilterAsync(x =>
                x.FollowingUserId == request.Follower &&
                x.FollowedUserId == request.Followed);
            if (oldFollow is not null)
            {
                oldFollow.IsDeleted = false;
                await _unitOfWork.CompleteAsync();
                return Unit.Value;
            }

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