namespace EasyTranslate.DalamudPlugin.Settings;

using Domain.Entities;

public class SettingsViewModel(UserSettingsRepository userSettingsRepository)
{
    public Language PreferredLanguage => userSettingsRepository.Get().DefaultSearchLanguage;

    public void SetPreferredLanguage(Language language)
    {
        userSettingsRepository.Save(
            new UserSettings
            {
                DefaultSearchLanguage = language,
                Version = userSettingsRepository.Get().Version,
            }
        );
    }
}
