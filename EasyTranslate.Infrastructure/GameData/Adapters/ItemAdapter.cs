namespace EasyTranslate.Infrastructure.GameData.Adapters;

using EasyTranslate.Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

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
            item.RowId,
            ContentType.Item,
            item.Icon,
            new Dictionary<Language, string>
            {
                { Language.English, englishSheet.GetRow(item.RowId)!.Name.RawString },
                { Language.French, frenchSheet.GetRow(item.RowId)!.Name.RawString },
                { Language.German, germanSheet.GetRow(item.RowId)!.Name.RawString },
                { Language.Japanese, japaneseSheet.GetRow(item.RowId)!.Name.RawString },
            }
        );
    }
}
