namespace EasyTranslate.Domain.Entities;

public record Item(int Id, string IconUrl, uint? IconId, IDictionary<Language, string> LocalisedNames, string DetailsUrl);
