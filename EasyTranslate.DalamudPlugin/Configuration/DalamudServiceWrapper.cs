﻿namespace EasyTranslate.DalamudPlugin.Configuration;

using Dalamud.IoC;
using Dalamud.Plugin;

#pragma warning disable CS8618

public class DalamudServiceWrapper<T>
{
    public DalamudServiceWrapper(DalamudPluginInterface pluginInterface)
    {
        pluginInterface.Inject(this);
    }

    [PluginService]
    [RequiredVersion("1.0")]
    public T Service { get; init; }
}
