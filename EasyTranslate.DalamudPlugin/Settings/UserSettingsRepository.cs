using System;
using Dalamud.Plugin;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Settings;

public class UserSettingsRepository
{
    private readonly LanguageUtils _languageUtils;
    private readonly IDalamudPluginInterface _pluginInterface;

    private UserSettings _userSettings;

    public UserSettingsRepository(IDalamudPluginInterface pluginInterface, LanguageUtils languageUtil)
    {
        _languageUtils = languageUtil;
        _pluginInterface = pluginInterface;
        _userSettings = _pluginInterface.GetPluginConfig() as UserSettings ?? CreateDefaultUserPreferences();
    }

    public UserSettings Get()
    {
        return _userSettings;
    }

    public void Save(UserSettings newSettings)
    {
        _userSettings = newSettings;
        _pluginInterface.SavePluginConfig(_userSettings);
        OnSearchLanguageChanged?.Invoke(this, newSettings.DefaultSearchLanguage);
    }

    public event EventHandler<Language>? OnSearchLanguageChanged;

    private UserSettings CreateDefaultUserPreferences()
    {
        UserSettings newPreferences = new() { Version = 1, DefaultSearchLanguage = _languageUtils.GetGameLanguage() };
        Save(newPreferences);

        return newPreferences;
    }
}
