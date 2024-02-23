namespace EasyTranslate.DalamudPlugin.Preferences;

using EasyTranslate.Domain.Entities;

public class SettingsViewModel(UserPreferencesRepository userPreferencesRepository)
{
    public Language PreferredLanguage => userPreferencesRepository.Get().DefaultSearchLanguage;

    public void SetPreferredLanguage(Language language)
    {
        userPreferencesRepository.Save(
            new UserPreferences
            {
                DefaultSearchLanguage = language,
                Version = userPreferencesRepository.Get().Version,
            }
        );
    }
}
