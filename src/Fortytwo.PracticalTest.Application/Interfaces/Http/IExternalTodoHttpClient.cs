namespace Fortytwo.PracticalTest.Application.Interfaces.Http;

public interface IPraticalTestHttpClient<T>
{
    Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);
}