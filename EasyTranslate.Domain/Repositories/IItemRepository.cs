namespace EasyTranslate.Domain.Repositories;

using EasyTranslate.Domain.Entities;

public interface IItemRepository
{
    public Task<IEnumerable<Item>> SearchByName(string name, Language searchLanguage);
}
