namespace EasyTranslate.DalamudPlugin.preferences;

using Dalamud.Plugin;
using EasyTranslate.Domain.Parsers;

public class UserPreferencesRepository
{
    private readonly DalamudPluginInterface pluginInterface;
    private UserPreferences userPreferences;

    public UserPreferencesRepository(DalamudPluginInterface pluginInterface)
    {
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
        var newPreferences = new UserPreferences
        {
            Version = 1,
            DefaultSearchLanguage = LanguageParser.FromIsoCode(pluginInterface.UiLanguage),
        };
        this.Save(newPreferences);

        return newPreferences;
    }
}
