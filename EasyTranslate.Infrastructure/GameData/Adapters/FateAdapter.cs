using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class FateAdapter : IContentTypeAdapter<Fate>
{
    public Func<Fate, bool> WhereClause(string searchName)
    {
        return fate => fate.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Fate, Content> MapToContent(
        Fate english,
        Fate french,
        Fate german,
        Fate japanese
    )
    {
        return fate => new Content(
            ContentType.Fate,
            fate.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
