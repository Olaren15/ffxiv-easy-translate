namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class BNpcNameAdapter : IContentTypeAdapter<BNpcName>
{
    public Func<BNpcName, bool> WhereClause(string searchName)
    {
        return npcName => npcName.Singular.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<BNpcName, Content> MapToContent(
        ExcelSheet<BNpcName> englishSheet,
        ExcelSheet<BNpcName> frenchSheet,
        ExcelSheet<BNpcName> germanSheet,
        ExcelSheet<BNpcName> japaneseSheet
    )
    {
        return npcName => new Content(
            ContentType.Npc,
            null,
            englishSheet.GetRow(npcName.RowId)!.Singular.RawString,
            frenchSheet.GetRow(npcName.RowId)!.Singular.RawString,
            germanSheet.GetRow(npcName.RowId)!.Singular.RawString,
            japaneseSheet.GetRow(npcName.RowId)!.Singular.RawString
        );
    }
}
