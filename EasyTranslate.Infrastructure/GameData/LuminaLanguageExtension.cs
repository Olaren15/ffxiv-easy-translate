using System.ComponentModel;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.Infrastructure.GameData;

using Language = Language;
using Lumina_Language = Lumina.Data.Language;

internal static class LuminaLanguageExtension
{
    public static Lumina_Language ToLuminaLanguage(this Language language)
    {
        return language switch
        {
            Language.English => Lumina_Language.English,
            Language.French => Lumina_Language.French,
            Language.German => Lumina_Language.German,
            Language.Japanese => Lumina_Language.Japanese,
            _ => throw new InvalidEnumArgumentException(nameof(language), (int)language,
                typeof(Language))
        };
    }
}
