using Dalamud.Game;
using Dalamud.Game.Config;
using Dalamud.Plugin.Services;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Localisation;

public class LanguageUtils(IGameConfig gameConfig)
{
    public Language GetGameLanguage()
    {
        bool success = gameConfig.TryGet(SystemConfigOption.Language, out uint gameLanguageCode);
        ClientLanguage gameLanguage = success ? (ClientLanguage)gameLanguageCode : ClientLanguage.English;

        return gameLanguage switch
        {
            ClientLanguage.Japanese => Language.Japanese,
            ClientLanguage.English => Language.English,
            ClientLanguage.German => Language.German,
            ClientLanguage.French => Language.French,
            _ => Language.English
        };
    }
}
