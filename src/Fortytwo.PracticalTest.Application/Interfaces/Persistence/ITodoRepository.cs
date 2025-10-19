using Fortytwo.PracticalTest.Domain.Entities;

namespace Fortytwo.PracticalTest.Application.Interfaces.Persistence;

public interface ITodoRepository
{
    public Task<ReadModel.Todo?> GetById(int id, CancellationToken cancellationToken);

    public Task<IList<ReadModel.Todo>> Get(int page, int pageSize, CancellationToken cancellationToken);

    public Task Create(Todo todo, CancellationToken cancellationToken);
}