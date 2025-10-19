using FluentValidation;
using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Domain.Entities;
using Fortytwo.PracticalTest.Domain.Exceptions;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;

public class CreateTodoCommandHandler(
    IUserRepository userRepository,
    ITodoRepository todoRepository,
    IValidator<CreateTodoCommand> validator) : IRequestHandler<CreateTodoCommand, int>
{
    public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await userRepository.GetUserIdByUserName(request.UserInfo.UserName, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException($"User with username {request.UserInfo.UserName} not found.");
        }
        
        var todo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            Done = request.Done,
            AssigneeId = request.Assignee,
            CreatedBy = user.Id,
            CreatedOn = DateTime.UtcNow,
            UpdatedBy = user.Id,
            UpdatedOn = DateTime.UtcNow,
            Author = user
        };
        await todoRepository.Create(todo, cancellationToken);

        return todo.Id;
    }
}