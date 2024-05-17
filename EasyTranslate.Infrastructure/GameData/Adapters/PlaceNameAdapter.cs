namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Sheets;
using ContentType = Domain.Entities.ContentType;

public class PlaceNameAdapter : IContentTypeAdapter<PlaceNameLite>
{
    public Func<PlaceNameLite, bool> WhereClause(string searchName)
    {
        return placeName => placeName.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<PlaceNameLite, Content> MapToContent(
        PlaceNameLite english,
        PlaceNameLite french,
        PlaceNameLite german,
        PlaceNameLite japanese
    )
    {
        return _ => new Content(
            ContentType.Place,
            null,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
