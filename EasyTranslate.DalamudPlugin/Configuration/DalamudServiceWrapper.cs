using Dalamud.IoC;
using Dalamud.Plugin;

namespace EasyTranslate.DalamudPlugin.Configuration;

#pragma warning disable CS8618

public class DalamudServiceWrapper<T>
{
    public DalamudServiceWrapper(IDalamudPluginInterface pluginInterface)
    {
        pluginInterface.Inject(this);
    }

    [PluginService] public T Service { get; init; }
}
