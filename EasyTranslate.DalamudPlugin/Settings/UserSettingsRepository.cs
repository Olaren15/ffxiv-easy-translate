using Dalamud.Game;
using Dalamud.Game.Config;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Settings;

public class UserSettingsRepository
{
    private readonly IGameConfig _gameConfig;
    private readonly IDalamudPluginInterface _pluginInterface;

    private UserSettings _userSettings;

    public UserSettingsRepository(IDalamudPluginInterface pluginInterface, IGameConfig gameConfig)
    {
        _gameConfig = gameConfig;
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
    }

    private UserSettings CreateDefaultUserPreferences()
    {
        bool success = _gameConfig.TryGet(SystemConfigOption.Language, out uint gameLanguageCode);
        ClientLanguage gameLanguage = success ? (ClientLanguage)gameLanguageCode : ClientLanguage.English;

        UserSettings newPreferences = new()
        {
            Version = 1,
            DefaultSearchLanguage = gameLanguage switch
            {
                ClientLanguage.Japanese => Language.Japanese,
                ClientLanguage.English => Language.English,
                ClientLanguage.German => Language.German,
                ClientLanguage.French => Language.French,
                _ => Language.English
            }
        };
        Save(newPreferences);

        return newPreferences;
    }
}
