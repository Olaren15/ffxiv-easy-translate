namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class ItemAdapter : IContentTypeAdapter<Item>
{
    public Func<Item, bool> WhereClause(string searchName)
    {
        return item => item.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Item, Content> MapToContent(
        ExcelSheet<Item> englishSheet,
        ExcelSheet<Item> frenchSheet,
        ExcelSheet<Item> germanSheet,
        ExcelSheet<Item> japaneseSheet
    )
    {
        return item => new Content(
            ContentType.Item,
            item.Icon,
            englishSheet.GetRow(item.RowId)!.Name.RawString,
            frenchSheet.GetRow(item.RowId)!.Name.RawString,
            germanSheet.GetRow(item.RowId)!.Name.RawString,
            japaneseSheet.GetRow(item.RowId)!.Name.RawString
        );
    }
}
