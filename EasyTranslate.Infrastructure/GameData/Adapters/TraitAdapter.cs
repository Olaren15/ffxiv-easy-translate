namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class TraitAdapter : IContentTypeAdapter<Trait>
{
    public Func<Trait, bool> WhereClause(string searchName)
    {
        return trait => trait.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase)
                        && trait.ClassJob.Row != 0;
    }

    public Func<Trait, Content> MapToContent(
        ExcelSheet<Trait> englishSheet,
        ExcelSheet<Trait> frenchSheet,
        ExcelSheet<Trait> germanSheet,
        ExcelSheet<Trait> japaneseSheet
    )
    {
        return trait => new Content(
            ContentType.Trait,
            (uint?)trait.Icon,
            englishSheet.GetRow(trait.RowId)!.Name.RawString,
            frenchSheet.GetRow(trait.RowId)!.Name.RawString,
            germanSheet.GetRow(trait.RowId)!.Name.RawString,
            japaneseSheet.GetRow(trait.RowId)!.Name.RawString
        );
    }
}
