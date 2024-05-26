using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class FateAdapter : IContentTypeAdapter<FateLite>
{
    public Func<FateLite, bool> WhereClause(string searchName)
    {
        return fate => fate.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<FateLite, Content> MapToContent(
        FateLite english,
        FateLite french,
        FateLite german,
        FateLite japanese
    )
    {
        return fate => new Content(
            ContentType.Fate,
            fate.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
