namespace EasyTranslate.Infrastructure.GameData;

using EasyTranslate.Domain.Entities;
using Lumina.Excel;

public interface IContentTypeAdapter<T> where T : ExcelRow
{
    public Func<T, bool> WhereClause(string searchName);

    public Func<T, Content> MapToContent(
        ExcelSheet<T> englishSheet,
        ExcelSheet<T> frenchSheet,
        ExcelSheet<T> germanSheet,
        ExcelSheet<T> japaneseSheet
    );
}
