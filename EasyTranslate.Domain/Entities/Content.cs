namespace EasyTranslate.Domain.Entities;

using System.ComponentModel;

public record Content(
    ContentType Type,
    uint? IconId,
    string EnglishName,
    string FrenchName,
    string GermanName,
    string JapaneseName
)
{
    public string NameForLanguage(Language language)
    {
        return language switch
        {
            Language.English => EnglishName,
            Language.French => FrenchName,
            Language.German => GermanName,
            Language.Japanese => JapaneseName,
            _ => throw new InvalidEnumArgumentException(nameof(language), (int)language, typeof(Language)),
        };
    }
}
