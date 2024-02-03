namespace EasyTranslate.UseCase.ItemSearch;

using EasyTranslate.Domain.Entities;

public interface ISearchItemCommand
{
    public IEnumerable<Item> SearchItem(ItemSearchQuery query);
}
