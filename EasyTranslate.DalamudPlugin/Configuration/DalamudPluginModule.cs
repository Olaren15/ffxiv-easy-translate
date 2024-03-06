namespace EasyTranslate.DalamudPlugin.Configuration;

using System.Collections.Generic;
using Dalamud.ContextMenu;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.DalamudPlugin.Search;
using EasyTranslate.DalamudPlugin.Settings;
using EasyTranslate.Infrastructure.XivApi.Configuration;
using EasyTranslate.UseCase.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DalamudPluginModule
{
    public static ServiceProvider CreateServiceProvider(DalamudPluginInterface pluginInterface)
    {
        var config = new ConfigurationBuilder()
                     .AddInMemoryCollection(
                         new Dictionary<string, string?>
                         {
                             [InfrastructureModule.XivApiUrlConfigName] = "https://xivapi.com",
                         }
                     )
                     .Build();

        return new ServiceCollection()
               .AddDalamudServices(pluginInterface)
               .AddPluginServices()
               .AddUseCaseServices()
               .AddInfrastructureServices(config)
               .BuildServiceProvider();
    }

    public static IServiceCollection AddDalamudServices(
        this IServiceCollection serviceCollection,
        DalamudPluginInterface pluginInterface
    )
    {
        return serviceCollection
               .AddExisting(pluginInterface)
               .AddExisting(pluginInterface.UiBuilder)
               .AddDalamudService<ICommandManager>()
               .AddDalamudService<ITextureProvider>()
               .AddDalamudService<IDataManager>()
               .AddDalamudService<IGameConfig>()
               .AddSingleton(
                   serviceProvider => new DalamudContextMenu(serviceProvider.GetService<DalamudPluginInterface>())
               );
    }

    public static IServiceCollection AddPluginServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
               .AddSingleton<LanguageSwitcher>()
               .AddSingleton<WindowSystem>(_ => new WindowSystem("EasyTranslate"))
               .AddSingleton<SearchView>()
               .AddTransient<SearchViewModel>()
               .AddSingleton<ItemMapper>()
               .AddSingleton<OpenSearchCommand>()
               .AddSingleton<UserSettingsRepository>()
               .AddSingleton<SettingsView>()
               .AddTransient<SettingsViewModel>()
               .AddSingleton<SearchFromContextMenuCommand>();
    }

    public static IServiceCollection AddExisting<T>(this IServiceCollection serviceCollection, T service)
        where T : class
    {
        return serviceCollection.AddSingleton(service);
    }

    private static IServiceCollection AddDalamudService<T>(this IServiceCollection serviceCollection) where T : class
    {
        serviceCollection.AddSingleton(
            serviceProvider =>
            {
                var pluginInterface = serviceProvider.GetService<DalamudPluginInterface>();
                var wrapper = new DalamudServiceWrapper<T>(pluginInterface!);
                return wrapper.Service;
            }
        );
        return serviceCollection;
    }
}
