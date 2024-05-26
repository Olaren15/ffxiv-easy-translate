using System.ComponentModel;
using Lumina.Data;

namespace EasyTranslate.Infrastructure.GameData;

using Lumina_Language = Language;
using Language = Domain.Entities.Language;

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
