namespace EasyTranslate.DalamudPlugin.Search;

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
            content.EnglishName,
            content.FrenchName,
            content.GermanName,
            content.JapaneseName
        );
    }

    public PresentableContent[] ConvertToPresentableContents(IEnumerable<Content> items)
    {
        var mapped = items.Select(ConvertToPresentableItem);
        return mapped as PresentableContent[] ?? mapped.ToArray();
    }
}
