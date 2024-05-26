using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;

namespace EasyTranslate.Infrastructure.GameData;

public class GameDataContentRepository(IEnumerable<ISearchByNameQuery> searhQueries) : IContentRepository
{
    public Task<IEnumerable<Content>> SearchByName(
        string searchName,
        Language searchLanguage,
        CancellationToken cancellationToken
    )
    {
        return Task.FromResult(
            searhQueries.SelectMany(searchQuery => searchQuery.Execute(searchName, searchLanguage.ToLuminaLanguage()))
                .Distinct()
        );
    }
}
