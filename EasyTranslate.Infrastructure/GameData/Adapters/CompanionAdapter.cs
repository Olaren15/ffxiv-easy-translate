namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class CompanionAdapter : IContentTypeAdapter<Companion>
{
    public Func<Companion, bool> WhereClause(string searchName)
    {
        return minion => minion.Singular.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Companion, Content> MapToContent(
        ExcelSheet<Companion> englishSheet,
        ExcelSheet<Companion> frenchSheet,
        ExcelSheet<Companion> germanSheet,
        ExcelSheet<Companion> japaneseSheet
    )
    {
        return minion => new Content(
            ContentType.Minion,
            minion.Icon,
            englishSheet.GetRow(minion.RowId)!.Singular.RawString,
            frenchSheet.GetRow(minion.RowId)!.Singular.RawString,
            germanSheet.GetRow(minion.RowId)!.Singular.RawString,
            japaneseSheet.GetRow(minion.RowId)!.Singular.RawString);
    }
}
