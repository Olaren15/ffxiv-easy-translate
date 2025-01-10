using Dalamud.Game.Gui.ContextMenu;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Plugin.Services;
using EasyTranslate.DalamudPlugin.Attributes;
using EasyTranslate.DalamudPlugin.Localisation;
using EasyTranslate.DalamudPlugin.Resources;
using Lumina.Excel.Sheets;

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

    private void SearchTranslations(IMenuItemClickedArgs menuItemClickedArgs)
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

        string itemName;
        if (itemId >= 2000000)
        {
            // Event items are stuff in the key items tab of inventory afaik
            itemName = _dataManager.Excel.GetSheet<EventItem>().GetRow(itemId.Value).Name.ExtractText();
        }
        else
        {
            // Modulo by 500000 to remove Hq/Colletable status from id
            itemName = _dataManager.Excel.GetSheet<Item>().GetRow(itemId.Value % 500000).Name.ExtractText();
        }

        _searchView.ShowAndSearchWithGameLanguage(itemName);
    }
}
