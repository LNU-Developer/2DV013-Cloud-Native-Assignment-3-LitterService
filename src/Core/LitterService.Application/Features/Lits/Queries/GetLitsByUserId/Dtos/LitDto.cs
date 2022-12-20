using System;

namespace LitterService.Application.Features.Lits.Queries.GetLitsByUserId.Dtos
{
    public sealed record LitDto
    {
        public LitDto(string message, DateTime createdAt, DateTime editedAt)
        {
            Message = message;
            CreatedAt = createdAt;
            EditedAt = editedAt;
        }
        public DateTime CreatedAt { get; private set; }
        public DateTime EditedAt { get; private set; }
        public string Message { get; private set; }

    }
}