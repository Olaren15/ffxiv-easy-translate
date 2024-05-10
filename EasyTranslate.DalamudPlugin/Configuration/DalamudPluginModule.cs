namespace EasyTranslate.DalamudPlugin.Configuration;

using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Infrastructure.Configuration;
using Localisation;
using Lumina.Excel;
using Microsoft.Extensions.DependencyInjection;
using Search;
using Settings;
using UseCase.Configuration;

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
               .AddDalamudService<IContextMenu>()
               .AddDalamudService<IDataManager>()
               .AddSingleton<ExcelModule>(serviceProvider => serviceProvider.GetService<IDataManager>()!.Excel);
    }

    public static IServiceCollection AddPluginServices(this IServiceCollection serviceCollection)
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
