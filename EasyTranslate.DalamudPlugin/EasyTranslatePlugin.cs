using System;
using System.Linq;
using System.Reflection;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using EasyTranslate.DalamudPlugin.Attributes;
using EasyTranslate.DalamudPlugin.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTranslate.DalamudPlugin;

// ReSharper disable once UnusedType.Global
public sealed class EasyTranslatePlugin : IDalamudPlugin
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IUiBuilder _uiBuilder;
    private readonly WindowSystem _windowSystem;

    public EasyTranslatePlugin(IDalamudPluginInterface pluginInterface)
    {
        _serviceProvider = DalamudPluginModule.CreateServiceProvider(pluginInterface);
        _windowSystem = _serviceProvider.GetService<WindowSystem>()!;
        _uiBuilder = _serviceProvider.GetService<IUiBuilder>()!;

        _uiBuilder.Draw += _windowSystem.Draw;

        // Instantiate classes with [EntryPoint] attribute.
        Assembly.GetAssembly(typeof(EasyTranslatePlugin))!
            .GetTypes()
            .Where(type => type.GetCustomAttribute(typeof(EntryPointAttribute), false) is not null)
            .ToList()
            .ForEach(entryPoint => ActivatorUtilities.CreateInstance(_serviceProvider, entryPoint));
    }

    public void Dispose()
    {
        _uiBuilder.Draw -= _windowSystem.Draw;
        _serviceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

    ~EasyTranslatePlugin()
    {
        Dispose();
    }
}
