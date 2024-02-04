namespace EasyTranslate.DalamudPlugin.Search;

using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using ImGuiNET;

public class SearchView : Window, IDisposable
{
    private readonly SearchViewModel searchViewModel;
    private readonly UiBuilder uiBuilder;
    private readonly WindowSystem windowSystem;

    private bool windowJustOpened;

    public SearchView(SearchViewModel searchViewModel, UiBuilder uiBuilder, WindowSystem windowSystem) : base("Search")
    {
        this.searchViewModel = searchViewModel;
        this.uiBuilder = uiBuilder;
        this.windowSystem = windowSystem;

        this.windowSystem.AddWindow(this);
        this.uiBuilder.OpenMainUi += Show;

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue),
        };
    }

    public void Dispose()
    {
        windowSystem.RemoveWindow(this);
        uiBuilder.OpenMainUi -= Show;
        GC.SuppressFinalize(this);
    }

    public override void OnOpen()
    {
        base.OnOpen();
        searchViewModel.ResetSearch();
        windowJustOpened = true;
    }

    public override void Draw()
    {
        DrawSearchBar();
        ImGui.Separator();
        DrawSearchResults();
        windowJustOpened = false;
    }

    private void DrawSearchBar()
    {
        var searchText = searchViewModel.SearchText;
        var enterPressed = ImGui.InputText("", ref searchText, 100, ImGuiInputTextFlags.EnterReturnsTrue);
        if (enterPressed || windowJustOpened)
        {
            ImGui.SetKeyboardFocusHere(-1);
        }
        searchViewModel.SearchText = searchText;

        ImGui.SameLine();
        var searchButtonPressed = ImGui.Button("Search");

        if (enterPressed || searchButtonPressed)
        {
            searchViewModel.ExecuteSearch();
        }
    }

    private void DrawSearchResults()
    {
        if (searchViewModel.SearchResultsAreLoading)
        {
            ImGui.Text("Loading...");
        }

        if (searchViewModel.SearchResults is not null)
        {
            ImGui.Text(searchViewModel.SearchResults.Any() ? "We got something uwu" : "We didn't find anything qwq");
            // TODO: Actual UI
        }
    }

    public void Show()
    {
        IsOpen = true;
    }
}
