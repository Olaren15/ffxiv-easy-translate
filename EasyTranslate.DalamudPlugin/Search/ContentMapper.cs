﻿namespace EasyTranslate.DalamudPlugin.Search;

using System.Collections.Generic;
using System.Linq;
using Dalamud.Plugin.Services;
using Domain.Entities;

public class ContentMapper(ITextureProvider textureProvider)
{
    public PresentableContent ConvertToPresentableItem(Content content)
    {
        return new PresentableContent(
            content.Type,
            content.IconId,
            content.IconId.HasValue ? textureProvider.GetIcon(content.IconId.Value) : null,
            content.englishName,
            content.frenchName,
            content.germanName,
            content.japaneseName
        );
    }

    public IEnumerable<PresentableContent> ConvertToPresentableItems(IEnumerable<Content> items)
    {
        return items.Select(ConvertToPresentableItem);
    }
}
