namespace EasyTranslate.Domain.Parsers;

using EasyTranslate.Domain.Entities;

public static class LanguageParser
{
    public static Language FromIsoCode(string code)
    {
        return code switch
        {
            "en" => Language.English,
            "fr" => Language.French,
            "de" => Language.German,
            "ja" => Language.Japanese,
            var _ => throw new ArgumentOutOfRangeException(nameof(code), code, null),
        };
    }
}
