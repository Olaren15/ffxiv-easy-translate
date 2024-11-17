using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class EventItemAdapter : IContentTypeAdapter<EventItem>
{
    public Func<EventItem, bool> WhereClause(string searchName)
    {
        return item => item.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<EventItem, Content> MapToContent(EventItem english, EventItem french, EventItem german,
        EventItem japanese)
    {
        return item => new Content(
            ContentType.Item,
            item.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            // The Name field is always empty in japanese
            japanese.Singular.ExtractText());
    }
}
