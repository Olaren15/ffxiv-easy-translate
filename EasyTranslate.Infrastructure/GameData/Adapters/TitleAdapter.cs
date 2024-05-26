using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class TitleAdapter : IContentTypeAdapter<TitleLite>
{
    public Func<TitleLite, bool> WhereClause(string searchName)
    {
        return title => title.Feminine.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase)
                        || title.Masculine.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<TitleLite, Content> MapToContent(
        TitleLite english,
        TitleLite french,
        TitleLite german,
        TitleLite japanese
    )
    {
        return _ => new Content(
            ContentType.Title,
            null,
            FormatTitle(english),
            FormatTitle(french),
            FormatTitle(german),
            FormatTitle(japanese)
        );
    }

    private static string FormatTitle(TitleLite title)
    {
        return title.Masculine.RawString == title.Feminine.RawString
            ? title.Feminine.RawString
            : $"{title.Masculine.RawString} / {title.Feminine.RawString}";
    }
}
