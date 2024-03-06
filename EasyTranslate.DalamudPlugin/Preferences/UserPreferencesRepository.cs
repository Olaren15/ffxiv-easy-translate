namespace EasyTranslate.DalamudPlugin.Preferences;

using Dalamud;
using Dalamud.Game.Config;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using EasyTranslate.Domain.Entities;

public class UserPreferencesRepository
{
    private readonly IGameConfig gameConfig;
    private readonly DalamudPluginInterface pluginInterface;

    private UserPreferences userPreferences;

    public UserPreferencesRepository(DalamudPluginInterface pluginInterface, IGameConfig gameConfig)
    {
        this.gameConfig = gameConfig;
        this.pluginInterface = pluginInterface;
        userPreferences = this.pluginInterface.GetPluginConfig() as UserPreferences ?? CreateDefaultUserPreferences();
    }

    public UserPreferences Get()
    {
        return userPreferences;
    }

    public void Save(UserPreferences newPreferences)
    {
        userPreferences = newPreferences;
        pluginInterface.SavePluginConfig(userPreferences);
    }

    private UserPreferences CreateDefaultUserPreferences()
    {
        var success = gameConfig.TryGet(SystemConfigOption.Language, out uint gameLanguageCode);
        var gameLanguage = success ? (ClientLanguage)gameLanguageCode : ClientLanguage.English;

        var newPreferences = new UserPreferences
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
