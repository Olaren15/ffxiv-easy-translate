using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class EmoteAdapter : IContentTypeAdapter<Emote>
{
    public Func<Emote, bool> WhereClause(string searchName)
    {
        return emote => emote.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Emote, Content> MapToContent(
        Emote english,
        Emote french,
        Emote german,
        Emote japanese
    )
    {
        return emote => new Content(
            ContentType.Emote,
            emote.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
