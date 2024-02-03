namespace EasyTranslate.DalamudPlugin.Configuration;

using Dalamud.IoC;
using Dalamud.Plugin.Services;

#pragma warning disable CS8618

// ReSharper disable once ClassNeverInstantiated.Global
public class DalamudServices
{
    [PluginService]
    [RequiredVersion("1.0")]
    public ICommandManager CommandManager { get; init; }
}
