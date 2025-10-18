using Fortytwo.PracticalTest.Application.Interfaces;
using Fortytwo.PracticalTest.Application.ReadModel;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.GetTodos;

public class GetTodosQueryHandler(ITodoRepository todoRepository) : IRequestHandler<GetTodosQuery, IList<Todo>>
{
    public async Task<IList<Todo>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return await todoRepository.Get(request.Page, request.PageSize, cancellationToken);
    }
}