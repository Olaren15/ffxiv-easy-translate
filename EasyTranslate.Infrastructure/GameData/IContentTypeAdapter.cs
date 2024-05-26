using EasyTranslate.Domain.Entities;
using Lumina.Excel;

namespace EasyTranslate.Infrastructure.GameData;

public interface IContentTypeAdapter<in T> where T : ExcelRow
{
    public Func<T, bool> WhereClause(string searchName);

    public Func<T, Content> MapToContent(T english, T french, T german, T japanese);
}
