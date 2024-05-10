namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class ContentFinderConditionAdapter : IContentTypeAdapter<ContentFinderCondition>
{
    public Func<ContentFinderCondition, bool> WhereClause(string searchName)
    {
        return content => content.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ContentFinderCondition, Content> MapToContent(
        ExcelSheet<ContentFinderCondition> englishSheet,
        ExcelSheet<ContentFinderCondition> frenchSheet,
        ExcelSheet<ContentFinderCondition> germanSheet,
        ExcelSheet<ContentFinderCondition> japaneseSheet
    )
    {
        return content => new Content(
            ContentType.Instance,
            content.ContentType.Value?.Icon,
            englishSheet.GetRow(content.RowId)!.Name.RawString,
            frenchSheet.GetRow(content.RowId)!.Name.RawString,
            germanSheet.GetRow(content.RowId)!.Name.RawString,
            japaneseSheet.GetRow(content.RowId)!.Name.RawString
        );
    }
}
