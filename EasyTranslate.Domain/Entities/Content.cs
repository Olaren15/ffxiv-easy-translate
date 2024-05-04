namespace EasyTranslate.Domain.Entities;

public record Content(
    ContentType Type,
    uint? IconId,
    string englishName,
    string frenchName,
    string germanName,
    string japaneseName
);
