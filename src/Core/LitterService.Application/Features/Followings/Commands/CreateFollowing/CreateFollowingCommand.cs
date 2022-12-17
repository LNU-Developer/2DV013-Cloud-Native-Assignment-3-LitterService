using System;
using MediatR;

namespace LitterService.Application.Features.Followings.Commands.CreateFollowing
{
    public sealed record CreateFollowingCommand : IRequest
    {
        public CreateFollowingCommand(Guid follower, Guid followed)
        {
            Follower = follower;
            Followed = followed;
        }
        public Guid Follower { get; private set; }
        public Guid Followed { get; private set; }
    }
}