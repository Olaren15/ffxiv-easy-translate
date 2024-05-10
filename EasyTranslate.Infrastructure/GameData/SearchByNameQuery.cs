using ExcelModule = Lumina.Excel.ExcelModule;
using ExcelRow = Lumina.Excel.ExcelRow;
using Lumina_Language = Lumina.Data.Language;

namespace EasyTranslate.Infrastructure.GameData;

using Domain.Entities;

public class SearchByNameQuery<T>(ExcelModule excelModule, IContentTypeAdapter<T> adapter)
    : ISearchByNameQuery where T : ExcelRow
{
    public IEnumerable<Content> Execute(string searchName, Lumina_Language searchLanguage)
    {
        return excelModule
               .GetSheet<T>(searchLanguage)
               ?.GetCachelessEnumerator()
               .Where(adapter.WhereClause(searchName))
               .Take(100)
               .Select(
                   adapter.MapToContent(
                       excelModule.GetSheet<T>(Lumina_Language.English)!,
                       excelModule.GetSheet<T>(Lumina_Language.French)!,
                       excelModule.GetSheet<T>(Lumina_Language.German)!,
                       excelModule.GetSheet<T>(Lumina_Language.Japanese)!
                   )
               )
               .ToList() // Bad iteration performance if we don't transform the results to a list
               ?? [];
    }
}
