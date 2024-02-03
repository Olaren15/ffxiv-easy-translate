namespace EasyTranslate;

using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using EasyTranslate.Windows;

// ReSharper disable once UnusedType.Global
public sealed class EasyTranslatePlugin : IDalamudPlugin
{
    private const string CommandName = "/trans";
    public readonly WindowSystem WindowSystem = new("EasyTranslate");

    public EasyTranslatePlugin(
        [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
        [RequiredVersion("1.0")] ICommandManager commandManager
    )
    {
        PluginInterface = pluginInterface;
        CommandManager = commandManager;

        SearchWindow = new SearchWindow();
        WindowSystem.AddWindow(SearchWindow);

        CommandManager.AddHandler(
            CommandName,
            new CommandInfo(OnSearchCommand)
            {
                HelpMessage = "Open the item search window",
            }
        );

        PluginInterface.UiBuilder.Draw += DrawUi;
        PluginInterface.UiBuilder.OpenMainUi += OpenSearchWindow;
    }

    private DalamudPluginInterface PluginInterface { get; init; }
    private ICommandManager CommandManager { get; init; }
    private SearchWindow SearchWindow { get; init; }

    public void Dispose()
    {
        PluginInterface.UiBuilder.Draw -= DrawUi;
        PluginInterface.UiBuilder.OpenMainUi -= OpenSearchWindow;
        WindowSystem.RemoveAllWindows();
        CommandManager.RemoveHandler(CommandName);
    }

    private void OnSearchCommand(string command, string args)
    {
        OpenSearchWindow();
    }

    private void DrawUi()
    {
        WindowSystem.Draw();
    }

    private void OpenSearchWindow()
    {
        SearchWindow.IsOpen = true;
    }
}
