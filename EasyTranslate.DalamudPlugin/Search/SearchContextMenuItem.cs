namespace EasyTranslate.DalamudPlugin.Search;

using System;
using Attributes;
using Dalamud.Game.Gui.ContextMenu;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Plugin.Services;
using Dalamud.Utility;
using Localisation;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using Resources;

[EntryPoint]
public sealed class SearchContextMenuItem
{
    private readonly IDataManager dataManager;
    private readonly SearchView searchView;
    private MenuItem menuItem;

    public SearchContextMenuItem(
        IDataManager dataManager,
        SearchView searchView,
        LanguageSwitcher languageSwitcher,
        IContextMenu contextMenu
    )
    {
        this.dataManager = dataManager;
        this.searchView = searchView;

        menuItem = CreateMenuItem();
        contextMenu.OnMenuOpened += args =>
        {
            if (args.MenuType == ContextMenuType.Inventory)
            {
                args.AddMenuItem(menuItem);
            }
        };
        languageSwitcher.OnLanguageChangedEvent += (_, _) => menuItem = CreateMenuItem();
    }

    private MenuItem CreateMenuItem()
    {
        return new MenuItem
        {
            IsEnabled = true,
            IsReturn = false,
            Name = new SeString(new TextPayload(Strings.SearchTranslations)),
            IsSubmenu = false,
            OnClicked = SearchTranslations,
            Prefix = null,
            PrefixChar = 'T',
            PrefixColor = 1,
            Priority = 10000,
        };
    }

    private void SearchTranslations(MenuItemClickedArgs menuItemClickedArgs)
    {
        if (menuItemClickedArgs.MenuType != ContextMenuType.Inventory)
        {
            // Only inventory menus are supported for now
            return;
        }

        var itemId = (menuItemClickedArgs.Target as MenuTargetInventory)?.TargetItem?.ItemId;
        if (itemId is null)
        {
            return;
        }

        ExcelRow? item;
        if (itemId >= 2000000)
        {
            // Event items are stuff in the key items tab of inventory afaik
            item = dataManager.Excel.GetSheet<EventItem>()?.GetRow(itemId.Value);
        }
        else
        {
            // Not sure why we modulo by 500000, but I stole the code from simple tweaks so i'll trust it lol
            item = dataManager.Excel.GetSheet<Item>()?.GetRow(itemId.Value % 500000);
        }

        if (item is null)
        {
            return;
        }

        var itemName = item switch
        {
            Item item1 => item1.Name.ToDalamudString().TextValue,
            EventItem eventItem => eventItem.Name.ToDalamudString().TextValue,
            _ => throw new InvalidOperationException(),
        };

        searchView.ShowAndSearch(itemName);
    }
}
