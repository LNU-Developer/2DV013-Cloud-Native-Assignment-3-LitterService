using System;
using MediatR;

namespace LitterService.Application.Features.Lits.Commands.CreateLit
{
    public sealed record CreateLitCommand : IRequest
    {
        public CreateLitCommand(Guid id, string message)
        {
            Id = id;
            Message = message;
        }
        public Guid Id { get; private set; }
        public string Message { get; private set; }
    }
}