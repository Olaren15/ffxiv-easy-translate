namespace EasyTranslate.DalamudPlugin.Commands;

using System;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Windows;

public class OpenSearchCommand : IDisposable
{
    private const string TextCommand = "/trans";
    private const string HelpMessage = "Open the item search window";

    private readonly ICommandManager commandManager;
    private readonly SearchWindow searchWindow;

    public OpenSearchCommand(ICommandManager commandManager, SearchWindow searchWindow)
    {
        this.commandManager = commandManager;
        this.searchWindow = searchWindow;
        commandManager.AddHandler(
            TextCommand,
            new CommandInfo(HandleCommand)
            {
                HelpMessage = HelpMessage,
            }
        );
    }

    public void Dispose()
    {
        commandManager.RemoveHandler(TextCommand);
        GC.SuppressFinalize(this);
    }

    public void HandleCommand(string command, string args)
    {
        searchWindow.Show();
    }
}
