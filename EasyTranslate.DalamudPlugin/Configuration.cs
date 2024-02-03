namespace EasyTranslate;

using System;
using Dalamud.Configuration;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;
}
