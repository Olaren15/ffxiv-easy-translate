using Lumina_Language = Lumina.Data.Language;

namespace EasyTranslate.Infrastructure.Lumina;

using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.Lumina.Sheets;

public class LuminaContentRepository(ItemSheet itemSheet) : IContentRepository
{
    public Task<IEnumerable<Content>> SearchByName(
        string name,
        Language searchLanguage,
        CancellationToken cancellationToken
    )
    {
        var luminaSearchLanguage = searchLanguage switch
        {
            Language.English => Lumina_Language.English,
            Language.French => Lumina_Language.French,
            Language.German => Lumina_Language.German,
            Language.Japanese => Lumina_Language.Japanese,
            var _ => Lumina_Language.English,
        };

        return Task.Run(() => itemSheet.SearchByName(name, luminaSearchLanguage, cancellationToken), cancellationToken);
    }
}
