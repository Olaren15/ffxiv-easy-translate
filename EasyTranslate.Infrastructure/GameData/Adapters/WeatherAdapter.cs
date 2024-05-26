using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class WeatherAdapter : IContentTypeAdapter<WeatherLite>
{
    public Func<WeatherLite, bool> WhereClause(string searchName)
    {
        return weather => weather.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<WeatherLite, Content> MapToContent(
        WeatherLite english,
        WeatherLite french,
        WeatherLite german,
        WeatherLite japanese
    )
    {
        return weather => new Content(
            ContentType.Weather,
            (uint)weather.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
