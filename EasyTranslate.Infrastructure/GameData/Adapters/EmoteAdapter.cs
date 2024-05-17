namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Sheets;
using ContentType = Domain.Entities.ContentType;

public class EmoteAdapter : IContentTypeAdapter<EmoteLite>
{
    public Func<EmoteLite, bool> WhereClause(string searchName)
    {
        return emote => emote.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<EmoteLite, Content> MapToContent(
        EmoteLite english,
        EmoteLite french,
        EmoteLite german,
        EmoteLite japanese
    )
    {
        return emote => new Content(
            ContentType.Emote,
            emote.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
