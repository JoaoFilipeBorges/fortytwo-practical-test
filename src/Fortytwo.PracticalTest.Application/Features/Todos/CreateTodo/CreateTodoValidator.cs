using FluentValidation;

namespace Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;

public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        //RuleFor(x => x.DueDate).
    }
}