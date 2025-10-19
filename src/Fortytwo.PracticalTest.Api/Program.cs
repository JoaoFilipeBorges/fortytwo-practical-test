using Fortytwo.PracticalTest.Application.Interfaces.Http;
using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Infrastructure.Http;
using Fortytwo.PracticalTest.Infrastructure.Persistence;
using Fortytwo.PracticalTest.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Fortytwo.PracticalTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Fortytwo.PracticalTest.Application.Features.Todos.GetTodoById.GetTodoByIdQueryHandler).Assembly));
            
            // Swagger generator
            builder.Services.AddSwaggerGen();
            
            // Memory Cache (used in the external todo Http Client)
            builder.Services.AddMemoryCache();
            
            // Repositories
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            
            // EF Core DB context
            builder.Services.AddDbContext<PracticalTestDbContext>(options =>
                options.UseSqlite($"Data Source={Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "practicaltest.db")}"));
            
            
            
            builder.Services.AddHttpClient<IExternalTodoHttpClient, ExternalTodoHttpClient>()
                .AddTypedClient((httpClient, sp) =>
                {
                    var config = builder.Configuration.GetSection("ExternalTodoClient");
                    var baseAddress = config.GetValue<string>("BaseAddress");
                    var cacheDuration = config.GetValue<int>("CacheDurationInSeconds");
                    var cache = sp.GetRequiredService<IMemoryCache>();
                    return new ExternalTodoHttpClient(cacheDuration, baseAddress, httpClient, cache);
                });
            
            
            
            
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PracticalTestDbContext>();
                db.Database.EnsureCreated();
            }
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    //options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
