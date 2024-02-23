namespace EasyTranslate.Infrastructure.XivApi.Search;

using System.Text.Json.Serialization;

// ReSharper disable once ClassNeverInstantiated.Global
public record ContentType([property: JsonPropertyName("IconID")] uint? IconId);
