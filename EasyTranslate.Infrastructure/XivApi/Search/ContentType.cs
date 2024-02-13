namespace EasyTranslate.Infrastructure.XivApi.Search;

using System.Text.Json.Serialization;

public record ContentType([property: JsonPropertyName("IconID")] uint? IconId);
