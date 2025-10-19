using Fortytwo.PracticalTest.Application.Interfaces.Http;
using Fortytwo.PracticalTest.Application.ReadModel.External;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fortytwo.PracticalTest.Infrastructure.Http;

public class ExternalTodoHttpClient(
    IOptions<ExternalTodoHttpClientOptions> configuration,
    HttpClient httpClient,
    IMemoryCache memoryCache) : IExternalTodoHttpClient
{
    
    public async Task<ExternalTodo?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (memoryCache.TryGetValue(id, out ExternalTodo externalTodo))
        {
            return externalTodo;
        }
        
        try
        {
            var response = await httpClient.GetAsync($"{configuration.Value.BaseAddress}/{id}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                
            }

            var todo = JsonConvert.DeserializeObject<ExternalTodo>(
                await response.Content.ReadAsStringAsync(cancellationToken));
            if (todo is not null)
            {
                memoryCache.Set(id, todo, TimeSpan.FromSeconds(configuration.Value.CacheDurationInSeconds));
            }
            
            
            return todo;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}