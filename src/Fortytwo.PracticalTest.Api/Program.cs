using System.Reflection;
using FluentValidation;
using Fortytwo.PracticalTest.Api.Auth;
using Fortytwo.PracticalTest.Api.DI;
using Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;
using Fortytwo.PracticalTest.Application.Interfaces.Http;
using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Infrastructure.Http;
using Fortytwo.PracticalTest.Infrastructure.Persistence;
using Fortytwo.PracticalTest.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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
            // Ensure the folder exists
            var dbFolder = Path.Combine(Environment.CurrentDirectory, "data");
            if (!Directory.Exists(dbFolder))
                Directory.CreateDirectory(dbFolder);

            var dbPath = Path.Combine(dbFolder, "practicaltest.db");
            var connectionString = $"Data Source={dbPath}";

            builder.Services.AddDbContext<PracticalTestDbContext>(options =>
                options.UseSqlite(connectionString));
            
            // Custom Http client
            builder.Services.Configure<ExternalTodoHttpClientOptions>(builder.Configuration.GetSection("ExternalTodoClient"));
            builder.Services.AddHttpClient<IExternalTodoHttpClient, ExternalTodoHttpClient>();

            builder.Services.AddValidatorsFromAssembly(typeof(CreateTodoValidator).Assembly);
            
            // Token generator
            builder.Services.AddSingleton<JwtGenerator>();
            
            // JWT configuration
            builder.Services.AddAuthentication(builder.Configuration);
            
            var app = builder.Build();

            // Ensure EF migration is launched
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PracticalTestDbContext>();
                db.Database.Migrate();
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
