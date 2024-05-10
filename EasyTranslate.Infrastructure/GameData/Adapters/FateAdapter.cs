namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class FateAdapter : IContentTypeAdapter<Fate>
{
    public Func<Fate, bool> WhereClause(string searchName)
    {
        return fate => fate.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Fate, Content> MapToContent(
        ExcelSheet<Fate> englishSheet,
        ExcelSheet<Fate> frenchSheet,
        ExcelSheet<Fate> germanSheet,
        ExcelSheet<Fate> japaneseSheet
    )
    {
        return fate => new Content(
            ContentType.Fate,
            fate.Icon,
            englishSheet.GetRow(fate.RowId)!.Name.RawString,
            frenchSheet.GetRow(fate.RowId)!.Name.RawString,
            germanSheet.GetRow(fate.RowId)!.Name.RawString,
            japaneseSheet.GetRow(fate.RowId)!.Name.RawString
        );
    }
}
