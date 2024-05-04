namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class MountAdapter : IContentTypeAdapter<Mount>
{
    public Func<Mount, bool> WhereClause(string searchName)
    {
        return mount => mount.Singular.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Mount, Content> MapToContent(
        ExcelSheet<Mount> englishSheet,
        ExcelSheet<Mount> frenchSheet,
        ExcelSheet<Mount> germanSheet,
        ExcelSheet<Mount> japaneseSheet
    )
    {
        return mount => new Content(
            ContentType.Mount,
            mount.Icon,
            englishSheet.GetRow(mount.RowId)!.Singular.RawString,
            frenchSheet.GetRow(mount.RowId)!.Singular.RawString,
            germanSheet.GetRow(mount.RowId)!.Singular.RawString,
            japaneseSheet.GetRow(mount.RowId)!.Singular.RawString
        );
    }
}
