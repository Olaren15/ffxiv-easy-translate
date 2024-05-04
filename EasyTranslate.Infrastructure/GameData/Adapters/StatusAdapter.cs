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
            ContentType.Status,
            status.Icon,
            englishSheet.GetRow(status.RowId)!.Name.RawString,
            frenchSheet.GetRow(status.RowId)!.Name.RawString,
            germanSheet.GetRow(status.RowId)!.Name.RawString,
            japaneseSheet.GetRow(status.RowId)!.Name.RawString
        );
    }
}
