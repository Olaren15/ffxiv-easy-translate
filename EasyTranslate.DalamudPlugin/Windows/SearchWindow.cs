namespace EasyTranslate.DalamudPlugin.Windows;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using EasyTranslate.Domain.Entities;
using EasyTranslate.UseCase.ItemSearch;
using ImGuiNET;

public class SearchWindow : Window, IDisposable
{
    private readonly SearchItemByNameCommand searchItemByNameCommand;
    private readonly JsonSerializerOptions serializerOptions = new() { WriteIndented = true };
    private readonly UiBuilder uiBuilder;
    private readonly WindowSystem windowSystem;

    private string? searchResults;
    private Task<IEnumerable<Item>>? searchTask;
    private string searchText = "";

    public SearchWindow(WindowSystem windowSystem, UiBuilder uiBuilder, SearchItemByNameCommand searchItemByNameCommand)
        : base("Search", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.windowSystem = windowSystem;
        this.uiBuilder = uiBuilder;
        this.searchItemByNameCommand = searchItemByNameCommand;

        windowSystem.AddWindow(this);
        uiBuilder.OpenMainUi += Show;

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
        searchText = "";
        searchResults = null;
    }

    public override void Draw()
    {
        if (ImGui.InputText("Search", ref searchText, 100, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            searchTask = searchItemByNameCommand.SearchItemByName(searchText, Language.English);
        }

        if (searchTask is { IsCompleted: true }) // Pool completion to not block the ui thread
        {
            searchResults = JsonSerializer.Serialize(searchTask.Result, serializerOptions);
        }

        if (searchResults is not null)
        {
            ImGui.Text(searchResults);
        }
    }

    public void Show()
    {
        IsOpen = true;
    }
}
