using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class WeatherAdapter : IContentTypeAdapter<Weather>
{
    public Func<Weather, bool> WhereClause(string searchName)
    {
        return weather => weather.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Weather, Content> MapToContent(
        Weather english,
        Weather french,
        Weather german,
        Weather japanese
    )
    {
        return weather => new Content(
            ContentType.Weather,
            (uint)weather.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
