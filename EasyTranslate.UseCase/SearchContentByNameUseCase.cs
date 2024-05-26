using EasyTranslate.Domain.Comparers;
using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;

namespace EasyTranslate.UseCase;

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

        IEnumerable<Content> searchResults = await contentRepository.SearchByName(
            searchQuery,
            searchLanguage,
            cancellationToken
        );

        return searchResults.OrderBy(
                content => nameComparer.Compare(
                    content.NameForLanguage(searchLanguage).ToUpperInvariant(),
                    searchQuery.ToUpperInvariant()
                )
            )
            .Take(100);
    }
}
