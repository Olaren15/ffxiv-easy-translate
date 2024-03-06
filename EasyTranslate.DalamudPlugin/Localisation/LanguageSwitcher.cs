namespace EasyTranslate.DalamudPlugin.Localisation;

using System;
using System.Globalization;
using Dalamud.Plugin;
using EasyTranslate.DalamudPlugin.Resources;

public sealed class LanguageSwitcher : IDisposable
{
    private readonly DalamudPluginInterface pluginInterface;

    public LanguageSwitcher(DalamudPluginInterface pluginInterface)
    {
        this.pluginInterface = pluginInterface;

        LanguageChangedHandler(pluginInterface.UiLanguage);
        pluginInterface.LanguageChanged += LanguageChangedHandler;
    }

    public void Dispose()
    {
        pluginInterface.LanguageChanged -= LanguageChangedHandler;
        GC.SuppressFinalize(this);
    }

    public event EventHandler? OnLanguageChangedEvent;

    private void LanguageChangedHandler(string languageCode)
    {
        Strings.Culture = new CultureInfo(languageCode);
        OnLanguageChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    ~LanguageSwitcher()
    {
        Dispose();
    }
}
