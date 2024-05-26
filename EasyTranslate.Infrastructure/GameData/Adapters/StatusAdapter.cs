using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

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
