using System;
using System.Collections.Generic;
using LitterService.Application.Features.Lits.Queries.GetLitsByUserId.Dtos;
using MediatR;

namespace LitterService.Application.Features.Lits.Queries.GetLitsByUserId
{
    public sealed record GetLitsByUserIdQuery : IRequest<List<LitDto>>
    {
        public GetLitsByUserIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}