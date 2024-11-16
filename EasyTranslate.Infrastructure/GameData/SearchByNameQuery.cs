using EasyTranslate.Domain.Entities;
using Lumina.Excel;
using ExcelModule = Lumina.Excel.ExcelModule;
using Lumina_Language = Lumina.Data.Language;

namespace EasyTranslate.Infrastructure.GameData;

public class SearchByNameQuery<T>(ExcelModule excelModule, IContentTypeAdapter<T> adapter)
    : ISearchByNameQuery where T : struct, IExcelRow<T>
{
    public IEnumerable<Content> Execute(string searchName, Lumina_Language searchLanguage)
    {
        return excelModule
            .GetSheet<T>(searchLanguage)
            .Where(adapter.WhereClause(searchName))
            .Take(100)
            .Select(
                result =>
                    adapter.MapToContent(
                        excelModule.GetSheet<T>(Lumina_Language.English).GetRow(result.RowId)!,
                        excelModule.GetSheet<T>(Lumina_Language.French).GetRow(result.RowId)!,
                        excelModule.GetSheet<T>(Lumina_Language.German).GetRow(result.RowId)!,
                        excelModule.GetSheet<T>(Lumina_Language.Japanese).GetRow(result.RowId)!
                    ).Invoke(result)
            );
    }
}
