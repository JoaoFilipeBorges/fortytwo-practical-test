using Fortytwo.PracticalTest.Application.ReadModel;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.GetTodos;

public record GetTodosQuery(
    int Page,
    int PageSize
    ) : IRequest<PagedList<TodoDto>>;