namespace EasyTranslate.UseCase.ItemSearch;

using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;

public class SearchItemByNameCommand(IContentRepository contentRepository)
{
    public async Task<IEnumerable<Content>> SearchItemByName(
        string itemName,
        Language searchLanguage,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            // Don't need to search for no reason
            return Enumerable.Empty<Content>();
        }

        return await contentRepository.SearchByName(itemName, searchLanguage, cancellationToken);
    }
}
