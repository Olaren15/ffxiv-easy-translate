using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.DalamudPlugin.Search;
using EasyTranslate.DalamudPlugin.Settings;
using EasyTranslate.Infrastructure.Configuration;
using EasyTranslate.UseCase.Configuration;
using Lumina.Excel;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTranslate.DalamudPlugin.Configuration;

public static class DalamudPluginModule
{
    public static ServiceProvider CreateServiceProvider(DalamudPluginInterface pluginInterface)
    {
        return new ServiceCollection()
            .AddDalamudServices(pluginInterface)
            .AddPluginServices()
            .AddUseCaseServices()
            .AddInfrastructureServices()
            .BuildServiceProvider();
    }

    private static IServiceCollection AddDalamudServices(
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
            .AddDalamudService<IContextMenu>()
            .AddDalamudService<IDataManager>()
            .AddSingleton<ExcelModule>(serviceProvider => serviceProvider.GetService<IDataManager>()!.Excel);
    }

    private static IServiceCollection AddPluginServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<LanguageSwitcher>()
            .AddSingleton<WindowSystem>(_ => new WindowSystem("EasyTranslate"))
            .AddSingleton<SearchView>()
            .AddTransient<SearchViewModel>()
            .AddSingleton<ContentMapper>()
            .AddSingleton<OpenSearchCommand>()
            .AddSingleton<UserSettingsRepository>()
            .AddSingleton<OpenSettingsCommand>()
            .AddSingleton<SettingsView>()
            .AddTransient<SettingsViewModel>()
            .AddSingleton<SearchContextMenuItem>();
    }

    private static IServiceCollection AddExisting<T>(this IServiceCollection serviceCollection, T service)
        where T : class
    {
        return serviceCollection.AddSingleton(service);
    }

    private static IServiceCollection AddDalamudService<T>(this IServiceCollection serviceCollection) where T : class
    {
        serviceCollection.AddSingleton(
            serviceProvider =>
            {
                DalamudPluginInterface? pluginInterface = serviceProvider.GetService<DalamudPluginInterface>();
                DalamudServiceWrapper<T> wrapper = new(pluginInterface!);
                return wrapper.Service;
            }
        );
        return serviceCollection;
    }
}
