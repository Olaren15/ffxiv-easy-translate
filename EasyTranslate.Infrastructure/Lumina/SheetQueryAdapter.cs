using ExcelRow = Lumina.Excel.ExcelRow;

namespace EasyTranslate.Infrastructure.Lumina;

using EasyTranslate.Domain.Entities;

public interface ISheetQueryAdapter<T> where T : ExcelRow
{
    public Func<T, bool> WhereClause(string searchName);

    public Func<T, Content> MapToContent(
        ExcelSheet<T> englishSheet,
        ExcelSheet<T> frenchSheet,
        ExcelSheet<T> germanSheet,
        ExcelSheet<T> japaneseSheet
    );
}
