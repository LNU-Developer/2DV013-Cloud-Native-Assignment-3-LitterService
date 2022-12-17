using System;

namespace LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId.Dtos
{
    public sealed record FollowingDto
    {
        public FollowingDto(Guid follower, Guid followed, DateTime createdAt, DateTime updatedAt)
        {
            Follower = follower;
            Followed = followed;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public Guid Follower { get; private set; }
        public Guid Followed { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

    }
}