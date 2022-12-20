using System;
using System.Collections.Generic;
using LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits.Dtos;
using MediatR;

namespace LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits
{
    public sealed record GetOwnAndFollowedLitsQuery : IRequest<List<LitDto>>
    {
        public GetOwnAndFollowedLitsQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}