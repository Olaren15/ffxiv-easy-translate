﻿namespace EasyTranslate.DalamudPlugin.Search;

using System.Collections.Generic;
using System.Linq;
using Dalamud.Plugin.Services;
using EasyTranslate.Domain.Entities;

public class ItemMapper(ITextureProvider textureProvider)
{
    public PresentableContent ConvertToPresentableItem(Content content)
    {
        return new PresentableContent(
            content.Id,
            content.IconId,
            content.IconId.HasValue ? textureProvider.GetIcon(content.IconId.Value) : null,
            content.LocalisedNames
        );
    }

    public IEnumerable<PresentableContent> ConvertToPresentableItems(IEnumerable<Content> items)
    {
        return items.Select(ConvertToPresentableItem);
    }
}
