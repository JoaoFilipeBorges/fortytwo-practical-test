using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fortytwo.PracticalTest.Infrastructure.Repositories;

using Fortytwo.PracticalTest.Domain.Entities;
using ReadModel = Fortytwo.PracticalTest.Application.ReadModel;

public class TodoRepository(PracticalTestDbContext dbContext) : ITodoRepository
{

    public async Task<ReadModel.Todo?> GetById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Todos.Select(t => new ReadModel.Todo
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Done = t.Done,
                Assignee = new ReadModel.User
                {
                    Id = t.AssigneeId,
                    UserName = t.Assignee.UserName
                },
                Creator = new ReadModel.User
                {
                    Id = t.CreatedBy,
                    UserName = t.Author.UserName
                }
            })
            .FirstOrDefaultAsync(td => td.Id == id, cancellationToken);
    }
    
    public async Task<IList<ReadModel.Todo>> Get(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Todos
            .Skip((page - 1) * pageSize)
            .Take(pageSize).Select(t => new ReadModel.Todo
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Done = t.Done,
                Assignee = new ReadModel.User
                {
                    Id = t.AssigneeId,
                    UserName = t.Assignee.UserName
                },
                Creator = new ReadModel.User
                {
                    Id = t.CreatedBy,
                    UserName = t.Author.UserName
                }
            }).ToListAsync(cancellationToken);
    }

    public async Task Create(Todo todo, CancellationToken cancellationToken)
    {
        await dbContext.Todos.AddAsync(todo, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}