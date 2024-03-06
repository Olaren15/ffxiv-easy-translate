namespace EasyTranslate.DalamudPlugin.Settings;

using System;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Resources;

public sealed class OpenSettingsCommand : IDisposable
{
    private readonly ICommandManager commandManager;
    private readonly SettingsView settingsView;

    public OpenSettingsCommand(ICommandManager commandManager, SettingsView settingsView)
    {
        this.commandManager = commandManager;
        this.settingsView = settingsView;
        this.commandManager.AddHandler(
            Strings.SettingsCommand,
            new CommandInfo(HandleCommand)
            {
                HelpMessage = Strings.SettingsCommandDescription,
            }
        );
    }

    public void Dispose()
    {
        if (commandManager.Commands.ContainsKey(Strings.SettingsCommand))
        {
            commandManager.RemoveHandler(Strings.SettingsCommand);
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
