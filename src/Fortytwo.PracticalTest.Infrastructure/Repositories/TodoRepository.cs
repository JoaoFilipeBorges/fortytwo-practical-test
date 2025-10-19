using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fortytwo.PracticalTest.Infrastructure.Repositories;

using Domain.Entities;
using Application.ReadModel;

public class TodoRepository(PracticalTestDbContext dbContext) : ITodoRepository
{

    public async Task<TodoDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Todos.Select(todo => new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description ?? "",
                DueDate = todo.DueDate,
                Done = todo.Done,
                Assignee = todo.Assignee != null ? new UserDto
                {
                    Id = todo.AssigneeId.Value,
                    UserName = todo.Assignee.UserName
                } : null,
                Creator = new UserDto
                {
                    Id = todo.CreatedBy,
                    UserName = todo.Author.UserName
                }
            })
            .FirstOrDefaultAsync(td => td.Id == id, cancellationToken);
    }
    
    public async Task<PagedList<TodoDto>> Get(int page, int pageSize, CancellationToken cancellationToken)
    {
        var totalItems = await dbContext.Todos.CountAsync(cancellationToken);
        var items = await dbContext.Todos
            .Skip((page - 1) * pageSize)
            .Take(pageSize).Select(todo => new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description ?? "",
                DueDate = todo.DueDate,
                Done = todo.Done,
                Assignee = todo.Assignee != null ? new UserDto
                {
                    Id = todo.AssigneeId.Value,
                    UserName = todo.Assignee.UserName
                } : null,
                Creator = new UserDto
                {
                    Id = todo.CreatedBy,
                    UserName = todo.Author.UserName
                }
            })
            .ToListAsync(cancellationToken);
        
        return new PagedList<TodoDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
    }

    public async Task Create(Todo todo, CancellationToken cancellationToken)
    {
        await dbContext.Todos.AddAsync(todo, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}