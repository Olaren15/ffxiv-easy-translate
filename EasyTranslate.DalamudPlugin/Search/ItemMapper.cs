namespace EasyTranslate.DalamudPlugin.Search;

using System.Collections.Generic;
using System.Linq;
using Dalamud.Plugin.Services;
using EasyTranslate.Domain.Entities;

public class ItemMapper
{
    private readonly ITextureProvider textureProvider;
    public ItemMapper(ITextureProvider textureProvider)
    {
        this.textureProvider = textureProvider;
    }

    public PresentableItem ConvertToPresentableItem(Item item)
    {
        return new PresentableItem(
            item.Id,
            item.IconUrl,
            item.IconId,
            item.IconId.HasValue ? textureProvider.GetIcon(item.IconId.Value) : null,
            item.LocalisedNames,
            item.DetailsUrl
        );
    }

    public IEnumerable<PresentableItem> ConvertToPresentableItems(IEnumerable<Item> items)
    {
        return items.Select(ConvertToPresentableItem);
    }
}
