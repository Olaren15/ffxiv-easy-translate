using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.GameData.Sheets;
using Lumina.Excel;

namespace EasyTranslate.Infrastructure.GameData;

public class GameDataContentRepository(IEnumerable<ISearchByNameQuery> searhQueries, ExcelModule excelModule)
    : IContentRepository
{
    public Task<IEnumerable<Content>> SearchByName(
        string searchName,
        Language searchLanguage,
        CancellationToken cancellationToken
    )
    {
        return Task.FromResult(
            searhQueries.SelectMany(searchQuery => searchQuery.Execute(searchName, searchLanguage.ToLuminaLanguage()))
                .Distinct()
        );
    }

    public Task<string?> GetItemName(uint itemId, Language searchLanguage, CancellationToken cancellationToken)
    {
        ExcelRow? item;
        if (itemId >= 2000000)
        {
            item = excelModule.GetSheet<EventItemLite>(searchLanguage.ToLuminaLanguage())?.GetRow(itemId);
        }
        else
        {
            uint itemIdNoHqNoCollectable = itemId % 500000;
            item = excelModule.GetSheet<ItemLite>(searchLanguage.ToLuminaLanguage())?.GetRow(itemIdNoHqNoCollectable);
        }

        string? name = item switch
        {
            null => null,
            ItemLite item1 => item1.Name.RawString,
            EventItemLite eventItem when searchLanguage == Language.Japanese => eventItem.Singular.RawString,
            EventItemLite eventItem => eventItem.Name.RawString,
            _ => throw new InvalidOperationException()
        };

        return Task.FromResult(name);
    }
}
