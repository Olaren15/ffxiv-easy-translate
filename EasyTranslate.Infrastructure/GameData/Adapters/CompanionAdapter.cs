using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class CompanionAdapter : IContentTypeAdapter<CompanionLite>
{
    public Func<CompanionLite, bool> WhereClause(string searchName)
    {
        return minion => minion.Singular.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<CompanionLite, Content> MapToContent(
        CompanionLite english,
        CompanionLite french,
        CompanionLite german,
        CompanionLite japanese
    )
    {
        return minion => new Content(
            ContentType.Minion,
            minion.Icon,
            english.Singular.RawString,
            french.Singular.RawString,
            german.Singular.RawString,
            japanese.Singular.RawString);
    }
}
