using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits.Dtos;
using MediatR;

namespace LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits
{

    public class GetOwnAndFollowedLitsQueryHandler : IRequestHandler<GetOwnAndFollowedLitsQuery, List<LitDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOwnAndFollowedLitsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<LitDto>> Handle(GetOwnAndFollowedLitsQuery request, CancellationToken cancellationToken)
        {
            var followings = await _unitOfWork.Followings.FindAsync(x => x.FollowingUserId == request.Id);
            var lits = new List<LitDto>();
            foreach (var follow in followings)
            {
                var dbLit = await _unitOfWork.Lits.FindAsync(x => x.IsDeleted == false && x.CreatedByUserId == follow.FollowedUserId);
                foreach (var lit in dbLit)
                    lits.Add(new LitDto(lit.Id, lit.Message, lit.CreatedAt, lit.UpdatedAt));
            }
            return lits.OrderBy(x => x.EditedAt).ToList();
        }
    }
}