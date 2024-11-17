using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class LeveAdapter : IContentTypeAdapter<Leve>
{
    private const uint LevequestIcon = 71241;

    public Func<Leve, bool> WhereClause(string searchName)
    {
        return leve => leve.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Leve, Content> MapToContent(
        Leve english,
        Leve french,
        Leve german,
        Leve japanese
    )
    {
        return _ => new Content(
            ContentType.LeveQuest,
            LevequestIcon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
