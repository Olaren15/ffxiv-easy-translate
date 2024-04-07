namespace EasyTranslate.Infrastructure.GameData;

using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;
using Lumina.Excel.GeneratedSheets2;

public class GameDataContentRepository(
    SearchByNameQuery<Achievement> achievementsQuery,
    SearchByNameQuery<Action> actionsQuery,
    SearchByNameQuery<Item> itemsQuery,
    SearchByNameQuery<Title> titlesQuery
) : IContentRepository
{
    public async Task<IEnumerable<Content>> SearchByName(
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
            var _ => Lumina.Data.Language.English,
        };

        /*
         TODO: Achieve feature-parity with the previous XivApiContentRepository.
         Missing fields:
          CraftAction, Trait, PvPAction, PvPTrait, Status, BNpcName,
          ENpcResident, Companion, Mount, Leve, Emote, InstanceContent, Recipe, Fate, Quest, ContentFinderCondition,
          Balloon, BuddyEquip, Orchestrion, PlaceName, Weather, World, Map
         */
        var achievements = Task.Run(
            () => achievementsQuery.Execute(searchName, luminaSearchLanguage),
            cancellationToken
        );
        var actions = Task.Run(() => actionsQuery.Execute(searchName, luminaSearchLanguage), cancellationToken);
        var items = Task.Run(() => itemsQuery.Execute(searchName, luminaSearchLanguage), cancellationToken);
        var titles = Task.Run(() => titlesQuery.Execute(searchName, luminaSearchLanguage), cancellationToken);

        // TODO: Sort results by relevancy
        return (await achievements).Concat(await actions).Concat(await items).Concat(await titles);
    }
}
