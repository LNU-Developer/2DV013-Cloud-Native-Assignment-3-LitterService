using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using LitterService.Application.Features.Lits.Queries.GetLitsByUserId.Dtos;
using MediatR;

namespace LitterService.Application.Features.Lits.Queries.GetLitsByUserId
{

    public class GetLitsByUserIdQueryHandler : IRequestHandler<GetLitsByUserIdQuery, List<LitDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetLitsByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<LitDto>> Handle(GetLitsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var lits = await _unitOfWork.Lits.FindAsync(x => x.CreatedByUserId == request.Id);
            var dtos = lits.Where(x => x.IsDeleted == false).Select(x => new LitDto(x.Message, x.CreatedAt, x.UpdatedAt)).ToList();
            return dtos;
        }
    }
}