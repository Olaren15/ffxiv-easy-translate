namespace EasyTranslate.DalamudPlugin.Settings;

using Dalamud;
using Dalamud.Game.Config;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using EasyTranslate.Domain.Entities;

public class UserSettingsRepository
{
    private readonly IGameConfig gameConfig;
    private readonly DalamudPluginInterface pluginInterface;

    private UserSettings userSettings;

    public UserSettingsRepository(DalamudPluginInterface pluginInterface, IGameConfig gameConfig)
    {
        this.gameConfig = gameConfig;
        this.pluginInterface = pluginInterface;
        userSettings = this.pluginInterface.GetPluginConfig() as UserSettings ?? CreateDefaultUserPreferences();
    }

    public UserSettings Get()
    {
        return userSettings;
    }

    public void Save(UserSettings newSettings)
    {
        userSettings = newSettings;
        pluginInterface.SavePluginConfig(userSettings);
    }

    private UserSettings CreateDefaultUserPreferences()
    {
        var success = gameConfig.TryGet(SystemConfigOption.Language, out uint gameLanguageCode);
        var gameLanguage = success ? (ClientLanguage)gameLanguageCode : ClientLanguage.English;

        var newPreferences = new UserSettings
        {
            Version = 1,
            DefaultSearchLanguage = gameLanguage switch
            {
                ClientLanguage.Japanese => Language.Japanese,
                ClientLanguage.English => Language.English,
                ClientLanguage.German => Language.German,
                ClientLanguage.French => Language.French,
                var _ => Language.English,
            },
        };
        Save(newPreferences);

        return newPreferences;
    }
}
