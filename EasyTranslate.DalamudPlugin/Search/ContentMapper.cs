using System.Collections.Generic;
using System.Linq;
using Dalamud.Plugin.Services;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Search;

public class ContentMapper(ITextureProvider textureProvider)
{
    private PresentableContent ConvertToPresentableItem(Content content)
    {
        return new PresentableContent(
            content.Type,
            content.IconId,
            content.IconId.HasValue ? textureProvider.GetFromGameIcon(content.IconId.Value) : null,
            content.EnglishName,
            content.FrenchName,
            content.GermanName,
            content.JapaneseName
        );
    }

    public PresentableContent[] ConvertToPresentableContents(IEnumerable<Content> items)
    {
        IEnumerable<PresentableContent> mapped = items.Select(ConvertToPresentableItem);
        return mapped as PresentableContent[] ?? mapped.ToArray();
    }
}
