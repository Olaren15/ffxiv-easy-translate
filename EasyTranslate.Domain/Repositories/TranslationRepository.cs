namespace EasyTranslate.Domain.Repositories;

using EasyTranslate.Domain.Entities;

public interface IItemRepository
{
    public IEnumerable<Item> SearchForItems(ItemSearchQuery query);
}
