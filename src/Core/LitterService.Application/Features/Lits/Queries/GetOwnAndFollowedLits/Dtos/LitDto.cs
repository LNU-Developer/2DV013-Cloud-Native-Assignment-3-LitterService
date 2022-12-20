using System;

namespace LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits.Dtos
{
    public sealed record LitDto
    {
        public LitDto(int id, string message, DateTime createdAt, DateTime editedAt)
        {
            Id = id;
            Message = message;
            CreatedAt = createdAt;
            EditedAt = editedAt;
        }
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime EditedAt { get; private set; }
        public string Message { get; private set; }

    }
}