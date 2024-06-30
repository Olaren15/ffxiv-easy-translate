using System;
using System.Numerics;
using Dalamud.Interface;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Windowing;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.DalamudPlugin.Resources;
using ImGuiNET;

namespace EasyTranslate.DalamudPlugin.Search;

public sealed class SearchView : Window, IDisposable
{
    private const int MaxImageSize = 80;
    private readonly SearchViewModel _searchViewModel;
    private readonly UiBuilder _uiBuilder;
    private readonly WindowSystem _windowSystem;

    private bool _windowJustOpened;

    public SearchView(
        SearchViewModel searchViewModel,
        UiBuilder uiBuilder,
        WindowSystem windowSystem,
        LanguageSwitcher languageSwitcher
    ) : base(Strings.SearchWindowTitle)
    {
        _searchViewModel = searchViewModel;
        _uiBuilder = uiBuilder;
        _windowSystem = windowSystem;

        _windowSystem.AddWindow(this);
        _uiBuilder.OpenMainUi += Show;
        languageSwitcher.OnLanguageChangedEvent += (_, _) => WindowName = Strings.SearchWindowTitle;

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(300, 200),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    }

    public void Dispose()
    {
        _windowSystem.RemoveWindow(this);
        _uiBuilder.OpenMainUi -= Show;
        GC.SuppressFinalize(this);
    }

    public override void OnOpen()
    {
        base.OnOpen();
        _windowJustOpened = true;
    }

    public override void Draw()
    {
        DrawSearchBar();
        DrawSearchResults();
        _windowJustOpened = false;
    }

    public void Show()
    {
        IsOpen = true;
    }

    public void ShowAndSearch(string searchText)
    {
        IsOpen = true;
        _searchViewModel.SearchText = searchText;
        _searchViewModel.ExecuteSearch();
    }

    private void DrawSearchBar()
    {
        string? searchText = _searchViewModel.SearchText;
        bool enterPressed = ImGui.InputText(
            "",
            ref searchText,
            100,
            ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.AutoSelectAll
        );
        _searchViewModel.SearchText = searchText;

        if (enterPressed || _windowJustOpened)
        {
            ImGui.SetKeyboardFocusHere(-1);
        }

        ImGui.SameLine();
        bool searchButtonPressed = ImGui.Button(Strings.Search);

        if (enterPressed || searchButtonPressed)
        {
            _searchViewModel.ExecuteSearch();
        }
    }

    private void DrawSearchResults()
    {
        if (_searchViewModel.SearchResultsAreLoading)
        {
            ImGui.Text(Strings.Loading);
            return;
        }

        if (_searchViewModel.SearchResults is null)
        {
            return;
        }

        if (_searchViewModel.SearchResults.Length != 0)
        {
            ImGui.BeginTable(
                "SearchResults",
                4,
                ImGuiTableFlags.Borders
                | ImGuiTableFlags.RowBg
                | ImGuiTableFlags.SizingFixedFit
                | ImGuiTableFlags.ScrollY
            );

            ImGui.TableSetupScrollFreeze(0, 1); // Make title row always visible
            ImGui.TableSetupColumn(Strings.Icon, ImGuiTableColumnFlags.WidthFixed, MaxImageSize);
            ImGui.TableSetupColumn(Strings.Names, ImGuiTableColumnFlags.WidthStretch);
            ImGui.TableSetupColumn(Strings.Type, ImGuiTableColumnFlags.WidthFixed);
            ImGui.TableSetupColumn(Strings.Actions, ImGuiTableColumnFlags.WidthFixed);
            ImGui.TableHeadersRow();

            for (int i = 0; i < _searchViewModel.SearchResults.Length; i++)
            {
                PresentableContent searchResult = _searchViewModel.SearchResults[i];
                ImGui.TableNextColumn();
                if (searchResult.IconTexture is not null)
                {
                    ImGui.Image(searchResult.IconTexture.ImGuiHandle, CalculateImageSize(searchResult.IconTexture));
                }

                ImGui.TableNextColumn();
                ImGui.Text($"{Strings.English}:\n{Strings.French}:\n{Strings.German}:\n{Strings.Japanese}:\n");
                ImGui.SameLine();
                ImGui.Text(
                    $"{searchResult.EnglishName}\n{searchResult.FrenchName}\n{searchResult.GermanName}\n{searchResult.JapaneseName}"
                );

                ImGui.TableNextColumn();
                ImGui.Text(searchResult.Type.LocalisedName());
                ImGui.SameLine();
                ImGui.Text("  "); // Fake padding lol

                ImGui.TableNextColumn();
                if (ImGui.Button(Strings.Copy + $"##{i}")) // Hamburger menu
                {
                    ImGui.OpenPopup($"copy-{i}");
                }

                DrawCopyPopup(i);
            }

            ImGui.EndTable();
        }
        else
        {
            ImGui.Text(Strings.NoResults);
        }
    }

    private void DrawCopyPopup(int i)
    {
        if (!ImGui.BeginPopup($"copy-{i}"))
        {
            return;
        }

        PresentableContent searchResult = _searchViewModel.SearchResults![i];

        if (ImGui.Selectable(Strings.English))
        {
            ImGui.SetClipboardText(searchResult.EnglishName);
        }

        if (ImGui.Selectable(Strings.French))
        {
            ImGui.SetClipboardText(searchResult.FrenchName);
        }

        if (ImGui.Selectable(Strings.German))
        {
            ImGui.SetClipboardText(searchResult.GermanName);
        }

        if (ImGui.Selectable(Strings.Japanese))
        {
            ImGui.SetClipboardText(searchResult.JapaneseName);
        }

        ImGui.EndPopup();
    }

    private static Vector2 CalculateImageSize(IDalamudTextureWrap image)
    {
        float scaleRatio = Math.Min(MaxImageSize / (float)image.Width, MaxImageSize / (float)image.Height);
        return new Vector2(image.Width * scaleRatio, image.Height * scaleRatio);
    }

    ~SearchView()
    {
        Dispose();
    }
}
