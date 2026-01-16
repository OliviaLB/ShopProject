using API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceTests.Infrastructure;
using System.Data.Common;

public class ServiceWebApplicationFactory : WebApplicationFactory<Program>
{
    private static Exception s_exception;
    private DbConnection? _connection;
    private static ServiceWebApplicationFactory s_instance;

    private ServiceWebApplicationFactory() { }

    public ShopDbContext GetDbContext()
    {
        var factory = Services.GetRequiredService<IDbContextFactory<ShopDbContext>>();
        return factory.CreateDbContext();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing")
           .UseConfiguration(Settings.Configuration)
           .ConfigureServices(services =>
           {
               services.AddDbContextFactory<ShopDbContext>();
           });
    }

    public static ServiceWebApplicationFactory Instance
    {
        get
        {
            if (s_instance is not null)
                return s_instance;

            throw s_exception ?? new Exception("server failed to initialise");
        }
    }

    public static void Initialise(Action<IServiceProvider> initialisationActions)
    {

        try
        {
            s_instance = new();
            // The WebApplicationFactory has a problem that until CreateDefaultClient() is called, no underlying TestServer instance is created.
            // Also, the underlying TestServer creation has a race condition, calling it from tests running in parallel may cause multiple servers to be spawned.
            s_instance.CreateDefaultClient();
            initialisationActions(s_instance.Services);
        }
        catch (Exception exception)
        {
            s_exception = new Exception("Initialisation failed", exception);
        }
    }

    public static void Terminate()
    {
        s_instance?.Dispose();
    }

    public static void Terminate(Action<IServiceProvider> terminationActions)
    {

        try
        {
            terminationActions(s_instance.Services);
            s_instance?.Dispose();
        }
        catch (Exception exception)
        {
            s_exception = new Exception("Termination failed", exception);
        }
    }
}
