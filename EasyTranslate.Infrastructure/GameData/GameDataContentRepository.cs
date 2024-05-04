namespace EasyTranslate.Infrastructure.GameData;

using Domain.Entities;
using Domain.Repositories;

public class GameDataContentRepository(IEnumerable<ISearchByNameQuery> searhQueries) : IContentRepository
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
          Emote, InstanceContent, Recipe, Fate, Quest, ContentFinderCondition,
          Balloon, BuddyEquip, Orchestrion, PlaceName, Weather, World, Map
         */

        // TODO: Sort results by relevancy
        return Task.FromResult(
            searhQueries.SelectMany(searchQuery => searchQuery.Execute(searchName, luminaSearchLanguage))
                        .Distinct()
                        .ToList()
                as IEnumerable<Content>
        );
    }
}
