namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class StatusAdapter : IContentTypeAdapter<Status>
{
    public Func<Status, bool> WhereClause(string searchName)
    {
        return status => status.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Status, Content> MapToContent(
        ExcelSheet<Status> englishSheet,
        ExcelSheet<Status> frenchSheet,
        ExcelSheet<Status> germanSheet,
        ExcelSheet<Status> japaneseSheet
    )
    {
        return status => new Content(
            status.RowId,
            ContentType.Status,
            status.Icon,
            new Dictionary<Language, string>
            {
                { Language.English, englishSheet.GetRow(status.RowId)!.Name.RawString },
                { Language.French, frenchSheet.GetRow(status.RowId)!.Name.RawString },
                { Language.German, germanSheet.GetRow(status.RowId)!.Name.RawString },
                { Language.Japanese, japaneseSheet.GetRow(status.RowId)!.Name.RawString },
            }
        );
    }
}
