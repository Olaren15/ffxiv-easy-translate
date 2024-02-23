namespace EasyTranslate.Infrastructure.XivApi.Search;

using System.Text.Json.Serialization;

// ReSharper disable once ClassNeverInstantiated.Global
public record Result(
    [property: JsonPropertyName("ID")] int Id,
    [property: JsonPropertyName("IconHD")] string IconUrl,
    [property: JsonPropertyName("IconID")] uint? IconId,
    [property: JsonPropertyName("IconObjectiveID")]
    uint? IconObjectiveId,
    [property: JsonPropertyName("Name_en")]
    string EnglishName,
    [property: JsonPropertyName("Name_fr")]
    string FrenchName,
    [property: JsonPropertyName("Name_de")]
    string GermanName,
    [property: JsonPropertyName("Name_ja")]
    string JapaneseName,
    [property: JsonPropertyName("Url")] string DetailsUrl,
    [property: JsonPropertyName("ContentType")]
    ContentType? ContentType
);
