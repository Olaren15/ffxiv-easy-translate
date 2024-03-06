namespace EasyTranslate.DalamudPlugin;

using System;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using EasyTranslate.DalamudPlugin.Configuration;
using EasyTranslate.DalamudPlugin.Search;
using EasyTranslate.DalamudPlugin.Settings;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once UnusedType.Global
public sealed class EasyTranslatePlugin : IDalamudPlugin
{
    private readonly ServiceProvider serviceProvider;
    private readonly UiBuilder uiBuilder;
    private readonly WindowSystem windowSystem;

    public EasyTranslatePlugin([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface)
    {
        serviceProvider = DalamudPluginModule.CreateServiceProvider(pluginInterface);
        windowSystem = serviceProvider.GetService<WindowSystem>()!;
        uiBuilder = serviceProvider.GetService<UiBuilder>()!;

        uiBuilder.Draw += windowSystem.Draw;

        // We need to manually trigger the instantiation of our plugin classes
        ActivatorUtilities.CreateInstance<OpenSearchCommand>(serviceProvider);
        ActivatorUtilities.CreateInstance<OpenSettingsCommand>(serviceProvider);
        ActivatorUtilities.CreateInstance<SearchFromContextMenuCommand>(serviceProvider);
    }

    public void Dispose()
    {
        uiBuilder.Draw -= windowSystem.Draw;
        serviceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

    ~EasyTranslatePlugin()
    {
        Dispose();
    }
}
