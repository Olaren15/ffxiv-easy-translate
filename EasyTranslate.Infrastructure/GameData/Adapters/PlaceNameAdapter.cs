using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class PlaceNameAdapter : IContentTypeAdapter<PlaceName>
{
    public Func<PlaceName, bool> WhereClause(string searchName)
    {
        return placeName => placeName.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<PlaceName, Content> MapToContent(
        PlaceName english,
        PlaceName french,
        PlaceName german,
        PlaceName japanese
    )
    {
        return _ => new Content(
            ContentType.Place,
            null,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
