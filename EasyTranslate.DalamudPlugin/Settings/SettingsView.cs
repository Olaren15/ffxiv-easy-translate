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
    private readonly IUiBuilder _uiBuilder;
    private readonly WindowSystem _windowSystem;

    public SettingsView(
        SettingsViewModel settingsViewModel,
        IUiBuilder uiBuilder,
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
        ImGui.Text(Strings.ChooseDefaultSearchLanguage);


        bool isEnglishActive = _settingsViewModel.PreferredLanguage == Language.English;
        if (ImGui.RadioButton(Strings.English, isEnglishActive) && !isEnglishActive)
        {
            _settingsViewModel.SetPreferredLanguage(Language.English);
        }

        bool isFrenchActive = _settingsViewModel.PreferredLanguage == Language.French;
        if (ImGui.RadioButton(Strings.French, isFrenchActive) && !isFrenchActive)
        {
            _settingsViewModel.SetPreferredLanguage(Language.French);
        }

        bool isGermanActive = _settingsViewModel.PreferredLanguage == Language.German;
        if (ImGui.RadioButton(Strings.German, isGermanActive) && !isGermanActive)
        {
            _settingsViewModel.SetPreferredLanguage(Language.German);
        }

        bool isJapaneseActive = _settingsViewModel.PreferredLanguage == Language.Japanese;
        if (ImGui.RadioButton(Strings.Japanese, isJapaneseActive) && !isJapaneseActive)
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
