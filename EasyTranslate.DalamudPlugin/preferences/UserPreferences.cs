namespace EasyTranslate.DalamudPlugin.preferences;

using System;
using Dalamud.Configuration;
using EasyTranslate.Domain.Entities;

[Serializable]
public class UserPreferences : IPluginConfiguration
{
    public Language DefaultSearchLanguage { get; set; }
    public int Version { get; set; }
}
