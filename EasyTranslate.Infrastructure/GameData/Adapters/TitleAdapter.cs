namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class TitleAdapter : IContentTypeAdapter<Title>
{
    public Func<Title, bool> WhereClause(string searchName)
    {
        return title => title.Feminine.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase)
                        || title.Masculine.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Title, Content> MapToContent(
        ExcelSheet<Title> englishSheet,
        ExcelSheet<Title> frenchSheet,
        ExcelSheet<Title> germanSheet,
        ExcelSheet<Title> japaneseSheet
    )
    {
        return title => new Content(
            ContentType.Title,
            null,
            FormatTitle(englishSheet.GetRow(title.RowId)!),
            FormatTitle(frenchSheet.GetRow(title.RowId)!),
            FormatTitle(germanSheet.GetRow(title.RowId)!),
            FormatTitle(japaneseSheet.GetRow(title.RowId)!)
        );
    }

    private static string FormatTitle(Title title)
    {
        return title.Masculine.RawString == title.Feminine.RawString
                   ? title.Feminine.RawString
                   : $"{title.Masculine.RawString} / {title.Feminine.RawString}";
    }
}
