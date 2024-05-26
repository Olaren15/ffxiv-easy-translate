using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class QuestAdapter : IContentTypeAdapter<QuestLite>
{
    private const uint QuestIcon = 71221;

    public Func<QuestLite, bool> WhereClause(string searchName)
    {
        return quest => quest.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<QuestLite, Content> MapToContent(
        QuestLite english,
        QuestLite french,
        QuestLite german,
        QuestLite japanese
    )
    {
        return _ => new Content(
            ContentType.Quest,
            QuestIcon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
