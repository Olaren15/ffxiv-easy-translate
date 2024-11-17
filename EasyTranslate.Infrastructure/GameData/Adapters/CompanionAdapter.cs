using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class CompanionAdapter : IContentTypeAdapter<Companion>
{
    public Func<Companion, bool> WhereClause(string searchName)
    {
        return minion => minion.Singular.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Companion, Content> MapToContent(
        Companion english,
        Companion french,
        Companion german,
        Companion japanese
    )
    {
        return minion => new Content(
            ContentType.Minion,
            minion.Icon,
            english.Singular.ExtractText(),
            french.Singular.ExtractText(),
            german.Singular.ExtractText(),
            japanese.Singular.ExtractText());
    }
}
