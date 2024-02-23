namespace EasyTranslate.DalamudPlugin.Commands;

using System;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Search;

public sealed class OpenSearchCommand : IDisposable
{
    private const string TextCommand = "/et";
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
        if (commandManager.Commands.ContainsKey(TextCommand))
        {
            commandManager.RemoveHandler(TextCommand);
        }

        GC.SuppressFinalize(this);
    }

    private void HandleCommand(string command, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            searchView.Show();
        }
        else
        {
            searchView.ShowAndSearch(args);
        }
    }

    ~OpenSearchCommand()
    {
        Dispose();
    }
}
