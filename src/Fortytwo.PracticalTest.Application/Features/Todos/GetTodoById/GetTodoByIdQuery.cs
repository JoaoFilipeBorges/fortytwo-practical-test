using Fortytwo.PracticalTest.Application.ReadModel;
using MediatR;

namespace Fortytwo.PracticalTest.Application.Features.Todos.GetTodoById;

public record GetTodoByIdQuery(
    int Id) : IRequest<TodoDto>;