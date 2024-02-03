namespace EasyTranslate.UseCase.ItemSearch;

using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;

public class SearchItemByNameCommand
{
    private readonly IItemRepository itemRepository;

    public SearchItemByNameCommand(IItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public async Task<IEnumerable<Item>> SearchItemByName(string name, Language nameLanguage)
    {
        return await itemRepository.SearchByName(name, nameLanguage);
    }
}
