namespace EasyTranslate.DalamudPlugin;

using System;
using Dalamud.Configuration;

[Serializable]
public class Config : IPluginConfiguration
{
    public int Version { get; set; } = 0;
}
