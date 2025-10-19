using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Application.ReadModel;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.GetTodos;

public class GetTodosQueryHandler(ITodoRepository todoRepository) : IRequestHandler<GetTodosQuery, PagedList<TodoDto>>
{
    public async Task<PagedList<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return await todoRepository.Get(request.Page, request.PageSize, cancellationToken);
    }
}