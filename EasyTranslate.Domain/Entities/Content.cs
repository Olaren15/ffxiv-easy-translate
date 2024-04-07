namespace EasyTranslate.Domain.Entities;

public record Content(uint Id, uint? IconId, IDictionary<Language, string> LocalisedNames);
