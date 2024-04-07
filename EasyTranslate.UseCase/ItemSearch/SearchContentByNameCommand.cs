namespace EasyTranslate.UseCase.ItemSearch;

using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;

public class SearchContentByNameCommand(IContentRepository contentRepository)
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

        return await contentRepository.SearchByName(searchName, searchLanguage, cancellationToken);
    }
}
