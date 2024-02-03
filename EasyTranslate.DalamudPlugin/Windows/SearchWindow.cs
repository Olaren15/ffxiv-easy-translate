namespace EasyTranslate.Windows;

using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

public class SearchWindow : Window
{
    private string searchText = "";
    private string? searchResults;

    public SearchWindow() : base("Search", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue),
        };
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
            searchResults = searchText;
        }

        if (searchResults is not null)
        {
            ImGui.Text(searchResults);
        }
    }
}
