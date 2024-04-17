namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class CraftActionAdapter : IContentTypeAdapter<CraftAction>
{
    public Func<CraftAction, bool> WhereClause(string searchName)
    {
        return craftAction => craftAction.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase)
                              && craftAction.ClassJob.Row != uint.MaxValue; // Filter non-player actions
    }

    public Func<CraftAction, Content> MapToContent(
        ExcelSheet<CraftAction> englishSheet,
        ExcelSheet<CraftAction> frenchSheet,
        ExcelSheet<CraftAction> germanSheet,
        ExcelSheet<CraftAction> japaneseSheet
    )
    {
        return craftAction => new Content(
            craftAction.RowId,
            ContentType.Action,
            craftAction.Icon,
            new Dictionary<Language, string>
            {
                { Language.English, englishSheet.GetRow(craftAction.RowId)!.Name.RawString },
                { Language.French, frenchSheet.GetRow(craftAction.RowId)!.Name.RawString },
                { Language.German, germanSheet.GetRow(craftAction.RowId)!.Name.RawString },
                { Language.Japanese, japaneseSheet.GetRow(craftAction.RowId)!.Name.RawString },
            }
        );
    }
}
