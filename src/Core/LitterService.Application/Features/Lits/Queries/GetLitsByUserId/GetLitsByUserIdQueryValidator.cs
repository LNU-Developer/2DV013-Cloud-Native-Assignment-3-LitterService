using FluentValidation;

namespace LitterService.Application.Features.Lits.Queries.GetLitsByUserId
{
    public class GetLitsByUserIdQueryValidator : AbstractValidator<GetLitsByUserIdQuery>
    {
        public GetLitsByUserIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The {PropertyName} cannot be empty.");
        }
    }
}