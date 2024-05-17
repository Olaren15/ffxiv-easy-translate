namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Sheets;
using ContentType = Domain.Entities.ContentType;

public class StatusAdapter : IContentTypeAdapter<StatusLite>
{
    public Func<StatusLite, bool> WhereClause(string searchName)
    {
        return status => status.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<StatusLite, Content> MapToContent(
        StatusLite english,
        StatusLite french,
        StatusLite german,
        StatusLite japanese
    )
    {
        return status => new Content(
            ContentType.Status,
            status.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
