using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class TitleAdapter : IContentTypeAdapter<Title>
{
    public Func<Title, bool> WhereClause(string searchName)
    {
        return title => title.Feminine.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase)
                        || title.Masculine.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Title, Content> MapToContent(
        Title english,
        Title french,
        Title german,
        Title japanese
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

    private static string FormatTitle(Title title)
    {
        string masculine = title.Masculine.ExtractText();
        string feminine = title.Feminine.ExtractText();
        return masculine == feminine ? feminine : $"{masculine} / {feminine}";
    }
}
