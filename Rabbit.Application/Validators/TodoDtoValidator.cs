using FluentValidation;
using Rabbit.Application.DTOs;

namespace Rabbit.Application.Validators;

public class TodoDtoValidator : AbstractValidator<TodoDto>
{
    public TodoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage("Title can't be more than 100 characters.");

        RuleFor(x => x.IsComplete)
            .NotNull()
            .WithMessage("IsComplete is required.");

        RuleFor(x => x.GuidId)
            .NotEmpty()
            .WithMessage("Guid id is required.");
    }
}