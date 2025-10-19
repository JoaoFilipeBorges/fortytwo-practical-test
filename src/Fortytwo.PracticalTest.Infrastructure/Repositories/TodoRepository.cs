using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Application.ReadModel.Mappings;
using Fortytwo.PracticalTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fortytwo.PracticalTest.Infrastructure.Repositories;

using Domain.Entities;
using Application.ReadModel;

public class TodoRepository(PracticalTestDbContext dbContext) : ITodoRepository
{

    public async Task<TodoDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Todos.Select(t => t.ToDto())
            .FirstOrDefaultAsync(td => td.Id == id, cancellationToken);
    }
    
    public async Task<PagedList<TodoDto>> Get(int page, int pageSize, CancellationToken cancellationToken)
    {
        var totalItems = await dbContext.Todos.CountAsync(cancellationToken);
        var items = await dbContext.Todos
            .Skip((page - 1) * pageSize)
            .Take(pageSize).Select(t => t.ToDto())
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