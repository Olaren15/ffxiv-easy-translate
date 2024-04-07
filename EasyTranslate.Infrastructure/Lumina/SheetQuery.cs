using ExcelModule = Lumina.Excel.ExcelModule;
using ExcelRow = Lumina.Excel.ExcelRow;
using Lumina_Language = Lumina.Data.Language;

namespace EasyTranslate.Infrastructure.Lumina;

using EasyTranslate.Domain.Entities;

public class SheetQuery(ExcelModule excelModule)
{
    public IEnumerable<Content> SearchByName<T>(
        string searchName,
        Lumina_Language searchLanguage,
        ISheetQueryAdapter<T> adapter
    ) where T : ExcelRow
    {
        return excelModule
               .GetSheet<T>(searchLanguage)
               ?.Where(adapter.WhereClause(searchName))
               .Take(100)
               .Select(
                   adapter.MapToContent(
                       excelModule.GetSheet<T>(Lumina_Language.English)!,
                       excelModule.GetSheet<T>(Lumina_Language.French)!,
                       excelModule.GetSheet<T>(Lumina_Language.German)!,
                       excelModule.GetSheet<T>(Lumina_Language.Japanese)!
                   )
               )
               .ToArray() // Bad iteration performance if we don't transform the results in an array
               ?? Enumerable.Empty<Content>();
    }
}
