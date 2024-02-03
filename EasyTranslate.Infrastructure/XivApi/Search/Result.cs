﻿namespace EasyTranslate.Infrastructure.XivApi.Search;

using System.Text.Json.Serialization;

public record Result(
    [property: JsonPropertyName("ID")] int Id,
    [property: JsonPropertyName("Icon")] string IconUrl,
    [property: JsonPropertyName("Name_en")]
    string EnglishName,
    [property: JsonPropertyName("Name_fr")]
    string FrenchName,
    [property: JsonPropertyName("Name_de")]
    string GermanName,
    [property: JsonPropertyName("Name_ja")]
    string JapaneseName,
    [property: JsonPropertyName("Url")] string DetailsUrl
);