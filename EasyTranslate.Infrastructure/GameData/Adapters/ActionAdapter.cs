namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class ActionAdapter : IContentTypeAdapter<Action>
{
    public Func<Action, bool> WhereClause(string searchName)
    {
        return action => action.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase)
                         && action.ClassJob.Row != uint.MaxValue; // Filter non-player actions
    }

    public Func<Action, Content> MapToContent(
        ExcelSheet<Action> englishSheet,
        ExcelSheet<Action> frenchSheet,
        ExcelSheet<Action> germanSheet,
        ExcelSheet<Action> japaneseSheet
    )
    {
        return action => new Content(
            action.RowId,
            ContentType.Action,
            action.Icon,
            new Dictionary<Language, string>
            {
                { Language.English, englishSheet.GetRow(action.RowId)!.Name.RawString },
                { Language.French, frenchSheet.GetRow(action.RowId)!.Name.RawString },
                { Language.German, germanSheet.GetRow(action.RowId)!.Name.RawString },
                { Language.Japanese, japaneseSheet.GetRow(action.RowId)!.Name.RawString },
            }
        );
    }
}
