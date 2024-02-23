namespace EasyTranslate.DalamudPlugin.Preferences;

using System;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using EasyTranslate.Domain.Entities;
using ImGuiNET;

public class SettingsView : Window, IDisposable
{
    private readonly SettingsViewModel settingsViewModel;
    private readonly UiBuilder uiBuilder;
    private readonly WindowSystem windowSystem;

    public SettingsView(SettingsViewModel settingsViewModel, UiBuilder uiBuilder, WindowSystem windowSystem) : base(
        "EasyTranslate Settings"
    )
    {
        this.settingsViewModel = settingsViewModel;
        this.uiBuilder = uiBuilder;
        this.windowSystem = windowSystem;

        this.windowSystem.AddWindow(this);
        this.uiBuilder.OpenConfigUi += Show;

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
        ImGui.Text("Choose your search language:");

        var currentLanguage = settingsViewModel.PreferredLanguage;

        if (ImGui.RadioButton("English", currentLanguage == Language.English))
        {
            settingsViewModel.SetPreferredLanguage(Language.English);
        }

        if (ImGui.RadioButton("French", currentLanguage == Language.French))
        {
            settingsViewModel.SetPreferredLanguage(Language.French);
        }

        if (ImGui.RadioButton("German", currentLanguage == Language.German))
        {
            settingsViewModel.SetPreferredLanguage(Language.German);
        }

        if (ImGui.RadioButton("Japanese", currentLanguage == Language.Japanese))
        {
            settingsViewModel.SetPreferredLanguage(Language.Japanese);
        }
    }

    public void Show()
    {
        IsOpen = true;
    }
}
