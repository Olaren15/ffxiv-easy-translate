namespace EasyTranslate.UseCase;

using Domain.Entities;
using Domain.Repositories;

public class SearchContentByNameCommand(
    IContentRepository contentRepository,
    SortContentByNameSimilarityCommand sortContentByNameSimilarityCommand
)
{
    public async Task<IEnumerable<Content>> Execute(
        string searchName,
        Language searchLanguage,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(searchName))
        {
            // Don't need to search for no reason
            return Enumerable.Empty<Content>();
        }

        var contentList = await contentRepository.SearchByName(searchName, searchLanguage, cancellationToken);
        return sortContentByNameSimilarityCommand.Execute(searchName, searchLanguage, contentList);
    }
}
