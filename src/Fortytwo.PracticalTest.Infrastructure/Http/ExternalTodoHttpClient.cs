using Fortytwo.PracticalTest.Application.Interfaces.Http;
using Fortytwo.PracticalTest.Application.ReadModel.External;

namespace Fortytwo.PracticalTest.Infrastructure.Http;

public class ExternalTodoHttpClient : IPraticalTestHttpClient<ExternalTodo>
{
    public Task<ExternalTodo> GetAsync(string url)
    {
        throw new NotImplementedException();
    }
}