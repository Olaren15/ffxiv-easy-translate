using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class LeveAdapter : IContentTypeAdapter<LeveLite>
{
    private const uint LevequestIcon = 71241;

    public Func<LeveLite, bool> WhereClause(string searchName)
    {
        return leve => leve.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<LeveLite, Content> MapToContent(
        LeveLite english,
        LeveLite french,
        LeveLite german,
        LeveLite japanese
    )
    {
        return _ => new Content(
            ContentType.LeveQuest,
            LevequestIcon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
