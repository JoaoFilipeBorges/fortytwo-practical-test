using Fortytwo.PracticalTest.Application.Interfaces.Http;
using Fortytwo.PracticalTest.Application.ReadModel.External;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Fortytwo.PracticalTest.Infrastructure.Http;

public class ExternalTodoHttpClient(
    int cacheDurationInSeconds,
    string address,
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
            var response = await httpClient.GetAsync($"{address}/{id}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                
            }

            var todo = JsonConvert.DeserializeObject<ExternalTodo>(
                await response.Content.ReadAsStringAsync(cancellationToken));
            if (todo is not null)
            {
                memoryCache.Set(id, todo, TimeSpan.FromSeconds(cacheDurationInSeconds));
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