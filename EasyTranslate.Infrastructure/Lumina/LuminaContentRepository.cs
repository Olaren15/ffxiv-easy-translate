using Lumina_Language = Lumina.Data.Language;

namespace EasyTranslate.Infrastructure.Lumina;

using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.Lumina.Sheets;

public class LuminaContentRepository(
    SheetQuery sheetQuery,
    AchievementSheetAdapter achievementSheetAdapter,
    ItemSheetAdapter itemSheetAdapter
) : IContentRepository
{
    public async Task<IEnumerable<Content>> SearchByName(
        string name,
        Language searchLanguage,
        CancellationToken cancellationToken
    )
    {
        var luminaSearchLanguage = searchLanguage switch
        {
            Language.English => Lumina_Language.English,
            Language.French => Lumina_Language.French,
            Language.German => Lumina_Language.German,
            Language.Japanese => Lumina_Language.Japanese,
            var _ => Lumina_Language.English,
        };

        /*
         TODO: Achieve feature-parity with the previous XivApiContentRepository.
         Missing fields:
          Title, Action, CraftAction, Trait, PvPAction, PvPTrait, Status, BNpcName,
          ENpcResident, Companion, Mount, Leve, Emote, InstanceContent, Recipe, Fate, Quest, ContentFinderCondition,
          Balloon, BuddyEquip, Orchestrion, PlaceName, Weather, World, Map
         */
        var achievements = Task.Run(
            () => sheetQuery.SearchByName(name, luminaSearchLanguage, achievementSheetAdapter),
            cancellationToken
        );
        var items = Task.Run(
            () => sheetQuery.SearchByName(name, luminaSearchLanguage, itemSheetAdapter),
            cancellationToken
        );

        // TODO: Sort results by relevancy
        return (await achievements).Concat(await items);
    }
}
