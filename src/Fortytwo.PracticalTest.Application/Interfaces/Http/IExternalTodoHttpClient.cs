using Fortytwo.PracticalTest.Application.ReadModel.External;

namespace Fortytwo.PracticalTest.Application.Interfaces.Http;

public interface IExternalTodoHttpClient
{
    Task<ExternalTodo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}