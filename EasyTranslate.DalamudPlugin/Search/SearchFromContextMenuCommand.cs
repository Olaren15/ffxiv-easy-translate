namespace EasyTranslate.DalamudPlugin.Search;

using System;
using Dalamud.ContextMenu;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Plugin.Services;
using Dalamud.Utility;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.DalamudPlugin.Resources;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;

public sealed class SearchFromContextMenuCommand
{
    private readonly IDataManager dataManager;
    private readonly SearchView searchView;
    private InventoryContextMenuItem inventoryContextMenuItem;

    public SearchFromContextMenuCommand(
        DalamudContextMenu contextMenu,
        IDataManager dataManager,
        SearchView searchView,
        LanguageSwitcher languageSwitcher
    )
    {
        this.dataManager = dataManager;
        this.searchView = searchView;

        inventoryContextMenuItem = CreateInventoryContextMenuItem();
        contextMenu.OnOpenInventoryContextMenu += args => args.AddCustomItem(inventoryContextMenuItem);
        languageSwitcher.OnLanguageChangedEvent +=
            (_, _) => inventoryContextMenuItem = CreateInventoryContextMenuItem();
    }

    private InventoryContextMenuItem CreateInventoryContextMenuItem()
    {
        return new InventoryContextMenuItem(
            new SeString(new TextPayload(Strings.SearchTranslations)),
            args => SearchTranslations(args.ItemId),
            true
        );
    }

    private void SearchTranslations(uint itemId)
    {
        ExcelRow? item;
        if (itemId >= 2000000)
        {
            // Event items are stuff in the key items tab of inventory afaik
            item = dataManager.Excel.GetSheet<EventItem>()?.GetRow(itemId);
        }
        else
        {
            // Not sure why we modulo by 500000, but I stole the code from simple tweaks so i'll trust it lol
            item = dataManager.Excel.GetSheet<Item>()?.GetRow(itemId % 500000);
        }

        if (item is null)
        {
            return;
        }

        var itemName = item switch
        {
            Item item1 => item1.Name.ToDalamudString().TextValue,
            EventItem eventItem => eventItem.Name.ToDalamudString().TextValue,
            var _ => throw new InvalidOperationException(),
        };

        searchView.ShowAndSearch(itemName);
    }
}
