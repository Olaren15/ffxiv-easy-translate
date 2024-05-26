using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Settings;

public class SettingsViewModel(UserSettingsRepository userSettingsRepository)
{
    public Language PreferredLanguage => userSettingsRepository.Get().DefaultSearchLanguage;

    public void SetPreferredLanguage(Language language)
    {
        userSettingsRepository.Save(
            new UserSettings { DefaultSearchLanguage = language, Version = userSettingsRepository.Get().Version }
        );
    }
}
