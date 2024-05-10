namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class PlaceNameAdapter : IContentTypeAdapter<PlaceName>
{
    public Func<PlaceName, bool> WhereClause(string searchName)
    {
        return placeName => placeName.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<PlaceName, Content> MapToContent(
        ExcelSheet<PlaceName> englishSheet,
        ExcelSheet<PlaceName> frenchSheet,
        ExcelSheet<PlaceName> germanSheet,
        ExcelSheet<PlaceName> japaneseSheet
    )
    {
        return placeName => new Content(
            ContentType.Place,
            null,
            englishSheet.GetRow(placeName.RowId)!.Name.RawString,
            frenchSheet.GetRow(placeName.RowId)!.Name.RawString,
            germanSheet.GetRow(placeName.RowId)!.Name.RawString,
            japaneseSheet.GetRow(placeName.RowId)!.Name.RawString
        );
    }
}
