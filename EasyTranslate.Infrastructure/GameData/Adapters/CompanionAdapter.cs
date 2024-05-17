namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Sheets;
using ContentType = Domain.Entities.ContentType;

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
