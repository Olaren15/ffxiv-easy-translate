namespace EasyTranslate.Domain.Entities;

public record Item(int Id, string IconUrl, IDictionary<Language, string> LocalisedNames, string DetailsUrl);
