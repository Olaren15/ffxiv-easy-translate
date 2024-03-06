namespace EasyTranslate.DalamudPlugin.Search;

using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Windowing;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.DalamudPlugin.Resources;
using EasyTranslate.Domain.Entities;
using ImGuiNET;

public sealed class SearchView : Window, IDisposable
{
    private const int MaxImageSize = 80;
    private readonly SearchViewModel searchViewModel;
    private readonly UiBuilder uiBuilder;
    private readonly WindowSystem windowSystem;

    private bool windowJustOpened;

    public SearchView(
        SearchViewModel searchViewModel,
        UiBuilder uiBuilder,
        WindowSystem windowSystem,
        LanguageSwitcher languageSwitcher
    ) : base(Strings.SearchWindowTitle)
    {
        this.searchViewModel = searchViewModel;
        this.uiBuilder = uiBuilder;
        this.windowSystem = windowSystem;

        this.windowSystem.AddWindow(this);
        this.uiBuilder.OpenMainUi += Show;
        languageSwitcher.OnLanguageChangedEvent += (_, _) => WindowName = Strings.SearchWindowTitle;

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(300, 200),
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
        windowJustOpened = true;
    }

    public override void Draw()
    {
        DrawSearchBar();
        DrawSearchResults();
        windowJustOpened = false;
    }

    public void Show()
    {
        IsOpen = true;
    }

    public void ShowAndSearch(string searchText)
    {
        IsOpen = true;
        searchViewModel.SearchText = searchText;
        searchViewModel.ExecuteSearch();
    }

    private void DrawSearchBar()
    {
        var searchText = searchViewModel.SearchText;
        var enterPressed = ImGui.InputText(
            "",
            ref searchText,
            100,
            ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.AutoSelectAll
        );
        searchViewModel.SearchText = searchText;

        if (enterPressed || windowJustOpened)
        {
            ImGui.SetKeyboardFocusHere(-1);
        }

        ImGui.SameLine();
        var searchButtonPressed = ImGui.Button(Strings.Search);

        if (enterPressed || searchButtonPressed)
        {
            searchViewModel.ExecuteSearch();
        }
    }

    private void DrawSearchResults()
    {
        if (searchViewModel.SearchResultsAreLoading)
        {
            ImGui.Text(Strings.Loading);
        }

        if (searchViewModel.SearchResults is not null)
        {
            if (searchViewModel.SearchResults.Any())
            {
                ImGui.BeginTable(
                    "SearchResults",
                    2,
                    ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit
                );
                ImGui.TableSetupColumn(Strings.Icon);
                ImGui.TableSetupColumn(Strings.Names);
                ImGui.TableHeadersRow();
                foreach (var searchResult in searchViewModel.SearchResults)
                {
                    ImGui.TableNextColumn();
                    if (searchResult.IconTexture is not null)
                    {
                        ImGui.Image(searchResult.IconTexture.ImGuiHandle, CalculateImageSize(searchResult.IconTexture));
                    }

                    ImGui.TableNextColumn();
                    ImGui.Text($"{Strings.English}:\n{Strings.French}:\n{Strings.German}:\n{Strings.Japanese}:\n");
                    ImGui.SameLine();
                    ImGui.Text(
                        $"{searchResult.LocalisedNames[Language.English]}\n{searchResult.LocalisedNames[Language.French]
                        }\n{searchResult.LocalisedNames[Language.German]}\n{
                            searchResult.LocalisedNames[Language.Japanese]}"
                    );
                }

                ImGui.EndTable();
            }
            else
            {
                ImGui.Text(Strings.NoResults);
            }
        }
    }

    private static Vector2 CalculateImageSize(IDalamudTextureWrap image)
    {
        var scaleRatio = Math.Min(MaxImageSize / (float)image.Width, MaxImageSize / (float)image.Height);
        return new Vector2(image.Width * scaleRatio, image.Height * scaleRatio);
    }

    ~SearchView()
    {
        Dispose();
    }
}
