using EasyTranslate.Domain.Entities;
using Language = Lumina.Data.Language;

namespace EasyTranslate.Infrastructure.GameData;

using Language = Language;

public interface ISearchByNameQuery
{
    IEnumerable<Content> Execute(string searchName, Language searchLanguage);
}
