namespace EasyTranslate.DalamudPlugin.Settings;

using EasyTranslate.Domain.Entities;

public class SettingsViewModel
{
    private readonly UserSettingsRepository userSettingsRepository;

    public SettingsViewModel(UserSettingsRepository userSettingsRepository)
    {
        this.userSettingsRepository = userSettingsRepository;
    }

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
