namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class CraftActionAdapter : IContentTypeAdapter<CraftAction>
{
    public Func<CraftAction, bool> WhereClause(string searchName)
    {
        return craftAction => craftAction.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<CraftAction, Content> MapToContent(
        ExcelSheet<CraftAction> englishSheet,
        ExcelSheet<CraftAction> frenchSheet,
        ExcelSheet<CraftAction> germanSheet,
        ExcelSheet<CraftAction> japaneseSheet
    )
    {
        return craftAction => new Content(
            ContentType.Action,
            craftAction.Icon,
            englishSheet.GetRow(craftAction.RowId)!.Name.RawString,
            frenchSheet.GetRow(craftAction.RowId)!.Name.RawString,
            germanSheet.GetRow(craftAction.RowId)!.Name.RawString,
            japaneseSheet.GetRow(craftAction.RowId)!.Name.RawString
        );
    }
}
