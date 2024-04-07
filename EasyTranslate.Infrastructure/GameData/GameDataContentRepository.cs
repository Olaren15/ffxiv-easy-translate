namespace EasyTranslate.Infrastructure.GameData;

using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;
using Lumina.Excel.GeneratedSheets2;

public class GameDataContentRepository(
    SearchByNameQuery<Achievement> achievementsQuery,
    SearchByNameQuery<Item> itemsQuery
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
            Language.English => Lumina.Data.Language.English,
            Language.French => Lumina.Data.Language.French,
            Language.German => Lumina.Data.Language.German,
            Language.Japanese => Lumina.Data.Language.Japanese,
            var _ => Lumina.Data.Language.English,
        };

        /*
         TODO: Achieve feature-parity with the previous XivApiContentRepository.
         Missing fields:
          Title, Action, CraftAction, Trait, PvPAction, PvPTrait, Status, BNpcName,
          ENpcResident, Companion, Mount, Leve, Emote, InstanceContent, Recipe, Fate, Quest, ContentFinderCondition,
          Balloon, BuddyEquip, Orchestrion, PlaceName, Weather, World, Map
         */
        var achievements = Task.Run(() => achievementsQuery.Execute(name, luminaSearchLanguage), cancellationToken);
        var items = Task.Run(() => itemsQuery.Execute(name, luminaSearchLanguage), cancellationToken);

        // TODO: Sort results by relevancy
        return (await achievements).Concat(await items);
    }
}
