using FluentValidation;

namespace LitterService.Application.Features.Lits.Commands.CreateLit
{
    public class CreateLitCommandValidator : AbstractValidator<CreateLitCommand>
    {
        public CreateLitCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The {PropertyName} cannot be empty.");
            RuleFor(x => x.Message).NotEmpty().MaximumLength(42).WithMessage("The value {PropertyValue} length is above 42 for {PropertyName}.");
        }
    }
}