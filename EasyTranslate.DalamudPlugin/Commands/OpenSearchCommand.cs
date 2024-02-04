namespace EasyTranslate.DalamudPlugin.Commands;

using System;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Search;

public class OpenSearchCommand : IDisposable
{
    private const string TextCommand = "/trans";
    private const string HelpMessage = "Open the item search window";

    private readonly ICommandManager commandManager;
    private readonly SearchView searchView;

    public OpenSearchCommand(ICommandManager commandManager, SearchView searchView)
    {
        this.commandManager = commandManager;
        this.searchView = searchView;
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
        searchView.Show();
    }
}
