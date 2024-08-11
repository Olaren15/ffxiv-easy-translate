using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class EventItemAdapter : IContentTypeAdapter<EventItemLite>
{
    public Func<EventItemLite, bool> WhereClause(string searchName)
    {
        return item => item.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<EventItemLite, Content> MapToContent(EventItemLite english, EventItemLite french, EventItemLite german,
        EventItemLite japanese)
    {
        return item => new Content(
            ContentType.Item,
            item.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            // The Name field is always empty in japanese
            japanese.Singular.RawString);
    }
}
