using Fortytwo.PracticalTest.Application.ReadModel;
using Fortytwo.PracticalTest.Domain.Entities;

namespace Fortytwo.PracticalTest.Application.Interfaces.Persistence;

public interface ITodoRepository
{
    public Task<TodoDto?> GetById(int id, CancellationToken cancellationToken);

    public Task<PagedList<TodoDto>> Get(int page, int pageSize, CancellationToken cancellationToken);

    public Task Create(Todo todo, CancellationToken cancellationToken);
}