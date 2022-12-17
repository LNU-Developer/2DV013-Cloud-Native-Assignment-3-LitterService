using System;
using System.Threading;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using LitterService.Domain.Entities;
using MediatR;

namespace LitterService.Application.Features.Lits.Commands.CreateLit
{

    public class CreateLitCommandHandler : IRequestHandler<CreateLitCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateLitCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreateLitCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Lits.AddAsync(new Lit
            {
                Message = request.Message,
                CreatedByUserId = request.Id,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}