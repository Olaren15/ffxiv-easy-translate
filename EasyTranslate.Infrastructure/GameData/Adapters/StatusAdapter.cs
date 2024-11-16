using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class StatusAdapter : IContentTypeAdapter<Status>
{
    public Func<Status, bool> WhereClause(string searchName)
    {
        return status => status.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Status, Content> MapToContent(
        Status english,
        Status french,
        Status german,
        Status japanese
    )
    {
        return status => new Content(
            ContentType.Status,
            status.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
