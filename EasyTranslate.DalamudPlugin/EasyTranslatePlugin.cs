namespace EasyTranslate.DalamudPlugin;

using System;
using System.Linq;
using System.Reflection;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using EasyTranslate.DalamudPlugin.Attributes;
using EasyTranslate.DalamudPlugin.Configuration;
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

        // Instantiate classes with [EntryPoint] attribute.
        Assembly.GetAssembly(typeof(EasyTranslatePlugin))!
                .GetTypes()
                .Where(type => type.GetCustomAttribute(typeof(EntryPointAttribute), false) is not null)
                .ToList()
                .ForEach(entryPoint => ActivatorUtilities.CreateInstance(serviceProvider, entryPoint));
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
