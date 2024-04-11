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
        return title =>
        {
            var englishTitle = englishSheet.GetRow(title.RowId)!;
            var frenchTitle = frenchSheet.GetRow(title.RowId)!;
            var germanTitle = germanSheet.GetRow(title.RowId)!;
            var japaneseTitle = japaneseSheet.GetRow(title.RowId)!;

            return new Content(
                title.RowId,
                ContentType.Title,
                null,
                new Dictionary<Language, string>
                {
                    { Language.English, $"{englishTitle.Masculine.RawString} / {englishTitle.Feminine}" },
                    { Language.French, $"{frenchTitle.Masculine.RawString} / {frenchTitle.Feminine}" },
                    { Language.German, $"{germanTitle.Masculine.RawString} / {germanTitle.Feminine}" },
                    { Language.Japanese, $"{japaneseTitle.Masculine.RawString} / {japaneseTitle.Feminine}" },
                }
            );
        };
    }
}
