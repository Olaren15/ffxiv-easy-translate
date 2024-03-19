namespace EasyTranslate.UseCase.ItemSearch;

using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;

public class SearchItemByNameCommand(IItemRepository itemRepository)
{
    public async Task<IEnumerable<Item>> SearchItemByName(
        string itemName,
        Language searchLanguage,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            // Don't need to search for nothing
            return Enumerable.Empty<Item>();
        }

        return await itemRepository.SearchByName(itemName, searchLanguage, cancellationToken);
    }
}
