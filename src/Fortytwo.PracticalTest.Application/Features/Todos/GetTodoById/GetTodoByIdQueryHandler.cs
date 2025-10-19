using Fortytwo.PracticalTest.Application.Interfaces.Http;
using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Application.ReadModel;
using Fortytwo.PracticalTest.Domain.Exceptions;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.GetTodoById;

public class GetTodoByIdQueryHandler(
    IExternalTodoHttpClient externalTodoHttpClient,
    ITodoRepository todoRepository) : IRequestHandler<GetTodoByIdQuery, TodoDto>
{
    public async Task<TodoDto> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.GetById(request.Id, cancellationToken);
        if (todo is null)
        {
            throw new ResourceNotFoundException($"Could not find todo with id: {request.Id}");
        }

        var externalTodo = await externalTodoHttpClient.GetByIdAsync(todo.Id, cancellationToken);
        if (externalTodo is not null)
        {
            todo.ExternalTitle = externalTodo.Title;
        }

        return todo;
    }
}