using System;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Attributes;
using EasyTranslate.DalamudPlugin.Resources;

namespace EasyTranslate.DalamudPlugin.Settings;

[EntryPoint]
public sealed class OpenSettingsCommand : IDisposable
{
    private readonly ICommandManager _commandManager;
    private readonly SettingsView _settingsView;

    public OpenSettingsCommand(ICommandManager commandManager, SettingsView settingsView)
    {
        _commandManager = commandManager;
        _settingsView = settingsView;
        _commandManager.AddHandler(
            Strings.SettingsCommand,
            new CommandInfo(HandleCommand) { HelpMessage = Strings.SettingsCommandDescription }
        );
    }

    public void Dispose()
    {
        if (_commandManager.Commands.ContainsKey(Strings.SettingsCommand))
        {
            _commandManager.RemoveHandler(Strings.SettingsCommand);
        }

        GC.SuppressFinalize(this);
    }

    private void HandleCommand(string command, string args)
    {
        _settingsView.Show();
    }

    ~OpenSettingsCommand()
    {
        Dispose();
    }
}
