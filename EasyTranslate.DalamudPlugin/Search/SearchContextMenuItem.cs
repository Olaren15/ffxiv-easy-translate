using System;
using Dalamud.Game.Gui.ContextMenu;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Plugin.Services;
using Dalamud.Utility;
using EasyTranslate.DalamudPlugin.Attributes;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.DalamudPlugin.Resources;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;

namespace EasyTranslate.DalamudPlugin.Search;

[EntryPoint]
public sealed class SearchContextMenuItem
{
    private readonly IDataManager _dataManager;
    private readonly SearchView _searchView;

    public SearchContextMenuItem(
        IDataManager dataManager,
        SearchView searchView,
        LanguageSwitcher languageSwitcher,
        IContextMenu contextMenu
    )
    {
        _dataManager = dataManager;
        _searchView = searchView;

        MenuItem menuItem = CreateMenuItem();
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
            Priority = 10000
        };
    }

    private void SearchTranslations(MenuItemClickedArgs menuItemClickedArgs)
    {
        if (menuItemClickedArgs.MenuType != ContextMenuType.Inventory)
        {
            // Only inventory menus are supported for now
            return;
        }

        uint? itemId = (menuItemClickedArgs.Target as MenuTargetInventory)?.TargetItem?.ItemId;
        if (itemId is null)
        {
            return;
        }

        ExcelRow? item;
        if (itemId >= 2000000)
        {
            // Event items are stuff in the key items tab of inventory afaik
            item = _dataManager.Excel.GetSheet<EventItem>()?.GetRow(itemId.Value);
        }
        else
        {
            // Not sure why we modulo by 500000, but I stole the code from simple tweaks so i'll trust it lol
            item = _dataManager.Excel.GetSheet<Item>()?.GetRow(itemId.Value % 500000);
        }

        if (item is null)
        {
            return;
        }

        string itemName = item switch
        {
            Item item1 => item1.Name.ToDalamudString().TextValue,
            EventItem eventItem => eventItem.Name.ToDalamudString().TextValue,
            _ => throw new InvalidOperationException()
        };

        _searchView.ShowAndSearch(itemName);
    }
}
