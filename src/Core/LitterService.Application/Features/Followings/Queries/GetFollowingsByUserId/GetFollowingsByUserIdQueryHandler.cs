using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId.Dtos;
using MediatR;

namespace LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId
{

    public class GetFollowingsByUserIdQueryHandler : IRequestHandler<GetFollowingsByUserIdQuery, List<FollowingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetFollowingsByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<FollowingDto>> Handle(GetFollowingsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Followings.GetFollowingsByUserId(request.Id);
        }
    }
}