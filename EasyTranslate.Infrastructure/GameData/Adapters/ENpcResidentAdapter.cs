namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class ENpcResidentAdapter : IContentTypeAdapter<ENpcResident>
{
    public Func<ENpcResident, bool> WhereClause(string searchName)
    {
        return npcName => npcName.Singular.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ENpcResident, Content> MapToContent(
        ExcelSheet<ENpcResident> englishSheet,
        ExcelSheet<ENpcResident> frenchSheet,
        ExcelSheet<ENpcResident> germanSheet,
        ExcelSheet<ENpcResident> japaneseSheet
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
