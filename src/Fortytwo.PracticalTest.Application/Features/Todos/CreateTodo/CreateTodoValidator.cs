using FluentValidation;

namespace Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;

public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        
        RuleFor(x => x.Assignee)
            .GreaterThan(0)
            .When(x => x.Assignee.HasValue)
            .WithMessage("Assignee must be greater than 0 when provided.");
        
        RuleFor(x => x.UserInfo)
            .NotNull().WithMessage("User information is required.")
            .DependentRules(() =>
            {
                RuleFor(x => x.UserInfo.UserName)
                    .NotEmpty()
                    .WithMessage("UserName in UserInfo cannot be empty.");
            });
    }
}