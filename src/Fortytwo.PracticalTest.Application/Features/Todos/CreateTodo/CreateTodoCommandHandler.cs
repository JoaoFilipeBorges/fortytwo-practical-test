using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Domain.Entities;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;

public class CreateTodoCommandHandler(ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, int>
{
    public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            Done = request.Done,
            AssigneeId = request.Assignee
        };
        await todoRepository.Create(todo, cancellationToken);

        return todo.Id;
    }
}