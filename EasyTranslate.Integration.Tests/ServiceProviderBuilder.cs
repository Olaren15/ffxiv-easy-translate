using System;
using EasyTranslate.Infrastructure.Configuration;
using EasyTranslate.UseCase.Configuration;
using Lumina;
using Lumina.Excel;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTranslate.Integration.Tests;

public static class ServiceProviderBuilder
{
    private static ServiceProvider? s_serviceProvider;
    private static readonly object s_lock = new();

    public static IServiceProvider Build()
    {
        if (s_serviceProvider is not null)
        {
            return s_serviceProvider;
        }

        lock (s_lock)
        {
            string? gameDataPath = Environment.GetEnvironmentVariable("GAME_DATA_PATH");
            if (gameDataPath is null)
            {
                throw new InvalidOperationException(
                    "Please set the GAME_DATA_PATH environment variable as described in the README.md file");
            }

            s_serviceProvider = new ServiceCollection()
                .AddUseCaseServices()
                .AddInfrastructureServices()
                .AddSingleton<ExcelModule>(_ => new GameData(gameDataPath).Excel)
                .BuildServiceProvider();
        }

        return s_serviceProvider;
    }
}
