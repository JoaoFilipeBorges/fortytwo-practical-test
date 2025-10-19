using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Domain.Entities;
using Fortytwo.PracticalTest.Domain.Exceptions;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;

public class CreateTodoCommandHandler(
    IUserRepository userRepository,
    ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, int>
{
    public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
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