namespace EasyTranslate.DalamudPlugin.Settings;

using System;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using Domain.Entities;
using ImGuiNET;
using Localisation;
using Resources;

public sealed class SettingsView : Window, IDisposable
{
    private readonly SettingsViewModel settingsViewModel;
    private readonly UiBuilder uiBuilder;
    private readonly WindowSystem windowSystem;

    public SettingsView(
        SettingsViewModel settingsViewModel,
        UiBuilder uiBuilder,
        WindowSystem windowSystem,
        LanguageSwitcher languageSwitcher
    ) : base(Strings.SettingsWindowTitle)
    {
        this.settingsViewModel = settingsViewModel;
        this.uiBuilder = uiBuilder;
        this.windowSystem = windowSystem;

        this.windowSystem.AddWindow(this);
        this.uiBuilder.OpenConfigUi += Show;
        languageSwitcher.OnLanguageChangedEvent += (_, _) => WindowName = Strings.SettingsWindowTitle;

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(300, 200),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue),
        };
    }

    public void Dispose()
    {
        windowSystem.RemoveWindow(this);
        uiBuilder.OpenConfigUi -= Show;
        GC.SuppressFinalize(this);
    }

    public override void Draw()
    {
        ImGui.Text(Strings.ChooseSearchLanguage);

        var currentLanguage = settingsViewModel.PreferredLanguage;

        if (ImGui.RadioButton(Strings.English, currentLanguage == Language.English))
        {
            settingsViewModel.SetPreferredLanguage(Language.English);
        }

        if (ImGui.RadioButton(Strings.French, currentLanguage == Language.French))
        {
            settingsViewModel.SetPreferredLanguage(Language.French);
        }

        if (ImGui.RadioButton(Strings.German, currentLanguage == Language.German))
        {
            settingsViewModel.SetPreferredLanguage(Language.German);
        }

        if (ImGui.RadioButton(Strings.Japanese, currentLanguage == Language.Japanese))
        {
            settingsViewModel.SetPreferredLanguage(Language.Japanese);
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
