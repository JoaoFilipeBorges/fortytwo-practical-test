using Fortytwo.PracticalTest.Api.Contracts.Todos;
using Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;
using Fortytwo.PracticalTest.Application.Features.Todos.GetTodoById;
using Fortytwo.PracticalTest.Application.Features.Todos.GetTodos;
using Fortytwo.PracticalTest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fortytwo.PracticalTest.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class TodoController(IMediator mediator) : PraticalTestBaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetTodosQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Get a todo by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await mediator.Send(new GetTodoByIdQuery(id));
        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTodoRequest request)
    {
        var command = new CreateTodoCommand(
            request.Title, 
            request.Description, 
            request.Done, 
            request.DueDate, 
            request.Assignee,
            new RequestUserInfo{ UserName = GetUsername() });
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }
    
    
}