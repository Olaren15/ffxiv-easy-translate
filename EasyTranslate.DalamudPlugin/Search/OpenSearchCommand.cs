namespace EasyTranslate.DalamudPlugin.Search;

using System;
using Attributes;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using Resources;

[EntryPoint]
public sealed class OpenSearchCommand : IDisposable
{
    private readonly ICommandManager commandManager;
    private readonly SearchView searchView;

    public OpenSearchCommand(ICommandManager commandManager, SearchView searchView)
    {
        this.commandManager = commandManager;
        this.searchView = searchView;
        commandManager.AddHandler(
            Strings.SearchCommand,
            new CommandInfo(HandleCommand)
            {
                HelpMessage = Strings.SearchCommandDescription,
            }
        );
    }

    public void Dispose()
    {
        if (commandManager.Commands.ContainsKey(Strings.SearchCommand))
        {
            commandManager.RemoveHandler(Strings.SearchCommand);
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
