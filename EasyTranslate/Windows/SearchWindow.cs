namespace EasyTranslate.Windows;

using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

public class SearchWindow : Window
{
    public SearchWindow() : base("Search", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue),
        };
    }

    public override void Draw()
    {
        ImGui.Text("Show a search bar here"); // TODO
    }
}
