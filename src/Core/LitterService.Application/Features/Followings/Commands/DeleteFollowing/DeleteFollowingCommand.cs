using System;
using MediatR;

namespace LitterService.Application.Features.Followings.Commands.DeleteFollowing
{
    public sealed record DeleteFollowingCommand : IRequest
    {
        public DeleteFollowingCommand(Guid follower, Guid followed)
        {
            Follower = follower;
            Followed = followed;
        }
        public Guid Follower { get; private set; }
        public Guid Followed { get; private set; }
    }
}