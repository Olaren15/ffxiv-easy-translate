using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class QuestAdapter : IContentTypeAdapter<Quest>
{
    private const uint QuestIcon = 71221;

    public Func<Quest, bool> WhereClause(string searchName)
    {
        return quest => quest.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Quest, Content> MapToContent(
        Quest english,
        Quest french,
        Quest german,
        Quest japanese
    )
    {
        return _ => new Content(
            ContentType.Quest,
            QuestIcon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
