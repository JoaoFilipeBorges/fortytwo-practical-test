using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Application.ReadModel;
using Fortytwo.PracticalTest.Domain.Exceptions;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.GetTodoById;

public class GetTodoByIdQueryHandler(
    ITodoRepository todoRepository) : IRequestHandler<GetTodoByIdQuery, Todo>
{
    public async Task<Todo> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.GetById(request.Id, cancellationToken);
        if (todo is null)
        {
            throw new ResourceNotFoundException($"Could not find todo with id: {request.Id}");
        }

        return todo;
    }
}