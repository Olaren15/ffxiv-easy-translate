namespace EasyTranslate.Infrastructure.GameData;

using Domain.Entities;
using Domain.Repositories;
using Lumina.Excel.GeneratedSheets2;

public class GameDataContentRepository(
    SearchByNameQuery<Achievement> achievementsQuery,
    SearchByNameQuery<Action> actionsQuery,
    SearchByNameQuery<Item> itemsQuery,
    SearchByNameQuery<Title> titlesQuery
) : IContentRepository
{
    public Task<IEnumerable<Content>> SearchByName(
        string searchName,
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
            _ => Lumina.Data.Language.English,
        };

        /*
         TODO: Achieve feature-parity with the previous XivApiContentRepository.
         Missing fields:
          CraftAction, Trait, PvPAction, PvPTrait, Status, BNpcName,
          ENpcResident, Companion, Mount, Leve, Emote, InstanceContent, Recipe, Fate, Quest, ContentFinderCondition,
          Balloon, BuddyEquip, Orchestrion, PlaceName, Weather, World, Map
         */

        // TODO: Sort results by relevancy
        return Task.FromResult(achievementsQuery
                               .Execute(searchName, luminaSearchLanguage)
                               .Concat(actionsQuery.Execute(searchName, luminaSearchLanguage))
                               .Concat(itemsQuery.Execute(searchName, luminaSearchLanguage))
                               .Concat(titlesQuery.Execute(searchName, luminaSearchLanguage)));
    }
}