namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class QuestAdapter : IContentTypeAdapter<Quest>
{
    private const uint QuestIcon = 71221;

    public Func<Quest, bool> WhereClause(string searchName)
    {
        return quest => quest.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Quest, Content> MapToContent(
        ExcelSheet<Quest> englishSheet,
        ExcelSheet<Quest> frenchSheet,
        ExcelSheet<Quest> germanSheet,
        ExcelSheet<Quest> japaneseSheet
    )
    {
        return quest => new Content(
            ContentType.Quest,
            QuestIcon,
            englishSheet.GetRow(quest.RowId)!.Name.RawString,
            frenchSheet.GetRow(quest.RowId)!.Name.RawString,
            germanSheet.GetRow(quest.RowId)!.Name.RawString,
            japaneseSheet.GetRow(quest.RowId)!.Name.RawString
        );
    }
}
