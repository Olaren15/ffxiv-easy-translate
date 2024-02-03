namespace EasyTranslate.DalamudPlugin.Configuration;

using System;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using EasyTranslate.DalamudPlugin.Commands;
using EasyTranslate.DalamudPlugin.Windows;
using EasyTranslate.Infrastructure.XivApi.Configuration;
using EasyTranslate.UseCase.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DalamudPluginModule
{
    public static IServiceProvider CreateServiceProvider(DalamudPluginInterface pluginInterface)
    {
        return new ServiceCollection()
               .AddDalamudServices(pluginInterface)
               .AddPluginServices()
               .AddInfrastructureServices()
               .AddUseCaseServices()
               .BuildServiceProvider();
    }

    public static IServiceCollection AddDalamudServices(
        this IServiceCollection serviceCollection,
        DalamudPluginInterface pluginInterface
    )
    {
        var dalamudServices = pluginInterface.Create<DalamudServices>()!;
        return serviceCollection
               .AddExisting(pluginInterface)
               .AddExisting(pluginInterface.UiBuilder)
               .AddExisting(dalamudServices.CommandManager);
    }

    public static IServiceCollection AddPluginServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
               .AddSingleton<WindowSystem>(_ => new WindowSystem("EasyTranslate"))
               .AddSingleton<SearchWindow>()
               .AddSingleton<OpenSearchCommand>();
    }

    public static IServiceCollection AddExisting<T>(this IServiceCollection serviceCollection, T service)
        where T : class
    {
        return serviceCollection.AddSingleton<T>(_ => service);
    }
}
