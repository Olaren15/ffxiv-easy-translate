namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class LeveAdapter : IContentTypeAdapter<Leve>
{
    private const uint LevequestIcon = 71241;

    public Func<Leve, bool> WhereClause(string searchName)
    {
        return leve => leve.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Leve, Content> MapToContent(
        ExcelSheet<Leve> englishSheet,
        ExcelSheet<Leve> frenchSheet,
        ExcelSheet<Leve> germanSheet,
        ExcelSheet<Leve> japaneseSheet
    )
    {
        return leve => new Content(
            ContentType.LeveQuest,
            LevequestIcon,
            englishSheet.GetRow(leve.RowId)!.Name.RawString,
            frenchSheet.GetRow(leve.RowId)!.Name.RawString,
            germanSheet.GetRow(leve.RowId)!.Name.RawString,
            japaneseSheet.GetRow(leve.RowId)!.Name.RawString
        );
    }
}
