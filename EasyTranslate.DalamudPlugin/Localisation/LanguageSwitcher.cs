using System;
using System.Globalization;
using Dalamud.Plugin;
using EasyTranslate.DalamudPlugin.Resources;

namespace EasyTranslate.DalamudPlugin.Localisation;

public sealed class LanguageSwitcher : IDisposable
{
    private readonly IDalamudPluginInterface _pluginInterface;

    public LanguageSwitcher(IDalamudPluginInterface pluginInterface)
    {
        _pluginInterface = pluginInterface;

        LanguageChangedHandler(pluginInterface.UiLanguage);
        pluginInterface.LanguageChanged += LanguageChangedHandler;
    }

    public void Dispose()
    {
        _pluginInterface.LanguageChanged -= LanguageChangedHandler;
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
