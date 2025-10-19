using Fortytwo.PracticalTest.Api.Auth;
using Fortytwo.PracticalTest.Api.DI;
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
                cfg.RegisterServicesFromAssembly(typeof(Application.Features.Todos.GetTodoById.GetTodoByIdQueryHandler).Assembly));
            
            // Swagger generator
            builder.Services.AddSwagger();
            
            // Memory Cache (used in the external todo Http Client)
            builder.Services.AddMemoryCache();
            
            // Repositories
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            
            // EF Core DB context
            builder.Services.AddDbContext<PracticalTestDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            
            // Custom Http client
            builder.Services.AddHttpClient<IExternalTodoHttpClient, ExternalTodoHttpClient>()
                .AddTypedClient((httpClient, sp) =>
                {
                    var config = builder.Configuration.GetSection("ExternalTodoClient");
                    var baseAddress = config.GetValue<string>("BaseAddress");
                    var cacheDuration = config.GetValue<int>("CacheDurationInSeconds");
                    var cache = sp.GetRequiredService<IMemoryCache>();
                    return new ExternalTodoHttpClient(cacheDuration, baseAddress, httpClient, cache);
                });


            // Token generator
            builder.Services.AddSingleton<JwtGenerator>();
            
            // JWT configuration
            builder.Services.AddAuthentication(builder.Configuration);
            
            var app = builder.Build();

            // Ensure EF migration is launched
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
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        
        // public static IHostBuilder CreateHostBuilder(string[] args)
        //     => Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(
        //             webBuilder => webBuilder.UseStartup<Startup>());
        //
        // public class Startup
        // {
        //     public void ConfigureServices(IServiceCollection services)
        //         => services.AddDbContext<PracticalTestDbContext>();
        //
        //     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //     {
        //     }
        // }
    }
}
