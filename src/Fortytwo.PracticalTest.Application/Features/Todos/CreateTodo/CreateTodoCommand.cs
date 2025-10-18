using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;

public record CreateTodoCommand(
    string Title,
    string Description,
    bool Done,
    DateTime DueDate,
    int Assignee
) : IRequest<int>;
