using System;
using System.Collections.Generic;
using LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId.Dtos;
using MediatR;

namespace LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId
{
    public sealed record GetFollowingsByUserIdQuery : IRequest<List<FollowingDto>>
    {
        public GetFollowingsByUserIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}