namespace EasyTranslate.DalamudPlugin.Commands;

using System;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Preferences;

public sealed class OpenSettingsCommand : IDisposable
{
    private const string TextCommand = "/ets";
    private const string HelpMessage = "Open the settings window";

    private readonly ICommandManager commandManager;
    private readonly SettingsView settingsView;

    public OpenSettingsCommand(ICommandManager commandManager, SettingsView settingsView)
    {
        this.commandManager = commandManager;
        this.settingsView = settingsView;
        this.commandManager.AddHandler(
            TextCommand,
            new CommandInfo(HandleCommand)
            {
                HelpMessage = HelpMessage,
            }
        );
    }

    public void Dispose()
    {
        if (commandManager.Commands.ContainsKey(TextCommand))
        {
            commandManager.RemoveHandler(TextCommand);
        }

        GC.SuppressFinalize(this);
    }

    private void HandleCommand(string command, string args)
    {
        settingsView.Show();
    }

    ~OpenSettingsCommand()
    {
        Dispose();
    }
}
