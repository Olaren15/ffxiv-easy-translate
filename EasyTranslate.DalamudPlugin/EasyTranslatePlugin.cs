namespace EasyTranslate.DalamudPlugin;

using System;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using EasyTranslate.DalamudPlugin.Commands;
using EasyTranslate.DalamudPlugin.Configuration;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once UnusedType.Global
public sealed class EasyTranslatePlugin : IDalamudPlugin
{
    private readonly IServiceProvider serviceProvider;
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
    }

    public void Dispose()
    {
        uiBuilder.Draw -= windowSystem.Draw;
    }
}
