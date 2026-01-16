using System.IO;
using API;
using Microsoft.Extensions.Configuration;

namespace ServiceTests.Infrastructure;

public static class Settings
{
    public static IConfiguration Configuration { get; }

    static Settings()
    {

        var path = AppDomain.CurrentDomain.BaseDirectory;
        var appSettingsWebApp = Path.Combine(path, "appsettings.json");
        var appSettingsEnv = Path.Combine(path, $"appsettings.Testing.json");

        var config = new ConfigurationBuilder()
            .AddJsonFile(appSettingsWebApp, true, true)
            .AddJsonFile(appSettingsEnv, false, true)
            .AddEnvironmentVariables()
            .AddUserSecrets<Program>()
            .Build();
        Configuration = config;
    }

    public static bool IsLocalEnvironment => ServiceTestEnvironment == "local";

    internal static bool IsCiEnvironment => ServiceTestEnvironment != "local";

    private static string ServiceTestEnvironment => Environment.GetEnvironmentVariable("SERVICE_TEST_ENV") ?? "local";
}
