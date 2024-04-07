namespace EasyTranslate.Domain.Entities;

public record Content(uint Id, ContentType Type, uint? IconId, IDictionary<Language, string> LocalisedNames);
