using Lumina_Language = Lumina.Data.Language;
using ExcelModule = Lumina.Excel.ExcelModule;
using Item = Lumina.Excel.GeneratedSheets2.Item;

namespace EasyTranslate.Infrastructure.Lumina.Sheets;

using EasyTranslate.Domain.Entities;

public class ItemSheet(ExcelModule excelModule)
{
    public IEnumerable<Content> SearchByName(
        string name,
        Lumina_Language searchLanguage,
        CancellationToken cancellationToken
    )
    {
        var itemSheet = excelModule.GetSheet<Item>(searchLanguage);
        return itemSheet
               ?.TakeWhile(_ => !cancellationToken.IsCancellationRequested)
               .Where(item => item.Name.RawString.Contains(name, StringComparison.OrdinalIgnoreCase))
               .Take(100)
               .ToArray() // Bad iteration performance if we don't cast to array
               .Select(
                   item => new Content(
                       item.RowId,
                       item.Icon,
                       new Dictionary<Language, string>
                       {
                           {
                               Language.English,
                               excelModule
                                   .GetSheet<Item>(Lumina_Language.English)
                                   ?.GetRow(item.RowId)
                                   ?.Name.RawString
                               ?? ""
                           },
                           {
                               Language.French,
                               excelModule
                                   .GetSheet<Item>(Lumina_Language.French)
                                   ?.GetRow(item.RowId)
                                   ?.Name.RawString
                               ?? ""
                           },
                           {
                               Language.German,
                               excelModule
                                   .GetSheet<Item>(Lumina_Language.German)
                                   ?.GetRow(item.RowId)
                                   ?.Name.RawString
                               ?? ""
                           },
                           {
                               Language.Japanese,
                               excelModule
                                   .GetSheet<Item>(Lumina_Language.Japanese)
                                   ?.GetRow(item.RowId)
                                   ?.Name.RawString
                               ?? ""
                           },
                       }
                   )
               )
               ?? Enumerable.Empty<Content>();
    }
}
