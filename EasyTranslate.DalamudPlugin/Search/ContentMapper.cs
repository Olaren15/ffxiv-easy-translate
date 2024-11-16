using System.Collections.Generic;
using System.Linq;
using Dalamud.Interface.Textures;
using Dalamud.Interface.Textures.Internal;
using Dalamud.Plugin.Services;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Search;

public class ContentMapper(ITextureProvider textureProvider, IPluginLog log)
{
    private PresentableContent ConvertToPresentableItem(Content content)
    {
        ISharedImmediateTexture? icon = null;
        if (content.IconId.HasValue)
        {
            try
            {
                icon = textureProvider.GetFromGameIcon(content.IconId.Value);
            }
            catch (IconNotFoundException)
            {
                // Because SE sometimes make mistakes and references invalid IDs :)
                log.Warning(
                    $"Could not retrieve icon id {content.IconId.Value}. Content name: {content.EnglishName}, Content type: {content.Type}");
            }
        }

        return new PresentableContent(
            content.Type,
            content.IconId,
            icon,
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
