namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class WeatherAdapter : IContentTypeAdapter<Weather>
{
    public Func<Weather, bool> WhereClause(string searchName)
    {
        return weather => weather.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Weather, Content> MapToContent(
        ExcelSheet<Weather> englishSheet,
        ExcelSheet<Weather> frenchSheet,
        ExcelSheet<Weather> germanSheet,
        ExcelSheet<Weather> japaneseSheet
    )
    {
        return weather => new Content(
            ContentType.Weather,
            (uint)weather.Icon,
            englishSheet.GetRow(weather.RowId)!.Name.RawString,
            frenchSheet.GetRow(weather.RowId)!.Name.RawString,
            germanSheet.GetRow(weather.RowId)!.Name.RawString,
            japaneseSheet.GetRow(weather.RowId)!.Name.RawString
        );
    }
}
