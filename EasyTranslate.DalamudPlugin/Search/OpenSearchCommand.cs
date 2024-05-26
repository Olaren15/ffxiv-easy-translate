using System;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Attributes;
using EasyTranslate.DalamudPlugin.Resources;

namespace EasyTranslate.DalamudPlugin.Search;

[EntryPoint]
public sealed class OpenSearchCommand : IDisposable
{
    private readonly ICommandManager _commandManager;
    private readonly SearchView _searchView;

    public OpenSearchCommand(ICommandManager commandManager, SearchView searchView)
    {
        _commandManager = commandManager;
        _searchView = searchView;
        commandManager.AddHandler(
            Strings.SearchCommand,
            new CommandInfo(HandleCommand) { HelpMessage = Strings.SearchCommandDescription }
        );
    }

    public void Dispose()
    {
        if (_commandManager.Commands.ContainsKey(Strings.SearchCommand))
        {
            _commandManager.RemoveHandler(Strings.SearchCommand);
        }

        GC.SuppressFinalize(this);
    }

    private void HandleCommand(string command, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            _searchView.Show();
        }
        else
        {
            _searchView.ShowAndSearch(args);
        }
    }

    ~OpenSearchCommand()
    {
        Dispose();
    }
}
