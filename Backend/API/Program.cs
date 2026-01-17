
using API.Endpoints;
using API.Extensions;
using API.Handlers;
using Persistence.SqlServer;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddAuthorization();

        builder.Services.AddCors();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger(builder.Configuration);
        builder.Services.AddServiceDependencies();
        builder.Services.AddPersistenceDependencies(builder.Configuration);

        var app = builder.Build();

        app.UseCors("AllowAll");

        app.UseCors(opt =>
        {
            opt
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("https://localhost:3000");
        });
        app.UseExceptionHandler();

        if (builder.Environment.IsDevelopmentEnvironment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapEndpoints();

        if (!app.Environment.IsEnvironment("Testing"))
        {
            DbInitialiser.InitDatabase(app);
        }

        app.Run();
    }
}