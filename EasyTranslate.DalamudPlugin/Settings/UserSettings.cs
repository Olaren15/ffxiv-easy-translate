namespace EasyTranslate.DalamudPlugin.Settings;

using System;
using Dalamud.Configuration;
using Domain.Entities;

[Serializable]
public class UserSettings : IPluginConfiguration
{
    public Language DefaultSearchLanguage { get; set; }
    public int Version { get; set; }
}
