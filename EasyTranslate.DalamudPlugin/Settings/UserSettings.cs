using System;
using Dalamud.Configuration;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Settings;

[Serializable]
public class UserSettings : IPluginConfiguration
{
    public Language DefaultSearchLanguage { get; set; }

    public int Version { get; set; }
}
