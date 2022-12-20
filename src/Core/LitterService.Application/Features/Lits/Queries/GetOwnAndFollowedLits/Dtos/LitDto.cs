using System;

namespace LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits.Dtos
{
    public sealed record LitDto
    {
        public LitDto(Guid creatorId, string message, DateTime createdAt, DateTime editedAt)
        {
            CreatorId = creatorId;
            Message = message;
            CreatedAt = createdAt;
            EditedAt = editedAt;
        }
        public Guid CreatorId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime EditedAt { get; private set; }
        public string Message { get; private set; }

    }
}