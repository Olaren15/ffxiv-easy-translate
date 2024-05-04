namespace EasyTranslate.Infrastructure.GameData;

using Domain.Entities;
using Language = Lumina.Data.Language;

public interface ISearchByNameQuery
{
    IEnumerable<Content> Execute(string searchName, Language searchLanguage);
}
