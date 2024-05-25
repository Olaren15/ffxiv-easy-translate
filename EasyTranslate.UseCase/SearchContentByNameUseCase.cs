namespace EasyTranslate.UseCase;

using Domain.Comparers;
using Domain.Entities;
using Domain.Repositories;

public class SearchContentByNameUseCase(
    IContentRepository contentRepository,
    IStringSimilarityComparer nameComparer
)
{
    public async Task<IEnumerable<Content>> Execute(
        string searchQuery,
        Language searchLanguage,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            // No need to search
            return [];
        }

        var searchResults = await contentRepository.SearchByName(searchQuery, searchLanguage, cancellationToken);
        return searchResults.OrderBy(
                                content => nameComparer.Compare(
                                    content.NameForLanguage(searchLanguage).ToUpperInvariant(),
                                    searchQuery.ToUpperInvariant())
                            )
                            .Take(100);
    }
}
