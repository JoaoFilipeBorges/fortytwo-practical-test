namespace Fortytwo.PracticalTest.Infrastructure.Http;

public class ExternalTodoHttpClientOptions
{
    public string BaseAddress { get; set; }
    public int CacheDurationInSeconds { get; set; }
}