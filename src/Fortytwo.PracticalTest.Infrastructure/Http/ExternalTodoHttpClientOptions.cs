namespace Fortytwo.PracticalTest.Infrastructure.Http;

public class ExternalTodoHttpClientOptions
{
    public string BaseAddress { get; init; }
    public int CacheDurationInSeconds { get; init; }
}