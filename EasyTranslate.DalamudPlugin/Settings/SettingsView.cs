using System;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.DalamudPlugin.Resources;
using EasyTranslate.Domain.Entities;
using ImGuiNET;

namespace EasyTranslate.DalamudPlugin.Settings;

public sealed class SettingsView : Window, IDisposable
{
    private readonly SettingsViewModel _settingsViewModel;
    private readonly UiBuilder _uiBuilder;
    private readonly WindowSystem _windowSystem;

    public SettingsView(
        SettingsViewModel settingsViewModel,
        UiBuilder uiBuilder,
        WindowSystem windowSystem,
        LanguageSwitcher languageSwitcher
    ) : base(Strings.SettingsWindowTitle)
    {
        _settingsViewModel = settingsViewModel;
        _uiBuilder = uiBuilder;
        _windowSystem = windowSystem;

        _windowSystem.AddWindow(this);
        _uiBuilder.OpenConfigUi += Show;
        languageSwitcher.OnLanguageChangedEvent += (_, _) => WindowName = Strings.SettingsWindowTitle;

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(300, 200),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    }

    public void Dispose()
    {
        _windowSystem.RemoveWindow(this);
        _uiBuilder.OpenConfigUi -= Show;
        GC.SuppressFinalize(this);
    }

    public override void Draw()
    {
        ImGui.Text(Strings.ChooseSearchLanguage);

        Language currentLanguage = _settingsViewModel.PreferredLanguage;

        if (ImGui.RadioButton(Strings.English, currentLanguage == Language.English))
        {
            _settingsViewModel.SetPreferredLanguage(Language.English);
        }

        if (ImGui.RadioButton(Strings.French, currentLanguage == Language.French))
        {
            _settingsViewModel.SetPreferredLanguage(Language.French);
        }

        if (ImGui.RadioButton(Strings.German, currentLanguage == Language.German))
        {
            _settingsViewModel.SetPreferredLanguage(Language.German);
        }

        if (ImGui.RadioButton(Strings.Japanese, currentLanguage == Language.Japanese))
        {
            _settingsViewModel.SetPreferredLanguage(Language.Japanese);
        }
    }

    public void Show()
    {
        IsOpen = true;
    }

    ~SettingsView()
    {
        Dispose();
    }
}
