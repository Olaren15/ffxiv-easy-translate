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
            npcName.RowId,
            ContentType.Npc,
            null,
            new Dictionary<Language, string>
            {
                { Language.English, englishSheet.GetRow(npcName.RowId)!.Singular.RawString },
                { Language.French, frenchSheet.GetRow(npcName.RowId)!.Singular.RawString },
                { Language.German, germanSheet.GetRow(npcName.RowId)!.Singular.RawString },
                { Language.Japanese, japaneseSheet.GetRow(npcName.RowId)!.Singular.RawString },
            }
        );
    }
}
